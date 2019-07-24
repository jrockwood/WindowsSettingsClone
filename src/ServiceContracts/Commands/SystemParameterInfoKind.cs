// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemParameterInfoKind.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Commands
{
    /// <summary>
    /// Enumerates the different kinds of functions for the <c>SystemParametersInfo</c> function.
    /// </summary>
    public enum SystemParameterInfoKind : uint
    {
        // IMPORTANT - keep these values the same as the underlying SPI_ values.
        // See https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfow.

        /// <summary>
        /// Retrieves the full path of the bitmap file for the desktop wallpaper.
        /// </summary>
        GetDesktopWallpaper = 0x0073,
    }
}
