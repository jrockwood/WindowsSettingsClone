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
    using Shared.CommandBridge;
    using Shared.Commands;
    using Windows.ApplicationModel.AppService;
    using Windows.Foundation.Collections;
    using RegistryHive = Microsoft.Win32.RegistryHive;

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

        private static async void OnConnectionRequestReceived(
            AppServiceConnection sender,
            AppServiceRequestReceivedEventArgs args)
        {
            var returnValueSet = new ValueSet();

            if (!ServiceCommand.TryDeserialize(
                args.Request.Message,
                out ServiceCommand command,
                out ServiceCommandResponse errorResponse))
            {
                errorResponse.SerializeTo(returnValueSet);
            }
            else
            {
                switch (command)
                {
                    case RegistryReadIntValueCommand registryCommand:
                        try
                        {
                            var registryHive = (RegistryHive)Enum.Parse(
                                typeof(RegistryHive),
                                registryCommand.Hive.ToString());

                            using (var baseKey = RegistryKey.OpenBaseKey(registryHive, RegistryView.Registry64))
                            using (RegistryKey subKey = baseKey.OpenSubKey(registryCommand.Key, writable: false))
                            {
                                int? value = (int?)subKey?.GetValue(
                                    registryCommand.ValueName,
                                    registryCommand.DefaultValue);

                                ServiceCommandResponse response = value.HasValue
                                    ? ServiceCommandResponse.Create(command.CommandName, value)
                                    : ServiceCommandResponse.CreateError(
                                        command.CommandName,
                                        ServiceErrorInfo.RegistryValueNameNotFound(
                                            registryCommand.Hive,
                                            registryCommand.Key,
                                            registryCommand.ValueName));

                                response.SerializeTo(returnValueSet);
                            }
                        }
                        catch (Exception e)
                        {
                            var response = ServiceCommandResponse.CreateError(command.CommandName, e);
                            response.SerializeTo(returnValueSet);
                        }

                        break;
                }
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
