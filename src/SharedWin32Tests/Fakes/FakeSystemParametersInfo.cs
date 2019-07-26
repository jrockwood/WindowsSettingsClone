// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeSystemParametersInfo.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests.Fakes
{
    using System.Text;
    using ServiceContracts.Commands;
    using SharedWin32.CommandExecutors.SystemParametersInfo;

    internal class FakeSystemParametersInfo : IWin32SystemParametersInfo
    {
        public bool SystemParametersInfo(
            SystemParameterInfoKind uiAction,
            uint uiParam,
            StringBuilder pvParam,
            SystemParameterInfoUpdateKind fWinIni)
        {
            return true;
        }

        public void ThrowExceptionForLastWin32Error()
        {
        }
    }
}
