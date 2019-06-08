// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsGroupPageViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Utility;
    using ViewServices;

    /// <summary>
    /// Represents a grouping of individual setting pages (System, Devices, etc.) that are listed on the home page.
    /// </summary>
    public class SettingsGroupPageViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private SettingsGroupPageViewModel(
            INavigationViewService navigationViewService,
            SettingGroupKind groupKind,
            string groupName,
            IEnumerable<SettingNavigationInfoViewModel> settings = null)
        {
            Param.VerifyNotNull(navigationViewService, nameof(navigationViewService));

            GroupKind = groupKind;
            GroupName = Param.VerifyString(groupName, nameof(groupName));
            Settings = new ReadOnlyCollection<SettingNavigationInfoViewModel>(
                settings?.ToList() ?? new List<SettingNavigationInfoViewModel>());

            IsGrouped = Settings.Any(vm => vm.HeaderDisplayName != null);
            GroupedSettings = Settings.GroupBy(
                setting => setting.HeaderDisplayName,
                (key, items) => new SettingNavigationInfoGroupViewModel(key, items));

            HomeCommand = new RelayCommand(() => navigationViewService.NavigateTo(typeof(HomePageViewModel), null));
        }

        //// ===========================================================================================================
        //// Commands
        //// ===========================================================================================================

        public ICommand HomeCommand { get; }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public SettingGroupKind GroupKind { get; }
        public string GroupName { get; }
        public ReadOnlyCollection<SettingNavigationInfoViewModel> Settings { get; }

        public bool IsGrouped { get; }
        public IEnumerable<SettingNavigationInfoGroupViewModel> GroupedSettings { get; }

        public SettingNavigationInfoViewModel SelectedItem
        {
            get => Settings.First(setting => setting.IsSelected);
            set
            {
                SettingNavigationInfoViewModel selectedItem = SelectedItem;
                if (selectedItem != value)
                {
                    selectedItem.IsSelected = false;
                    value.IsSelected = true;
                    OnPropertyChanged();
                }
            }
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Creates a new <see cref="SettingsGroupPageViewModel"/> corresponding to the specified <see
        /// cref="SettingGroupKind"/>. Called from the view.
        /// </summary>
        public static SettingsGroupPageViewModel CreateFromGroupKind(
            SettingGroupKind groupKind,
            INavigationViewService navigationViewService)
        {
            switch (groupKind)
            {
                case SettingGroupKind.System:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.System,
                        Strings.SystemGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.DisplaySettingName,
                                SettingsEditorKind.SystemDisplay,
                                GlyphInfo.TvMonitor,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.SoundSettingName,
                                SettingsEditorKind.SystemSound,
                                GlyphInfo.Volume),
                            new SettingNavigationInfoViewModel(
                                Strings.NotificationsAndActionsSettingName,
                                SettingsEditorKind.SystemNotificationsAndActions,
                                GlyphInfo.Message),
                            new SettingNavigationInfoViewModel(
                                Strings.FocusAssistSettingName,
                                SettingsEditorKind.SystemFocusAssist,
                                GlyphInfo.QuietHours),
                            new SettingNavigationInfoViewModel(
                                Strings.PowerAndSleepSettingName,
                                SettingsEditorKind.SystemPowerAndSleep,
                                GlyphInfo.PowerButton),
                            new SettingNavigationInfoViewModel(
                                Strings.StorageSettingName,
                                SettingsEditorKind.SystemStorage,
                                GlyphInfo.HardDrive),
                            new SettingNavigationInfoViewModel(
                                Strings.TabletModeSettingName,
                                SettingsEditorKind.SystemTabletMode,
                                GlyphInfo.TabletMode),
                            new SettingNavigationInfoViewModel(
                                Strings.MultitaskingSettingName,
                                SettingsEditorKind.SystemMultitasking,
                                GlyphInfo.TaskView),
                            new SettingNavigationInfoViewModel(
                                Strings.ProjectingToThisPCSettingName,
                                SettingsEditorKind.SystemProjectingToThisPC,
                                GlyphInfo.Project),
                            new SettingNavigationInfoViewModel(
                                Strings.SharedExperiencesSettingName,
                                SettingsEditorKind.SystemSharedExperiences,
                                GlyphInfo.Connected),
                            new SettingNavigationInfoViewModel(
                                Strings.ClipboardSettingName,
                                SettingsEditorKind.SystemClipboard,
                                GlyphInfo.Paste),
                            new SettingNavigationInfoViewModel(
                                Strings.RemoteDesktopSettingName,
                                SettingsEditorKind.SystemRemoteDesktop,
                                GlyphInfo.Remote),
                            new SettingNavigationInfoViewModel(
                                Strings.AboutSettingName,
                                SettingsEditorKind.SystemAbout,
                                GlyphInfo.Info),
                        });

                case SettingGroupKind.Devices:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.Devices,
                        Strings.DevicesGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.BluetoothAndOtherDevicesSettingName,
                                SettingsEditorKind.DevicesBluetoothAndOtherDevices,
                                GlyphInfo.Devices,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.PrintersAndScannersSettingName,
                                SettingsEditorKind.DevicesPrintersAndScanners,
                                GlyphInfo.Print),
                            new SettingNavigationInfoViewModel(
                                Strings.MouseSettingName,
                                SettingsEditorKind.DevicesMouse,
                                GlyphInfo.Mouse),
                            new SettingNavigationInfoViewModel(
                                Strings.TypingSettingName,
                                SettingsEditorKind.DevicesTyping,
                                GlyphInfo.KeyboardClassic),
                            new SettingNavigationInfoViewModel(
                                Strings.PenAndWindowsInkSettingName,
                                SettingsEditorKind.DevicesPenAndWindowsInk,
                                GlyphInfo.PenWorkspace),
                            new SettingNavigationInfoViewModel(
                                Strings.AutoPlaySettingName,
                                SettingsEditorKind.DevicesAutoPlay,
                                GlyphInfo.PlaybackRate1x),
                            new SettingNavigationInfoViewModel(
                                Strings.UsbSettingName,
                                SettingsEditorKind.DevicesUsb,
                                GlyphInfo.Usb),
                        });

                case SettingGroupKind.Phone:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.Phone,
                        Strings.PhoneGroupName);

                case SettingGroupKind.NetworkAndInternet:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.NetworkAndInternet,
                        Strings.NetworkAndInternetGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.NetworkStatusSettingName,
                                SettingsEditorKind.NetworkAndInternetNetworkStatus,
                                GlyphInfo.MyNetwork,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.EthernetSettingName,
                                SettingsEditorKind.NetworkAndInternetEthernet,
                                GlyphInfo.Ethernet),
                            new SettingNavigationInfoViewModel(
                                Strings.DialUpSettingName,
                                SettingsEditorKind.NetworkAndInternetDialUp,
                                GlyphInfo.DialUp),
                            new SettingNavigationInfoViewModel(
                                Strings.VpnSettingName,
                                SettingsEditorKind.NetworkAndInternetVpn,
                                GlyphInfo.Vpn),
                            new SettingNavigationInfoViewModel(
                                Strings.DataUsageSettingName,
                                SettingsEditorKind.NetworkAndInternetDataUsage,
                                GlyphInfo.PieSingle),
                            new SettingNavigationInfoViewModel(
                                Strings.ProxySettingName,
                                SettingsEditorKind.NetworkAndInternetProxy,
                                GlyphInfo.Globe),
                        });

                case SettingGroupKind.Personalization:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.Personalization,
                        Strings.PersonalizationGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.BackgroundSettingName,
                                SettingsEditorKind.PersonalizationBackground,
                                GlyphInfo.Photo2,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.ColorsSettingName,
                                SettingsEditorKind.PersonalizationColors,
                                GlyphInfo.Color),
                            new SettingNavigationInfoViewModel(
                                Strings.LockScreenSettingName,
                                SettingsEditorKind.PersonalizationLockScreen,
                                GlyphInfo.LockScreenDesktop),
                            new SettingNavigationInfoViewModel(
                                Strings.ThemesSettingName,
                                SettingsEditorKind.PersonalizationThemes,
                                GlyphInfo.Personalize),
                            new SettingNavigationInfoViewModel(
                                Strings.FontsSettingName,
                                SettingsEditorKind.PersonalizationFonts,
                                GlyphInfo.Font),
                            new SettingNavigationInfoViewModel(
                                Strings.StartSettingName,
                                SettingsEditorKind.PersonalizationStart,
                                GlyphInfo.Tiles),
                            new SettingNavigationInfoViewModel(
                                Strings.TaskbarSettingName,
                                SettingsEditorKind.PersonalizationTaskbar,
                                GlyphInfo.DockBottom),
                        });

                case SettingGroupKind.Apps:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.Apps,
                        Strings.AppsGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.AppsAndFeaturesSettingName,
                                SettingsEditorKind.AppsAppsAndFeatures,
                                GlyphInfo.AllApps,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.DefaultAppsSettingName,
                                SettingsEditorKind.AppsDefaultApps,
                                GlyphInfo.OpenWith),
                            new SettingNavigationInfoViewModel(
                                Strings.OfflineMapsSettingName,
                                SettingsEditorKind.AppsOfflineMaps,
                                GlyphInfo.DownloadMap),
                            new SettingNavigationInfoViewModel(
                                Strings.AppsForWebsitesSettingName,
                                SettingsEditorKind.AppsAppsForWebsites,
                                GlyphInfo.NewWindow),
                            new SettingNavigationInfoViewModel(
                                Strings.VideoPlaybackSettingName,
                                SettingsEditorKind.AppsVideoPlayback,
                                GlyphInfo.Video),
                            new SettingNavigationInfoViewModel(
                                Strings.StartupSettingName,
                                SettingsEditorKind.AppsStartup,
                                GlyphInfo.SetLockScreen),
                        });

                case SettingGroupKind.Accounts:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.Accounts,
                        Strings.AccountsGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.YourInfoSettingName,
                                SettingsEditorKind.AccountsYourInfo,
                                GlyphInfo.ContactInfo,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.EmailAndAccountsSettingName,
                                SettingsEditorKind.AccountsEmailAndAccounts,
                                GlyphInfo.Mail),
                            new SettingNavigationInfoViewModel(
                                Strings.SignInOptionsSettingName,
                                SettingsEditorKind.AccountsSignInOptions,
                                GlyphInfo.Permissions),
                            new SettingNavigationInfoViewModel(
                                Strings.AccessWorkOrSchoolSettingName,
                                SettingsEditorKind.AccountsAccessWorkOrSchool,
                                GlyphInfo.Work),
                            new SettingNavigationInfoViewModel(
                                Strings.FamilyAndOtherUsersSettingName,
                                SettingsEditorKind.AccountsFamilyAndOtherUsers,
                                GlyphInfo.AddFriend),
                            new SettingNavigationInfoViewModel(
                                Strings.SyncYourSettingsSettingName,
                                SettingsEditorKind.AccountsSyncYourSettings,
                                GlyphInfo.Sync),
                        });

                case SettingGroupKind.TimeAndLanguage:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.TimeAndLanguage,
                        Strings.TimeAndLanguageGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.DateAndTimeSettingName,
                                SettingsEditorKind.TimeAndLanguageDateAndTime,
                                GlyphInfo.DateTime,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.RegionSettingName,
                                SettingsEditorKind.TimeAndLanguageRegion,
                                GlyphInfo.Globe2),
                            new SettingNavigationInfoViewModel(
                                Strings.LanguageSettingName,
                                SettingsEditorKind.TimeAndLanguageLanguage,
                                GlyphInfo.Characters),
                            new SettingNavigationInfoViewModel(
                                Strings.SpeechSettingName,
                                SettingsEditorKind.TimeAndLanguageSpeech,
                                GlyphInfo.Microphone),
                        });

                case SettingGroupKind.Gaming:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.Gaming,
                        Strings.GamingGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.GameBarSettingName,
                                SettingsEditorKind.GamingGameBar,
                                GlyphInfo.GameBar,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.CapturesSettingName,
                                SettingsEditorKind.GamingCaptures,
                                GlyphInfo.Captures),
                            new SettingNavigationInfoViewModel(
                                Strings.BroadcastingSettingName,
                                SettingsEditorKind.GamingBroadcasting,
                                GlyphInfo.SatelliteDish),
                            new SettingNavigationInfoViewModel(
                                Strings.GameModeSettingName,
                                SettingsEditorKind.GamingGameMode,
                                GlyphInfo.SpeedHigh),
                            new SettingNavigationInfoViewModel(
                                Strings.XboxNetworkingSettingName,
                                SettingsEditorKind.GamingXboxNetworking,
                                GlyphInfo.XboxLogo),
                        });

                case SettingGroupKind.EaseOfAccess:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.EaseOfAccess,
                        Strings.EaseOfAccessGroupName,
                        new[]
                        {
                            // Vision
                            new SettingNavigationInfoViewModel(
                                Strings.DisplaySettingName,
                                SettingsEditorKind.EaseOfAccessDisplay,
                                GlyphInfo.TvMonitor,
                                isSelected: true,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.CursorAndPointerSettingName,
                                SettingsEditorKind.EaseOfAccessCursorAndPointer,
                                GlyphInfo.TouchPointer,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.MagnifierSettingName,
                                SettingsEditorKind.EaseOfAccessMagnifier,
                                GlyphInfo.ZoomIn,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.ColorFiltersSettingName,
                                SettingsEditorKind.EaseOfAccessColorFilters,
                                GlyphInfo.Color,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.HighContrastSettingName,
                                SettingsEditorKind.EaseOfAccessHighContrast,
                                GlyphInfo.Brightness,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.NarratorSettingName,
                                SettingsEditorKind.EaseOfAccessNarrator,
                                GlyphInfo.Narrator,
                                headerDisplayName: Strings.VisionSettingHeader),

                            // Audio
                            new SettingNavigationInfoViewModel(
                                Strings.AudioSettingName,
                                SettingsEditorKind.EaseOfAccessAudio,
                                GlyphInfo.Volume,
                                headerDisplayName: Strings.AudioSettingName),
                            new SettingNavigationInfoViewModel(
                                Strings.ClosedCaptionsSettingName,
                                SettingsEditorKind.EaseOfAccessClosedCaptions,
                                GlyphInfo.CC,
                                headerDisplayName: Strings.AudioSettingName),

                            // Interaction
                            new SettingNavigationInfoViewModel(
                                Strings.SpeechSettingName,
                                SettingsEditorKind.EaseOfAccessSpeech,
                                GlyphInfo.Microphone,
                                headerDisplayName: Strings.InteractionSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.KeyboardSettingName,
                                SettingsEditorKind.EaseOfAccessKeyboard,
                                GlyphInfo.KeyboardClassic,
                                headerDisplayName: Strings.InteractionSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.MouseSettingName,
                                SettingsEditorKind.EaseOfAccessMouse,
                                GlyphInfo.Mouse,
                                headerDisplayName: Strings.InteractionSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.EyeControlSettingName,
                                SettingsEditorKind.EaseOfAccessEyeControl,
                                GlyphInfo.EyeGaze,
                                headerDisplayName: Strings.InteractionSettingHeader),
                        });

                case SettingGroupKind.Search:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.Search,
                        Strings.SearchGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.PermissionsAndHistorySettingName,
                                SettingsEditorKind.SearchPermissionsAndHistory,
                                GlyphInfo.SecureApp,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.SearchingWindowsSettingName,
                                SettingsEditorKind.SearchSearchingWindows,
                                GlyphInfo.Zoom),
                            new SettingNavigationInfoViewModel(
                                Strings.MoreDetailsSettingName,
                                SettingsEditorKind.SearchMoreDetails,
                                GlyphInfo.Info),
                        });

                case SettingGroupKind.Cortana:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.Cortana,
                        Strings.CortanaGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.TalkToCortanaSettingName,
                                SettingsEditorKind.CortanaTalkToCortana,
                                GlyphInfo.Microphone,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.PermissionsSettingName,
                                SettingsEditorKind.CortanaPermissions,
                                GlyphInfo.SecureApp),
                            new SettingNavigationInfoViewModel(
                                Strings.MoreDetailsSettingName,
                                SettingsEditorKind.CortanaMoreDetails,
                                GlyphInfo.Info),
                        });

                case SettingGroupKind.Privacy:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.Privacy,
                        Strings.PrivacyGroupName,
                        new[]
                        {
                            // Windows permissions
                            new SettingNavigationInfoViewModel(
                                Strings.GeneralSettingName,
                                SettingsEditorKind.PrivacyGeneral,
                                GlyphInfo.Lock,
                                isSelected: true,
                                headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.SpeechSettingName,
                                SettingsEditorKind.PrivacySpeech,
                                GlyphInfo.Speech,
                                headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.InkingAndTypingPersonalizationSettingName,
                                SettingsEditorKind.PrivacyInkingAndTypingPersonalization,
                                GlyphInfo.Trackers,
                                headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.DiagnosticsAndFeedbackSettingName,
                                SettingsEditorKind.PrivacyDiagnosticsAndFeedback,
                                GlyphInfo.Feedback,
                                headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.ActivityHistorySettingName,
                                SettingsEditorKind.PrivacyActivityHistory,
                                GlyphInfo.TaskView,
                                headerDisplayName: Strings.WindowsPermissionsSettingHeader),

                            // App permissions
                            new SettingNavigationInfoViewModel(
                                Strings.LocationSettingName,
                                SettingsEditorKind.PrivacyLocation,
                                GlyphInfo.MapPin,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.CameraSettingName,
                                SettingsEditorKind.PrivacyCamera,
                                GlyphInfo.Camera,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.MicrophoneSettingName,
                                SettingsEditorKind.PrivacyMicrophone,
                                GlyphInfo.Microphone,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.VoiceActivationSettingName,
                                SettingsEditorKind.PrivacyVoiceActivation,
                                GlyphInfo.MicrophoneListening,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.NotificationsSettingName,
                                SettingsEditorKind.PrivacyNotifications,
                                GlyphInfo.ActionCenter,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.AccountInfoSettingName,
                                SettingsEditorKind.PrivacyAccountInfo,
                                GlyphInfo.ContactInfo,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.ContactsSettingName,
                                SettingsEditorKind.PrivacyContacts,
                                GlyphInfo.People,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.CalendarSettingName,
                                SettingsEditorKind.PrivacyCalendar,
                                GlyphInfo.Calendar,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.PhoneCallsSettingName,
                                SettingsEditorKind.PrivacyPhoneCalls,
                                GlyphInfo.Phone,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.CallHistorySettingName,
                                SettingsEditorKind.PrivacyCallHistory,
                                GlyphInfo.History,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.EmailSettingName,
                                SettingsEditorKind.PrivacyEmail,
                                GlyphInfo.Mail,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.TasksSettingName,
                                SettingsEditorKind.PrivacyTasks,
                                GlyphInfo.Trackers,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.MessagingSettingName,
                                SettingsEditorKind.PrivacyMessaging,
                                GlyphInfo.Message,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.RadiosSettingName,
                                SettingsEditorKind.PrivacyRadios,
                                GlyphInfo.NetworkTower,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.OtherDevicesSettingName,
                                SettingsEditorKind.PrivacyOtherDevices,
                                GlyphInfo.Devices2,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.BackgroundAppsSettingName,
                                SettingsEditorKind.PrivacyBackgroundApps,
                                GlyphInfo.Diagnostic,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.AppDiagnosticsSettingName,
                                SettingsEditorKind.PrivacyAppDiagnostics,
                                GlyphInfo.AreaChart,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.AutomaticFileDownloadsSettingName,
                                SettingsEditorKind.PrivacyAutomaticFileDownloads,
                                GlyphInfo.Cloud,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.DocumentsSettingName,
                                SettingsEditorKind.PrivacyDocuments,
                                GlyphInfo.Page,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.PicturesSettingName,
                                SettingsEditorKind.PrivacyPictures,
                                GlyphInfo.Photo2,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.VideosSettingName,
                                SettingsEditorKind.PrivacyVideos,
                                GlyphInfo.Movies,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.FileSystemSettingName,
                                SettingsEditorKind.PrivacyFileSystem,
                                GlyphInfo.Page,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                        });

                case SettingGroupKind.UpdateAndSecurity:
                    return new SettingsGroupPageViewModel(
                        navigationViewService,
                        SettingGroupKind.UpdateAndSecurity,
                        Strings.UpdateAndSecurityGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.WindowsUpdateSettingName,
                                SettingsEditorKind.UpdateAndSecurityWindowsUpdate,
                                GlyphInfo.Sync,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.DeliveryOptimizationSettingName,
                                SettingsEditorKind.UpdateAndSecurityDeliveryOptimization,
                                GlyphInfo.DeliveryOptimization),
                            new SettingNavigationInfoViewModel(
                                Strings.WindowsSecuritySettingName,
                                SettingsEditorKind.UpdateAndSecurityWindowsSecurity,
                                GlyphInfo.DefenderApp),
                            new SettingNavigationInfoViewModel(
                                Strings.BackupSettingName,
                                SettingsEditorKind.UpdateAndSecurityBackup,
                                GlyphInfo.Upload),
                            new SettingNavigationInfoViewModel(
                                Strings.TroubleshootSettingName,
                                SettingsEditorKind.UpdateAndSecurityTroubleshoot,
                                GlyphInfo.Repair),
                            new SettingNavigationInfoViewModel(
                                Strings.RecoverySettingName,
                                SettingsEditorKind.UpdateAndSecurityRecovery,
                                GlyphInfo.ResetDrive),
                            new SettingNavigationInfoViewModel(
                                Strings.ActivationSettingName,
                                SettingsEditorKind.UpdateAndSecurityActivation,
                                GlyphInfo.Completed),
                            new SettingNavigationInfoViewModel(
                                Strings.FindMyDeviceSettingName,
                                SettingsEditorKind.UpdateAndSecurityFindMyDevice,
                                GlyphInfo.MapPin),
                            new SettingNavigationInfoViewModel(
                                Strings.ForDevelopersSettingName,
                                SettingsEditorKind.UpdateAndSecurityForDevelopers,
                                GlyphInfo.DeveloperTools),
                            new SettingNavigationInfoViewModel(
                                Strings.WindowsInsiderProgramSettingName,
                                SettingsEditorKind.UpdateAndSecurityWindowsInsiderProgram,
                                GlyphInfo.WindowsInsider),
                        });

                default:
                    throw new ArgumentOutOfRangeException(nameof(groupKind), groupKind, null);
            }
        }
    }
}
