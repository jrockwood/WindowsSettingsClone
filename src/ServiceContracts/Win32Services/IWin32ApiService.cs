// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IWin32ApiService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Win32Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Service contract for invoking Win32 system functions.
    /// </summary>
    public interface IWin32ApiService
    {
        /// <summary>
        /// Gets the path to the current Windows desktop wallpaper or null if there is no wallpaper.
        /// </summary>
        Task<string> GetDesktopWallpaperPathAsync();
    }
}
