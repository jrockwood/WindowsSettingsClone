// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryReadService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.FullTrustServices
{
    using System.Threading.Tasks;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using ServiceContracts.Win32Services;
    using Shared.Commands;

    /// <summary>
    /// Implementation of the <see cref="IRegistryReadService"/> that calls across the desktop bridge to read from the
    /// Windows Registry.
    /// </summary>
    internal class RegistryReadService : FullTrustServiceBase, IRegistryReadService
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public RegistryReadService(ICommandBridgeClientService commandBridge)
            : base(commandBridge)
        {
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public async Task<int> ReadValueAsync(RegistryBaseKey baseKey, string key, string valueName, int defaultValue)
        {
            var command = new RegistryReadIntValueCommand(baseKey, key, valueName, defaultValue);
            IServiceCommandResponse response = await CommandBridge.SendCommandAsync(command);
            response.ThrowIfError();
            return (int)response.Result;
        }

        public async Task<bool> ReadValueAsync(RegistryBaseKey baseKey, string key, string valueName, bool defaultValue)
        {
            var command = new RegistryReadIntValueCommand(baseKey, key, valueName, defaultValue ? 1 : 0);
            IServiceCommandResponse response = await CommandBridge.SendCommandAsync(command);
            response.ThrowIfError();
            return (int)response.Result != 0;
        }

        public async Task<string> ReadValueAsync(RegistryBaseKey baseKey, string key, string valueName, string defaultValue)
        {
            var command = new RegistryReadStringValueCommand(baseKey, key, valueName, defaultValue);
            IServiceCommandResponse response = await CommandBridge.SendCommandAsync(command);
            response.ThrowIfError();
            return (string)response.Result;
        }
    }
}
