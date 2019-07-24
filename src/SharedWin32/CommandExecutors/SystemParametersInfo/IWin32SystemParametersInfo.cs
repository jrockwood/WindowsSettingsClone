// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IWin32SystemParametersInfo.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.SystemParametersInfo
{
    using System.Text;
    using ServiceContracts.Commands;

    /// <summary>
    /// Contract for calling the Win32 <c>SystemParametersInfo</c> function. Extracted to an an interface for easy unit testing.
    /// </summary>
    public interface IWin32SystemParametersInfo
    {
        bool SystemParametersInfo(
            SystemParameterInfoKind uiAction,
            uint uiParam,
            StringBuilder pvParam,
            SystemParameterInfoUpdateKind fWinIni);

        /// <summary>
        /// Throws an exception with information about the last invocation's failure. If the last invocation succeeded,
        /// nothing happens.
        /// </summary>
        void ThrowExceptionForLastWin32Error();
    }
}
