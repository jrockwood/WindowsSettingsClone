// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="DesktopBackgroundSettingsTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Models.Tests.Personalization
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Models.Personalization;
    using NUnit.Framework;
    using ServiceContracts.Commands;
    using Shared.Tests.FakeServices;

    public class DesktopBackgroundSettingsTests
    {
        [Test]
        public async Task CreateAsync_should_throw_on_null_args()
        {
            Func<Task<DesktopBackgroundSettings>> action = () => DesktopBackgroundSettings.CreateAsync(null);
            (await action.Should().ThrowExactlyAsync<ArgumentNullException>()).And.ParamName.Should()
                .Be("registryReadService");
        }

        [Test]
        public async Task CreateAsync_should_map_background_types_correctly()
        {
            var fakeRegistry = new FakeRegistryService();
            var registryValueToBackgroundKinds = new Dictionary<int, DesktopBackgroundKind>
            {
                [0] = DesktopBackgroundKind.Picture,
                [1] = DesktopBackgroundKind.SolidColor,
                [2] = DesktopBackgroundKind.Slideshow,
            };

            foreach (KeyValuePair<int, DesktopBackgroundKind> pair in registryValueToBackgroundKinds)
            {
                fakeRegistry.SetMockedValue(
                    RegistryBaseKey.CurrentUser,
                    @"Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers",
                    "BackgroundType",
                    pair.Key);

                DesktopBackgroundSettings settings = await DesktopBackgroundSettings.CreateAsync(fakeRegistry);
                settings.BackgroundKind.Should().Be(pair.Value);
            }
        }

        //[Test]
        //public async Task CreateAsync_should_map_fit_modes_correctly()
        //{
        //    var fakeRegistry = new FakeRegistryService();
        //    var registryValueToFitMode = new Dictionary<string, DesktopBackgroundFitMode>
        //    {
        //        ["10"] = DesktopBackgroundFitMode.Fill,
        //        ["6"] = DesktopBackgroundFitMode.Fit,
        //        ["2"] = DesktopBackgroundFitMode.Center,
        //        ["3"] = DesktopBackgroundFitMode.Span,
        //        ["2"] = DesktopBackgroundFitMode.Stretch,
        //        ["0"] = DesktopBackgroundFitMode.Tile,
        //    };

        //    foreach (KeyValuePair<string, DesktopBackgroundFitMode> pair in registryValueToFitMode)
        //    {
        //        fakeRegistry.SetMockedValue(
        //            RegistryBaseKey.CurrentUser,
        //            @"Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers",
        //            "BackgroundType",
        //            pair.Key);

        //        DesktopBackgroundSettings settings = await DesktopBackgroundSettings.CreateAsync(fakeRegistry);
        //        settings.FitMode.Should().Be(pair.Value);
        //    }
        //}
    }
}
