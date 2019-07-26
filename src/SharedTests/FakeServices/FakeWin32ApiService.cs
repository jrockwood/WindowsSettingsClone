// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeWin32ApiService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Tests.FakeServices
{
    using System.Threading.Tasks;
    using ServiceContracts.Win32Services;

    /// <summary>
    /// Implements a fake implementation of an <see cref="IWin32ApiService"/>.
    /// </summary>
    public class FakeWin32ApiService : IWin32ApiService
    {
        public string WallpaperPath { get; set; }

        public Task<string> GetDesktopWallpaperPathAsync()
        {
            return Task.FromResult(WallpaperPath);
        }
    }
}
