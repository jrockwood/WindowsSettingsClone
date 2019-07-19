// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandBridgeClientService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.ViewServices
{
    using System;
    using System.Threading.Tasks;
    using ServiceContracts.CommandBridge;
    using Shared.CommandBridge;
    using Shared.Diagnostics;
    using Windows.ApplicationModel.AppService;
    using Windows.Foundation.Collections;

    /// <summary>
    /// Implements the client-side of a <see cref="ICommandBridgeClientService"/>.
    /// </summary>
    internal sealed class CommandBridgeClientService : ICommandBridgeClientService
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly AppServiceConnection _connection;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public CommandBridgeClientService(AppServiceConnection connection)
        {
            _connection = Param.VerifyNotNull(connection, nameof(connection));
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public async Task<IServiceCommandResponse> SendCommandAsync(IServiceCommand command)
        {
            var valueSet = new ValueSet();
            command.SerializeToValueSet(valueSet);
            AppServiceResponse bridgeResponse = await _connection.SendMessageAsync(valueSet);
            AppServiceResponseStatus status = bridgeResponse.Status;

            IServiceCommandResponse response;
            if (status == AppServiceResponseStatus.Success)
            {
                if (!ServiceCommandResponse.TryDeserializeFromValueSet(
                    bridgeResponse.Message,
                    out response,
                    out IServiceCommandResponse errorResponse))
                {
                    response = errorResponse;
                }
            }
            else
            {
                response = ServiceCommandResponse.CreateError(command.CommandName,
                    ServiceErrorInfo.InternalError($"AppServiceConnection failed with status '{status}'"));
            }

            return response;
        }
    }
}
