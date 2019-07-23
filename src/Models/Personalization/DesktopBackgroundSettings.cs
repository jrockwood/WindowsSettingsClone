// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="DesktopBackgroundSettings.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Models.Personalization
{
    using System;
    using System.Threading.Tasks;
    using ServiceContracts.Commands;
    using ServiceContracts.FullTrust;
    using Shared.Diagnostics;

    /// <summary>
    /// Contains the model for all of the settings related to the desktop background.
    /// </summary>
    public class DesktopBackgroundSettings
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        // ReSharper disable once InconsistentNaming
        private const RegistryBaseKey HKCU = RegistryBaseKey.CurrentUser;

        private const string DesktopPath = @"Control Panel\Desktop";
        private const string SlideshowPath = @"Control Panel\Personalization\Desktop Slideshow";
        private const string WallpapersPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers";

        private const string BackgroundType = "BackgroundType";
        private const string Interval = "Interval";
        private const string Shuffle = "Shuffle";
        private const string WallpaperStyle = "WallpaperStyle";

        private const int PictureBackgroundType = 0;
        private const int SolidColorBackgroundType = 1;
        private const int SlideshowBackgroundType = 2;

        private const string FillStyle = "10";
        private const string FitStyle = "6";
        private const string StretchStyle = "2";
        private const string TileAndCenterStyle = "0";
        private const string SpanStyle = "22";

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private DesktopBackgroundSettings(
            DesktopBackgroundKind backgroundKind,
            DesktopBackgroundFitMode fitMode,
            bool shuffleSlideshow,
            int slideshowInterval)
        {
            BackgroundKind = backgroundKind;
            FitMode = fitMode;
            ShuffleSlideshow = shuffleSlideshow;
            SlideshowInterval = slideshowInterval;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public DesktopBackgroundKind BackgroundKind { get; }
        public DesktopBackgroundFitMode FitMode { get; }

        public int SlideshowInterval { get; }
        public bool ShuffleSlideshow { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static async Task<DesktopBackgroundSettings> CreateAsync(IRegistryReadService registryReadService)
        {
            Param.VerifyNotNull(registryReadService, nameof(registryReadService));

            int backgroundType = await registryReadService.ReadValueAsync(HKCU, WallpapersPath, BackgroundType, 0);
            int interval = await registryReadService.ReadValueAsync(HKCU, SlideshowPath, Interval, 1000 * 60);
            bool shuffle = await registryReadService.ReadValueAsync(HKCU, SlideshowPath, Shuffle, false);

            return new DesktopBackgroundSettings(
                backgroundKind: BackgroundTypeToFit(backgroundType),
                fitMode: DesktopBackgroundFitMode.Fill,
                shuffleSlideshow: shuffle,
                slideshowInterval: interval);
        }

        public static async Task SetShuffleSlideshowAsync(bool value, IRegistryWriteService registryWriteService)
        {
            await registryWriteService.WriteValueAsync(HKCU, SlideshowPath, Shuffle, value);
        }

        private static DesktopBackgroundKind BackgroundTypeToFit(int backgroundType)
        {
            switch (backgroundType)
            {
                case PictureBackgroundType:
                    return DesktopBackgroundKind.Picture;

                case SolidColorBackgroundType:
                    return DesktopBackgroundKind.SolidColor;

                case SlideshowBackgroundType:
                    return DesktopBackgroundKind.Slideshow;

                default:
                    throw new InvalidOperationException($"Unknown background type '{backgroundType}'");
            }
        }

        private static DesktopBackgroundFitMode WallpaperStyleToFitMode(string wallpaperStyle)
        {
            switch (wallpaperStyle)
            {
                case FillStyle:
                    return DesktopBackgroundFitMode.Fill;

                default:
                    throw new InvalidOperationException($"Unknown wallpaper style '{wallpaperStyle}'");
            }
        }
    }
}
