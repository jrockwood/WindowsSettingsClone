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
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Logging;
    using Shared.CommandBridge;
    using Shared.Commands;
    using Shared.Diagnostics;
    using Shared.Logging;
    using SharedWin32.CommandExecutors.Registry;
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

        private readonly AppServiceConnection _connection;
        private readonly ILogger _logger;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public UwpCommunicationBridge(ILogger logger)
        {
            _logger = Param.VerifyNotNull(logger, nameof(logger));

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

        private async void OnConnectionRequestReceived(
            AppServiceConnection sender,
            AppServiceRequestReceivedEventArgs args)
        {
            try
            {
                // Execute the command and get the response.
                IServiceCommandResponse response;

                if (!ServiceCommand.TryDeserialize(
                    args.Request.Message,
                    out ServiceCommand command,
                    out ServiceCommandResponse errorResponse))
                {
                    response = errorResponse;
                }
                else
                {
                    _logger.LogDebug($"Received command: {command.ToDebugString()}");

                    var registryExecutor = new RegistryCommandExecutor();

                    switch (command)
                    {
                        case RegistryReadIntValueCommand registryCommand:
                            response = registryExecutor.ExecuteRead(registryCommand, _logger);
                            break;

                        case RegistryReadStringValueCommand registryCommand:
                            response = registryExecutor.ExecuteRead(registryCommand, _logger);
                            break;

                        case RegistryWriteIntValueCommand registryCommand:
                            response = registryExecutor.ExecuteWrite(registryCommand, _logger);
                            break;

                        case RegistryWriteStringValueCommand registryCommand:
                            response = registryExecutor.ExecuteWrite(registryCommand, _logger);
                            break;

                        default:
                            response = ServiceCommandResponse.CreateError(
                                command.CommandName,
                                new InvalidOperationException($"Unknown or unhandled command '{command.CommandName}'"));
                            break;
                    }
                }

                // Serialize the response to a ValueSet.
                var returnValueSet = new ValueSet();
                response.SerializeTo(returnValueSet);

                _logger.LogDebug($"Sending response: {response.ToDebugString()}");

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

        private static void OnConnectionServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            // The UWP app service is closing, so shut down this application.
            Environment.Exit(0);
        }
    }
}
