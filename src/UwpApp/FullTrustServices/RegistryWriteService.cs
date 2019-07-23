// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryWriteService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.FullTrustServices
{
    using System.Threading.Tasks;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using ServiceContracts.Win32;
    using Shared.Commands;
    using Shared.Diagnostics;

    /// <summary>
    /// Implementation of the <see cref="IRegistryWriteService"/> that calls across the desktop bridge to write to the
    /// Windows Registry.
    /// </summary>
    internal class RegistryWriteService : IRegistryWriteService
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly ICommandBridgeClientService _commandBridge;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public RegistryWriteService(ICommandBridgeClientService commandBridge)
        {
            _commandBridge = Param.VerifyNotNull(commandBridge, nameof(commandBridge));
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public async Task WriteValueAsync(RegistryBaseKey baseKey, string key, string valueName, int value)
        {
            var command = new RegistryWriteIntValueCommand(baseKey, key, valueName, value);
            IServiceCommandResponse response = await _commandBridge.SendCommandAsync(command);
            response.ThrowIfError();
        }

        public async Task WriteValueAsync(RegistryBaseKey baseKey, string key, string valueName, bool value)
        {
            var command = new RegistryWriteIntValueCommand(baseKey, key, valueName, value ? 1 : 0);
            IServiceCommandResponse response = await _commandBridge.SendCommandAsync(command);
            response.ThrowIfError();
        }

        public async Task WriteValueAsync(RegistryBaseKey baseKey, string key, string valueName, string value)
        {
            var command = new RegistryWriteStringValueCommand(baseKey, key, valueName, value);
            IServiceCommandResponse response = await _commandBridge.SendCommandAsync(command);
            response.ThrowIfError();
        }
    }
}
