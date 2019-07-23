// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryPageViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
{
    using System;
    using System.ComponentModel;
    using EditorViewModels;
    using ServiceContracts.Logging;
    using ServiceContracts.ViewServices;
    using Shared.Diagnostics;

    /// <summary>
    /// Page that contains a grouping of individual settings. Examples include System, Devices, and Personalization.
    /// </summary>
    public class CategoryPageViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private EditorViewModel _currentEditor;
        private readonly ILogger _logger;
        private readonly IAppServiceLocator _serviceLocator;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private CategoryPageViewModel(
            ILogger logger,
            IAppServiceLocator serviceLocator,
            CategoryPageNavigationViewModel navigationViewModel)
        {
            _logger = Param.VerifyNotNull(logger, nameof(logger));
            _serviceLocator = Param.VerifyNotNull(serviceLocator, nameof(serviceLocator));

            Navigation = Param.VerifyNotNull(navigationViewModel, nameof(navigationViewModel));
            Navigation.PropertyChanged += OnNavigationPropertyChanged;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public CategoryPageNavigationViewModel Navigation { get; }

        public EditorViewModel CurrentEditor
        {
            get => _currentEditor;
            set => SetProperty(ref _currentEditor, value);
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Creates a new <see cref="CategoryPageViewModel"/> corresponding to the specified <see
        /// cref="CategoryKind"/>. Called from the view.
        /// </summary>
        public static CategoryPageViewModel CreateFromCategoryKind(
            CategoryKind category,
            ILogger logger,
            IAppServiceLocator serviceLocator)
        {
            switch (category)
            {
                case CategoryKind.System:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.System,
                            Strings.SystemCategoryName,
                            new[]
                            {
                                new CategoryPageNavigationItem(
                                    Strings.DisplaySettingName,
                                    EditorKind.SystemDisplay,
                                    GlyphInfo.TvMonitor),
                                new CategoryPageNavigationItem(
                                    Strings.SoundSettingName,
                                    EditorKind.SystemSound,
                                    GlyphInfo.Volume),
                                new CategoryPageNavigationItem(
                                    Strings.NotificationsAndActionsSettingName,
                                    EditorKind.SystemNotificationsAndActions,
                                    GlyphInfo.Message),
                                new CategoryPageNavigationItem(
                                    Strings.FocusAssistSettingName,
                                    EditorKind.SystemFocusAssist,
                                    GlyphInfo.QuietHours),
                                new CategoryPageNavigationItem(
                                    Strings.PowerAndSleepSettingName,
                                    EditorKind.SystemPowerAndSleep,
                                    GlyphInfo.PowerButton),
                                new CategoryPageNavigationItem(
                                    Strings.StorageSettingName,
                                    EditorKind.SystemStorage,
                                    GlyphInfo.HardDrive),
                                new CategoryPageNavigationItem(
                                    Strings.TabletModeSettingName,
                                    EditorKind.SystemTabletMode,
                                    GlyphInfo.TabletMode),
                                new CategoryPageNavigationItem(
                                    Strings.MultitaskingSettingName,
                                    EditorKind.SystemMultitasking,
                                    GlyphInfo.TaskView),
                                new CategoryPageNavigationItem(
                                    Strings.ProjectingToThisPCSettingName,
                                    EditorKind.SystemProjectingToThisPC,
                                    GlyphInfo.Project),
                                new CategoryPageNavigationItem(
                                    Strings.SharedExperiencesSettingName,
                                    EditorKind.SystemSharedExperiences,
                                    GlyphInfo.Connected),
                                new CategoryPageNavigationItem(
                                    Strings.ClipboardSettingName,
                                    EditorKind.SystemClipboard,
                                    GlyphInfo.Paste),
                                new CategoryPageNavigationItem(
                                    Strings.RemoteDesktopSettingName,
                                    EditorKind.SystemRemoteDesktop,
                                    GlyphInfo.Remote),
                                new CategoryPageNavigationItem(
                                    Strings.AboutSettingName,
                                    EditorKind.SystemAbout,
                                    GlyphInfo.Info),
                            }));

                case CategoryKind.Devices:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.Devices,
                            Strings.DevicesCategoryName,
                            new[]
                            {
                                new CategoryPageNavigationItem(
                                    Strings.BluetoothAndOtherDevicesSettingName,
                                    EditorKind.DevicesBluetoothAndOtherDevices,
                                    GlyphInfo.Devices),
                                new CategoryPageNavigationItem(
                                    Strings.PrintersAndScannersSettingName,
                                    EditorKind.DevicesPrintersAndScanners,
                                    GlyphInfo.Print),
                                new CategoryPageNavigationItem(
                                    Strings.MouseSettingName,
                                    EditorKind.DevicesMouse,
                                    GlyphInfo.Mouse),
                                new CategoryPageNavigationItem(
                                    Strings.TypingSettingName,
                                    EditorKind.DevicesTyping,
                                    GlyphInfo.KeyboardClassic),
                                new CategoryPageNavigationItem(
                                    Strings.PenAndWindowsInkSettingName,
                                    EditorKind.DevicesPenAndWindowsInk,
                                    GlyphInfo.PenWorkspace),
                                new CategoryPageNavigationItem(
                                    Strings.AutoPlaySettingName,
                                    EditorKind.DevicesAutoPlay,
                                    GlyphInfo.PlaybackRate1x),
                                new CategoryPageNavigationItem(
                                    Strings.UsbSettingName,
                                    EditorKind.DevicesUsb,
                                    GlyphInfo.Usb),
                            }));

                case CategoryKind.Phone:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.Phone,
                            Strings.PhoneCategoryName));

                case CategoryKind.NetworkAndInternet:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.NetworkAndInternet,
                            Strings.NetworkAndInternetCategoryName,
                            new[]
                            {
                                new CategoryPageNavigationItem(
                                    Strings.NetworkStatusSettingName,
                                    EditorKind.NetworkAndInternetNetworkStatus,
                                    GlyphInfo.MyNetwork),
                                new CategoryPageNavigationItem(
                                    Strings.EthernetSettingName,
                                    EditorKind.NetworkAndInternetEthernet,
                                    GlyphInfo.Ethernet),
                                new CategoryPageNavigationItem(
                                    Strings.DialUpSettingName,
                                    EditorKind.NetworkAndInternetDialUp,
                                    GlyphInfo.DialUp),
                                new CategoryPageNavigationItem(
                                    Strings.VpnSettingName,
                                    EditorKind.NetworkAndInternetVpn,
                                    GlyphInfo.Vpn),
                                new CategoryPageNavigationItem(
                                    Strings.DataUsageSettingName,
                                    EditorKind.NetworkAndInternetDataUsage,
                                    GlyphInfo.PieSingle),
                                new CategoryPageNavigationItem(
                                    Strings.ProxySettingName,
                                    EditorKind.NetworkAndInternetProxy,
                                    GlyphInfo.Globe),
                            }));

                case CategoryKind.Personalization:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.Personalization,
                            Strings.PersonalizationCategoryName,
                            new[]
                            {
                                new CategoryPageNavigationItem(
                                    Strings.BackgroundSettingName,
                                    EditorKind.PersonalizationBackground,
                                    GlyphInfo.Photo2),
                                new CategoryPageNavigationItem(
                                    Strings.ColorsSettingName,
                                    EditorKind.PersonalizationColors,
                                    GlyphInfo.Color),
                                new CategoryPageNavigationItem(
                                    Strings.LockScreenSettingName,
                                    EditorKind.PersonalizationLockScreen,
                                    GlyphInfo.LockScreenDesktop),
                                new CategoryPageNavigationItem(
                                    Strings.ThemesSettingName,
                                    EditorKind.PersonalizationThemes,
                                    GlyphInfo.Personalize),
                                new CategoryPageNavigationItem(
                                    Strings.FontsSettingName,
                                    EditorKind.PersonalizationFonts,
                                    GlyphInfo.Font),
                                new CategoryPageNavigationItem(
                                    Strings.StartSettingName,
                                    EditorKind.PersonalizationStart,
                                    GlyphInfo.Tiles),
                                new CategoryPageNavigationItem(
                                    Strings.TaskbarSettingName,
                                    EditorKind.PersonalizationTaskbar,
                                    GlyphInfo.DockBottom),
                            }));

                case CategoryKind.Apps:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.Apps,
                            Strings.AppsCategoryName,
                            new[]
                            {
                                new CategoryPageNavigationItem(
                                    Strings.AppsAndFeaturesSettingName,
                                    EditorKind.AppsAppsAndFeatures,
                                    GlyphInfo.AllApps),
                                new CategoryPageNavigationItem(
                                    Strings.DefaultAppsSettingName,
                                    EditorKind.AppsDefaultApps,
                                    GlyphInfo.OpenWith),
                                new CategoryPageNavigationItem(
                                    Strings.OfflineMapsSettingName,
                                    EditorKind.AppsOfflineMaps,
                                    GlyphInfo.DownloadMap),
                                new CategoryPageNavigationItem(
                                    Strings.AppsForWebsitesSettingName,
                                    EditorKind.AppsAppsForWebsites,
                                    GlyphInfo.NewWindow),
                                new CategoryPageNavigationItem(
                                    Strings.VideoPlaybackSettingName,
                                    EditorKind.AppsVideoPlayback,
                                    GlyphInfo.Video),
                                new CategoryPageNavigationItem(
                                    Strings.StartupSettingName,
                                    EditorKind.AppsStartup,
                                    GlyphInfo.SetLockScreen),
                            }));

                case CategoryKind.Accounts:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.Accounts,
                            Strings.AccountsCategoryName,
                            new[]
                            {
                                new CategoryPageNavigationItem(
                                    Strings.YourInfoSettingName,
                                    EditorKind.AccountsYourInfo,
                                    GlyphInfo.ContactInfo),
                                new CategoryPageNavigationItem(
                                    Strings.EmailAndAccountsSettingName,
                                    EditorKind.AccountsEmailAndAccounts,
                                    GlyphInfo.Mail),
                                new CategoryPageNavigationItem(
                                    Strings.SignInOptionsSettingName,
                                    EditorKind.AccountsSignInOptions,
                                    GlyphInfo.Permissions),
                                new CategoryPageNavigationItem(
                                    Strings.AccessWorkOrSchoolSettingName,
                                    EditorKind.AccountsAccessWorkOrSchool,
                                    GlyphInfo.Work),
                                new CategoryPageNavigationItem(
                                    Strings.FamilyAndOtherUsersSettingName,
                                    EditorKind.AccountsFamilyAndOtherUsers,
                                    GlyphInfo.AddFriend),
                                new CategoryPageNavigationItem(
                                    Strings.SyncYourSettingsSettingName,
                                    EditorKind.AccountsSyncYourSettings,
                                    GlyphInfo.Sync),
                            }));

                case CategoryKind.TimeAndLanguage:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.TimeAndLanguage,
                            Strings.TimeAndLanguageCategoryName,
                            new[]
                            {
                                new CategoryPageNavigationItem(
                                    Strings.DateAndTimeSettingName,
                                    EditorKind.TimeAndLanguageDateAndTime,
                                    GlyphInfo.DateTime),
                                new CategoryPageNavigationItem(
                                    Strings.RegionSettingName,
                                    EditorKind.TimeAndLanguageRegion,
                                    GlyphInfo.Globe2),
                                new CategoryPageNavigationItem(
                                    Strings.LanguageSettingName,
                                    EditorKind.TimeAndLanguageLanguage,
                                    GlyphInfo.Characters),
                                new CategoryPageNavigationItem(
                                    Strings.SpeechSettingName,
                                    EditorKind.TimeAndLanguageSpeech,
                                    GlyphInfo.Microphone),
                            }));

                case CategoryKind.Gaming:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.Gaming,
                            Strings.GamingCategoryName,
                            new[]
                            {
                                new CategoryPageNavigationItem(
                                    Strings.GameBarSettingName,
                                    EditorKind.GamingGameBar,
                                    GlyphInfo.GameBar),
                                new CategoryPageNavigationItem(
                                    Strings.CapturesSettingName,
                                    EditorKind.GamingCaptures,
                                    GlyphInfo.Captures),
                                new CategoryPageNavigationItem(
                                    Strings.BroadcastingSettingName,
                                    EditorKind.GamingBroadcasting,
                                    GlyphInfo.SatelliteDish),
                                new CategoryPageNavigationItem(
                                    Strings.GameModeSettingName,
                                    EditorKind.GamingGameMode,
                                    GlyphInfo.SpeedHigh),
                                new CategoryPageNavigationItem(
                                    Strings.XboxNetworkingSettingName,
                                    EditorKind.GamingXboxNetworking,
                                    GlyphInfo.XboxLogo),
                            }));

                case CategoryKind.EaseOfAccess:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.EaseOfAccess,
                            Strings.EaseOfAccessCategoryName,
                            new[]
                            {
                                // Vision
                                new CategoryPageNavigationItem(
                                    Strings.DisplaySettingName,
                                    EditorKind.EaseOfAccessDisplay,
                                    GlyphInfo.TvMonitor,
                                    headerDisplayName: Strings.VisionSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.CursorAndPointerSettingName,
                                    EditorKind.EaseOfAccessCursorAndPointer,
                                    GlyphInfo.TouchPointer,
                                    headerDisplayName: Strings.VisionSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.MagnifierSettingName,
                                    EditorKind.EaseOfAccessMagnifier,
                                    GlyphInfo.ZoomIn,
                                    headerDisplayName: Strings.VisionSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.ColorFiltersSettingName,
                                    EditorKind.EaseOfAccessColorFilters,
                                    GlyphInfo.Color,
                                    headerDisplayName: Strings.VisionSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.HighContrastSettingName,
                                    EditorKind.EaseOfAccessHighContrast,
                                    GlyphInfo.Brightness,
                                    headerDisplayName: Strings.VisionSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.NarratorSettingName,
                                    EditorKind.EaseOfAccessNarrator,
                                    GlyphInfo.Narrator,
                                    headerDisplayName: Strings.VisionSettingHeader),

                                // Audio
                                new CategoryPageNavigationItem(
                                    Strings.AudioSettingName,
                                    EditorKind.EaseOfAccessAudio,
                                    GlyphInfo.Volume,
                                    headerDisplayName: Strings.AudioSettingName),
                                new CategoryPageNavigationItem(
                                    Strings.ClosedCaptionsSettingName,
                                    EditorKind.EaseOfAccessClosedCaptions,
                                    GlyphInfo.CC,
                                    headerDisplayName: Strings.AudioSettingName),

                                // Interaction
                                new CategoryPageNavigationItem(
                                    Strings.SpeechSettingName,
                                    EditorKind.EaseOfAccessSpeech,
                                    GlyphInfo.Microphone,
                                    headerDisplayName: Strings.InteractionSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.KeyboardSettingName,
                                    EditorKind.EaseOfAccessKeyboard,
                                    GlyphInfo.KeyboardClassic,
                                    headerDisplayName: Strings.InteractionSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.MouseSettingName,
                                    EditorKind.EaseOfAccessMouse,
                                    GlyphInfo.Mouse,
                                    headerDisplayName: Strings.InteractionSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.EyeControlSettingName,
                                    EditorKind.EaseOfAccessEyeControl,
                                    GlyphInfo.EyeGaze,
                                    headerDisplayName: Strings.InteractionSettingHeader),
                            }));

                case CategoryKind.Search:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.Search,
                            Strings.SearchCategoryName,
                            new[]
                            {
                                new CategoryPageNavigationItem(
                                    Strings.PermissionsAndHistorySettingName,
                                    EditorKind.SearchPermissionsAndHistory,
                                    GlyphInfo.SecureApp),
                                new CategoryPageNavigationItem(
                                    Strings.SearchingWindowsSettingName,
                                    EditorKind.SearchSearchingWindows,
                                    GlyphInfo.Zoom),
                                new CategoryPageNavigationItem(
                                    Strings.MoreDetailsSettingName,
                                    EditorKind.SearchMoreDetails,
                                    GlyphInfo.Info),
                            }));

                case CategoryKind.Cortana:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.Cortana,
                            Strings.CortanaCategoryName,
                            new[]
                            {
                                new CategoryPageNavigationItem(
                                    Strings.TalkToCortanaSettingName,
                                    EditorKind.CortanaTalkToCortana,
                                    GlyphInfo.Microphone),
                                new CategoryPageNavigationItem(
                                    Strings.PermissionsSettingName,
                                    EditorKind.CortanaPermissions,
                                    GlyphInfo.SecureApp),
                                new CategoryPageNavigationItem(
                                    Strings.MoreDetailsSettingName,
                                    EditorKind.CortanaMoreDetails,
                                    GlyphInfo.Info),
                            }));

                case CategoryKind.Privacy:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.Privacy,
                            Strings.PrivacyCategoryName,
                            new[]
                            {
                                // Windows permissions
                                new CategoryPageNavigationItem(
                                    Strings.GeneralSettingName,
                                    EditorKind.PrivacyGeneral,
                                    GlyphInfo.Lock,
                                    headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.SpeechSettingName,
                                    EditorKind.PrivacySpeech,
                                    GlyphInfo.Speech,
                                    headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.InkingAndTypingPersonalizationSettingName,
                                    EditorKind.PrivacyInkingAndTypingPersonalization,
                                    GlyphInfo.Trackers,
                                    headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.DiagnosticsAndFeedbackSettingName,
                                    EditorKind.PrivacyDiagnosticsAndFeedback,
                                    GlyphInfo.Feedback,
                                    headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.ActivityHistorySettingName,
                                    EditorKind.PrivacyActivityHistory,
                                    GlyphInfo.TaskView,
                                    headerDisplayName: Strings.WindowsPermissionsSettingHeader),

                                // App permissions
                                new CategoryPageNavigationItem(
                                    Strings.LocationSettingName,
                                    EditorKind.PrivacyLocation,
                                    GlyphInfo.MapPin,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.CameraSettingName,
                                    EditorKind.PrivacyCamera,
                                    GlyphInfo.Camera,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.MicrophoneSettingName,
                                    EditorKind.PrivacyMicrophone,
                                    GlyphInfo.Microphone,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.VoiceActivationSettingName,
                                    EditorKind.PrivacyVoiceActivation,
                                    GlyphInfo.MicrophoneListening,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.NotificationsSettingName,
                                    EditorKind.PrivacyNotifications,
                                    GlyphInfo.ActionCenter,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.AccountInfoSettingName,
                                    EditorKind.PrivacyAccountInfo,
                                    GlyphInfo.ContactInfo,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.ContactsSettingName,
                                    EditorKind.PrivacyContacts,
                                    GlyphInfo.People,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.CalendarSettingName,
                                    EditorKind.PrivacyCalendar,
                                    GlyphInfo.Calendar,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.PhoneCallsSettingName,
                                    EditorKind.PrivacyPhoneCalls,
                                    GlyphInfo.Phone,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.CallHistorySettingName,
                                    EditorKind.PrivacyCallHistory,
                                    GlyphInfo.History,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.EmailSettingName,
                                    EditorKind.PrivacyEmail,
                                    GlyphInfo.Mail,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.TasksSettingName,
                                    EditorKind.PrivacyTasks,
                                    GlyphInfo.Trackers,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.MessagingSettingName,
                                    EditorKind.PrivacyMessaging,
                                    GlyphInfo.Message,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.RadiosSettingName,
                                    EditorKind.PrivacyRadios,
                                    GlyphInfo.NetworkTower,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.OtherDevicesSettingName,
                                    EditorKind.PrivacyOtherDevices,
                                    GlyphInfo.Devices2,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.BackgroundAppsSettingName,
                                    EditorKind.PrivacyBackgroundApps,
                                    GlyphInfo.Diagnostic,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.AppDiagnosticsSettingName,
                                    EditorKind.PrivacyAppDiagnostics,
                                    GlyphInfo.AreaChart,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.AutomaticFileDownloadsSettingName,
                                    EditorKind.PrivacyAutomaticFileDownloads,
                                    GlyphInfo.Cloud,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.DocumentsSettingName,
                                    EditorKind.PrivacyDocuments,
                                    GlyphInfo.Page,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.PicturesSettingName,
                                    EditorKind.PrivacyPictures,
                                    GlyphInfo.Photo2,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.VideosSettingName,
                                    EditorKind.PrivacyVideos,
                                    GlyphInfo.Movies,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                                new CategoryPageNavigationItem(
                                    Strings.FileSystemSettingName,
                                    EditorKind.PrivacyFileSystem,
                                    GlyphInfo.Page,
                                    headerDisplayName: Strings.AppPermissionsSettingHeader),
                            }));

                case CategoryKind.UpdateAndSecurity:
                    return new CategoryPageViewModel(
                        logger,
                        serviceLocator,
                        new CategoryPageNavigationViewModel(
                            serviceLocator.NavigationViewService,
                            CategoryKind.UpdateAndSecurity,
                            Strings.UpdateAndSecurityCategoryName,
                            new[]
                            {
                                new CategoryPageNavigationItem(
                                    Strings.WindowsUpdateSettingName,
                                    EditorKind.UpdateAndSecurityWindowsUpdate,
                                    GlyphInfo.Sync),
                                new CategoryPageNavigationItem(
                                    Strings.DeliveryOptimizationSettingName,
                                    EditorKind.UpdateAndSecurityDeliveryOptimization,
                                    GlyphInfo.DeliveryOptimization),
                                new CategoryPageNavigationItem(
                                    Strings.WindowsSecuritySettingName,
                                    EditorKind.UpdateAndSecurityWindowsSecurity,
                                    GlyphInfo.DefenderApp),
                                new CategoryPageNavigationItem(
                                    Strings.BackupSettingName,
                                    EditorKind.UpdateAndSecurityBackup,
                                    GlyphInfo.Upload),
                                new CategoryPageNavigationItem(
                                    Strings.TroubleshootSettingName,
                                    EditorKind.UpdateAndSecurityTroubleshoot,
                                    GlyphInfo.Repair),
                                new CategoryPageNavigationItem(
                                    Strings.RecoverySettingName,
                                    EditorKind.UpdateAndSecurityRecovery,
                                    GlyphInfo.ResetDrive),
                                new CategoryPageNavigationItem(
                                    Strings.ActivationSettingName,
                                    EditorKind.UpdateAndSecurityActivation,
                                    GlyphInfo.Completed),
                                new CategoryPageNavigationItem(
                                    Strings.FindMyDeviceSettingName,
                                    EditorKind.UpdateAndSecurityFindMyDevice,
                                    GlyphInfo.MapPin),
                                new CategoryPageNavigationItem(
                                    Strings.ForDevelopersSettingName,
                                    EditorKind.UpdateAndSecurityForDevelopers,
                                    GlyphInfo.DeveloperTools),
                                new CategoryPageNavigationItem(
                                    Strings.WindowsInsiderProgramSettingName,
                                    EditorKind.UpdateAndSecurityWindowsInsiderProgram,
                                    GlyphInfo.WindowsInsider),
                            }));

                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, null);
            }
        }

        private void OnNavigationPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Navigation.SelectedItem))
            {
                return;
            }

            CurrentEditor = Navigation.SelectedItem == null
                ? null
                : EditorViewModelFactory.CreateFromKind(Navigation.SelectedItem.EditorKind, _logger, _serviceLocator);
            CurrentEditor?.LoadAsync();
        }
    }
}
