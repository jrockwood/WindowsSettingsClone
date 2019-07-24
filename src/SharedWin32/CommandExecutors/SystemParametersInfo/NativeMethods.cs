// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.SystemParametersInfo
{
    using System.Runtime.InteropServices;
    using System.Text;
    using ServiceContracts.Commands;

    /// <summary>
    /// Contains p/invoke methods that invoke underlying system calls.
    /// </summary>
    internal static class NativeMethods
    {
        public const int MaxPath = 260;
        public const int ErrorSuccess = 0;

        // For reading a string parameter
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(
            SystemParameterInfoKind uiAction,
            uint uiParam,
            StringBuilder pvParam,
            SystemParameterInfoUpdateKind fWinIni);
    }
}
