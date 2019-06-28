// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandBridgeClientService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.CommandBridge
{
    using System.Threading.Tasks;

    /// <summary>
    /// Service contract for a client-side command bridge, which sends commands to the server and deserializes the result.
    /// </summary>
    public interface ICommandBridgeClientService
    {
        Task<ServiceCommandResponse> SendCommandAsync(ServiceCommand command);
    }
}
