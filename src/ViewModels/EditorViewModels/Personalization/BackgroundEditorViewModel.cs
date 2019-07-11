// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BackgroundEditorViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.EditorViewModels.Personalization
{
    using System.Threading;
    using System.Threading.Tasks;
    using Models.Personalization;
    using ServiceContracts.FullTrust;

    public class BackgroundEditorViewModel : EditorViewModel
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

        private SlideshowAlbum _slideshowAlbum = new SlideshowAlbum("DesktopWallpaper", @"C:\");
        private bool _shuffleSlideshow;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public BackgroundEditorViewModel(IRegistryWriteService registryWriteService)
            : base(registryWriteService, CreateBonusBarViewModel())
        {
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public override EditorKind EditorKind => EditorKind.PersonalizationBackground;
        public override string DisplayName => Strings.BackgroundSettingName;

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
            set => SetPropertyAndWaitForAsyncUpdate(
                ref _shuffleSlideshow,
                value,
                () => DesktopBackgroundSettings.SetShuffleSlideshowAsync(value, RegistryWriteService));
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        protected override async Task LoadInternalAsync(
            IRegistryReadService registryReadService,
            CancellationToken cancellationToken)
        {
            DesktopBackgroundSettings model = await DesktopBackgroundSettings.CreateAsync(registryReadService);
            BackgroundKinds.Select(model.BackgroundKind);
            FitKinds.Select(model.FitMode);
            ShuffleSlideshow = model.ShuffleSlideshow;
        }

        private static BonusBarViewModel CreateBonusBarViewModel()
        {
            return BonusBarViewModel.CreateStandard(
                relatedSettings: new[]
                {
                    new BonusBarNavigationLink(Strings.HighContrastSettingsLink,EditorKind.EaseOfAccessHighContrast),
                    new BonusBarNavigationLink(Strings.SyncYourSettingsLink, EditorKind.AccountsSyncYourSettings)
                },
                supportLinks: new[]
                {
                    new BonusBarWebLink(
                        Strings.ShowDesktopIconsLink,
                        "https://www.bing.com/search?q=show desktop icons windows 10 site:microsoft.com&form=B00032&ocid=SettingsHAQ-BingIA&mkt=en-US"),
                    new BonusBarWebLink(
                        Strings.FindNewThemesLink,
                        "https://www.bing.com/search?q=get themes windows 10 site:microsoft.com&form=B00032&ocid=SettingsHAQ-BingIA&mkt=en-US"),
                    new BonusBarWebLink(
                        Strings.ChangeMyDeesktopBackgroundLink,
                        "https://www.bing.com/search?q=change background picture windows 10 site:microsoft.com&form=B00032&ocid=SettingsHAQ-BingIA&mkt=en-US")
                });
        }
    }
}
