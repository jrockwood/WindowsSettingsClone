// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="UwpCommunicationBridge.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.DesktopServicesApp
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Win32;
    using Windows.ApplicationModel.AppService;
    using Windows.Foundation.Collections;

    /// <summary>
    /// Communication bridge between the full-trust application (this) and the UWP sand-boxed application.
    /// </summary>
    /// <remarks>
    /// Much of this code comes from <see href="https://github.com/PaulaScholz/RegistryUWP"/>.
    /// </remarks>
    internal sealed class UwpCommunicationBridge
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        // HRESULT 80004005 is E_FAIL
        // ReSharper disable once InconsistentNaming
        private const int E_FAIL = unchecked((int)0x80004005);

        private readonly AppServiceConnection _connection;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public UwpCommunicationBridge()
        {
            _connection = new AppServiceConnection
            {
                // The AppServiceName must match the name declared in the Packaging project's Package.appxmanifest file.
                // You'll have to view it as code to see the XML. It will look like this:
                //
                //      <Extensions>
                //        <desktop:Extension
                //          Category="windows.fullTrustProcess"
                //          Executable="DesktopServicesApp\DesktopServicesApp.exe" />
                //        <uap:Extension Category="windows.appService">
                //          <uap:AppService Name="org.rockwood.settingsclone" />
                //        </uap:Extension>
                //      </Extensions>
                AppServiceName = "org.rockwood.settingsclone",
                PackageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName
            };

            // Hook up the connection event handlers.
            _connection.RequestReceived += OnConnectionRequestReceived;
            _connection.ServiceClosed += OnConnectionServiceClosed;
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public async Task<bool> InitializeAsync()
        {
            // open a connection to the UWP AppService
            AppServiceConnectionStatus result = await _connection.OpenAsync();
            return result == AppServiceConnectionStatus.Success;
        }

        private static async void OnConnectionRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            ValueSet message = args.Request.Message;

            string commandName = message["CommandName"] as string;

            var returnValueSet = new ValueSet
            {
                ["CommandName"] = commandName,
            };

            switch (commandName)
            {
                case "ReadRegistryKey":
                    try
                    {
                        string registryHiveString = message["RegistryHive"] as string ?? throw new InvalidOperationException("Missing parameter: RegistryHive");
                        string registryViewString = message["RegistryView"] as string ?? RegistryView.Registry64.ToString();
                        string registryPath = message["RegistryPath"] as string ?? throw new InvalidOperationException("Missing parameter: RegistryPath");
                        string registryValueName = message["RegistryValueName"] as string ?? throw new InvalidOperationException("Missing parameter: RegistryValueName");
                        object defaultValue = message["DefaultValue"];
                        string registryValueOptionsString = message["RegistryValueOptions"] as string ?? RegistryValueOptions.None.ToString();

                        var registryHive = (RegistryHive)Enum.Parse(typeof(RegistryHive), registryHiveString);
                        var registryView = (RegistryView)Enum.Parse(typeof(RegistryView), registryViewString);
                        var registryValueOptions = (RegistryValueOptions)Enum.Parse(typeof(RegistryValueOptions), registryValueOptionsString);

                        // open HKLM with a 64bit view. If you use Registry32, your view will be virtualized to the current user
                        using (var baseKey = RegistryKey.OpenBaseKey(registryHive, registryView))
                        using (RegistryKey subKey = baseKey.OpenSubKey(registryPath, writable: false))
                        {
                            object value = subKey.GetValue(registryValueName, defaultValue, registryValueOptions);
                            returnValueSet.Add("CommandResult", value);
                        }
                    }
                    catch (Exception e)
                    {
                        returnValueSet.Add("ExceptionMessage", e.Message);
                    }

                    break;
            }

            try
            {
                // Return the data to the caller.
                await args.Request.SendResponseAsync(returnValueSet);
            }
            finally
            {
                // Complete the deferral so that the platform knows that we're done responding to the app service call.
                // Note for error handling: this must be called even if SendResponseAsync() throws an exception.
                args.GetDeferral().Complete();
            }
        }

        private void OnConnectionServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            // The UWP app service is closing, so shut down this application.
            Environment.Exit(0);
        }
    }
}
