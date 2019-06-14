// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BackgroundSettingsEditorViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.SettingsEditorViewModels.Personalization
{
    public class BackgroundSettingsEditorViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private const int OneMinute = 60 * 1000;
        private const int TenMinutes = OneMinute * 10;
        private const int ThirtyMinutes = OneMinute * 30;
        private const int OneHour = OneMinute * 60;
        private const int SixHours = OneHour * 6;
        private const int OneDay = OneHour * 24;

        private SlideshowAlbum _slideshowAlbum = new SlideshowAlbum("DesktopWallpaper", GlyphInfo.HardDrive, @"C:\");
        private bool _shuffleSlideshow;

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public SelectionObservableCollection<NamedValue<DesktopBackgroundKind>> BackgroundKinds { get; } =
            new SelectionObservableCollection<NamedValue<DesktopBackgroundKind>>(
                selectedIndex: 2,
                collection: new[]
                {
                    new NamedValue<DesktopBackgroundKind>(
                        Strings.BackgroundComboBoxPictureOption,
                        DesktopBackgroundKind.Picture),
                    new NamedValue<DesktopBackgroundKind>(
                        Strings.BackgroundComboBoxSolidColorOption,
                        DesktopBackgroundKind.SolidColor),
                    new NamedValue<DesktopBackgroundKind>(
                        Strings.BackgroundComboBoxSlideshowOption,
                        DesktopBackgroundKind.Slideshow),
                });

        public SelectionObservableCollection<NamedValue<int>> ChangePictureIntervals { get; } =
            new SelectionObservableCollection<NamedValue<int>>(
                selectedIndex: 0,
                collection: new[]
                {
                    new NamedValue<int>(Strings.BackgroundChangePictureIntervalComboBox1MinuteOption, OneMinute),
                    new NamedValue<int>(Strings.BackgroundChangePictureIntervalComboBox10MinutesOption, TenMinutes),
                    new NamedValue<int>(
                        Strings.BackgroundChangePictureIntervalComboBox30MinutesOption,
                        ThirtyMinutes),
                    new NamedValue<int>(Strings.BackgroundChangePictureIntervalComboBox1HourOption, OneHour),
                    new NamedValue<int>(Strings.BackgroundChangePictureIntervalComboBox6HoursOption, SixHours),
                    new NamedValue<int>(Strings.BackgroundChangePictureIntervalComboBox1DayOption, OneDay),
                });

        public SelectionObservableCollection<NamedValue<DesktopBackgroundFitMode>> FitKinds { get; } =
            new SelectionObservableCollection<NamedValue<DesktopBackgroundFitMode>>(
                selectedIndex: 0,
                collection: new[]
                {
                    new NamedValue<DesktopBackgroundFitMode>(
                        Strings.BackgroundFitComboBoxFillOption,
                        DesktopBackgroundFitMode.Fill),
                    new NamedValue<DesktopBackgroundFitMode>(
                        Strings.BackgroundFitComboBoxFitOption,
                        DesktopBackgroundFitMode.Fit),
                    new NamedValue<DesktopBackgroundFitMode>(
                        Strings.BackgroundFitComboBoxStretchOption,
                        DesktopBackgroundFitMode.Stretch),
                    new NamedValue<DesktopBackgroundFitMode>(
                        Strings.BackgroundFitComboBoxTileOption,
                        DesktopBackgroundFitMode.Tile),
                    new NamedValue<DesktopBackgroundFitMode>(
                        Strings.BackgroundFitComboBoxCenterOption,
                        DesktopBackgroundFitMode.Center),
                    new NamedValue<DesktopBackgroundFitMode>(
                        Strings.BackgroundFitComboBoxSpanOption,
                        DesktopBackgroundFitMode.Span),
                });

        public SlideshowAlbum SlideshowAlbum
        {
            get => _slideshowAlbum;
            set => SetProperty(ref _slideshowAlbum, value);
        }

        public bool ShuffleSlideshow
        {
            get => _shuffleSlideshow;
            set => SetProperty(ref _shuffleSlideshow, value);
        }
    }

    /// <summary>
    /// Represents the different kinds of desktop backgrounds.
    /// </summary>
    public enum DesktopBackgroundKind
    {
        Picture,
        SolidColor,
        Slideshow,
    }

    /// <summary>
    /// Represents the different kinds of modes in fitting the background image on the desktop.
    /// </summary>
    public enum DesktopBackgroundFitMode
    {
        Fill,
        Fit,
        Stretch,
        Tile,
        Center,
        Span,
    }

    public class SlideshowAlbum
    {
        public SlideshowAlbum(string displayName, GlyphInfo glyphInfo, string folderPath)
        {
            DisplayName = displayName;
            GlyphInfo = glyphInfo;
            FolderPath = folderPath;
        }

        public string DisplayName { get; }
        public GlyphInfo GlyphInfo { get; }
        public string FolderPath { get; }
    }
}
