// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemParameterInfoUpdateKind.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Commands
{
    using System;

    [Flags]
    public enum SystemParameterInfoUpdateKind : uint
    {
        // IMPORTANT - keep these values the same as the underlying SPI_ values.
        // See https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfow.

        None = 0x00,

        /// <summary>
        /// Writes the new system-wide parameter setting to the user profile.
        /// </summary>
        UpdateIniFile = 0x01,

        /// <summary>
        /// Broadcasts the WM_SETTINGCHANGE message after updating the user profile.
        /// </summary>
        SendChange = 0x02,

        /// <summary>
        /// Same as <see cref="SendChange"/>.
        /// </summary>
        SendWinIniChange = 0x02,
    }
}
