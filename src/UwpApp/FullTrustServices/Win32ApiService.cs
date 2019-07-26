// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Win32ApiService.cs" company="Justin Rockwood">
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
    /// Implementation of the <see cref="IWin32ApiService"/> that calls across the desktop bridge to read from the
    /// Windows Registry.
    /// </summary>
    internal class Win32ApiService : FullTrustServiceBase, IWin32ApiService
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public Win32ApiService(ICommandBridgeClientService commandBridge)
            : base(commandBridge)
        {
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Gets the path to the current Windows desktop wallpaper or null if there is no wallpaper.
        /// </summary>
        public async Task<string> GetDesktopWallpaperPathAsync()
        {
            var command = new SystemParametersInfoGetValueCommand(SystemParameterInfoKind.GetDesktopWallpaper);
            IServiceCommandResponse response = await CommandBridge.SendCommandAsync(command);
            response.ThrowIfError();
            return (string)response.Result;
        }
    }
}
