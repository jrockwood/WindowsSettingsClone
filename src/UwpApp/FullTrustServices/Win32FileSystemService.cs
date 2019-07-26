// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Win32FileSystemService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.FullTrustServices
{
    using System.Threading.Tasks;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Win32Services;
    using Shared.Commands;

    internal class Win32FileSystemService : FullTrustServiceBase, IWin32FileSystemService
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public Win32FileSystemService(ICommandBridgeClientService commandBridge)
            : base(commandBridge)
        {
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public async Task CopyFileAsync(string sourceFileName, string destinationFileName, bool overwrite)
        {
            var command = new FileCopyCommand(sourceFileName, destinationFileName, overwrite);
            IServiceCommandResponse response = await CommandBridge.SendCommandAsync(command);
            response.ThrowIfError();
        }
    }
}
