// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemParametersInfoCommandExecutorTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests.CommandExecutors.SystemParametersInfo
{
    using System.Text;
    using FluentAssertions;
    using Moq;
    using NUnit.Framework;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using Shared.CommandBridge;
    using Shared.Commands;
    using SharedWin32.CommandExecutors.SystemParametersInfo;

    public class SystemParametersInfoCommandExecutorTests
    {
        [Test]
        public void ExecuteGet_for_GetDesktopWallpaper()
        {
            const string wallpaperPath = @"C:\Users\Somebody\Wallpaper.png";

            var mock = new Mock<IWin32SystemParametersInfo>();
            mock.Setup(
                    x => x.SystemParametersInfo(
                        SystemParameterInfoKind.GetDesktopWallpaper,
                        NativeMethods.MaxPath,
                        It.IsAny<StringBuilder>(),
                        SystemParameterInfoUpdateKind.None))
                .Returns(
                    (
                        SystemParameterInfoKind uiAction,
                        uint uiParam,
                        StringBuilder pvParam,
                        SystemParameterInfoUpdateKind fWinIni) =>
                    {
                        pvParam.Append(wallpaperPath);
                        return true;
                    });

            var executor = new SystemParametersInfoCommandExecutor(mock.Object);
            executor.ExecuteGet(new SystemParametersInfoGetValueCommand(SystemParameterInfoKind.GetDesktopWallpaper))
                .Should()
                .BeEquivalentTo(
                    ServiceCommandResponse.Create(ServiceCommandName.SystemParametersInfoGetValue, wallpaperPath));
        }

        [Test]
        public void ExecuteSet_for_SetDesktopWallpaper()
        {
        }
    }
}
