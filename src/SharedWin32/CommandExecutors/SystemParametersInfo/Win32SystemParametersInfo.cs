// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Win32SystemParametersInfo.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.SystemParametersInfo
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using ServiceContracts.Commands;

    internal class Win32SystemParametersInfo : IWin32SystemParametersInfo
    {
        public bool SystemParametersInfo(
            SystemParameterInfoKind uiAction,
            uint uiParam,
            StringBuilder pvParam,
            SystemParameterInfoUpdateKind fWinIni)
        {
            return NativeMethods.SystemParametersInfo(uiAction, uiParam, pvParam, fWinIni);
        }

        public void ThrowExceptionForLastWin32Error()
        {
            int win32Error = Marshal.GetLastWin32Error();
            if (win32Error != NativeMethods.ErrorSuccess)
            {
                int errorCode = Marshal.GetHRForLastWin32Error();
                Marshal.ThrowExceptionForHR(errorCode, new IntPtr(-1));
            }
        }
    }
}
