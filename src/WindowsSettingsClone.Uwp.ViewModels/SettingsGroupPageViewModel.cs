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
    using Utility;

    /// <summary>
    /// Represents a grouping of individual setting pages (System, Devices, etc.) that are listed on the home page.
    /// </summary>
    public class SettingsGroupPageViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private SettingsGroupPageViewModel(
            SettingGroupKind groupKind,
            string groupName,
            IEnumerable<SettingNavigationInfoViewModel> settings = null)
        {
            GroupKind = groupKind;
            GroupName = Param.VerifyString(groupName, nameof(groupName));
            Settings = new ReadOnlyCollection<SettingNavigationInfoViewModel>(
                settings?.ToList() ?? new List<SettingNavigationInfoViewModel>());

            IsGrouped = Settings.Any(vm => vm.HeaderDisplayName != null);
            GroupedSettings = Settings.GroupBy(
                setting => setting.HeaderDisplayName,
                (key, items) => new SettingNavigationInfoGroupViewModel(key, items));
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public SettingGroupKind GroupKind { get; }
        public string GroupName { get; }
        public ReadOnlyCollection<SettingNavigationInfoViewModel> Settings { get; }

        public bool IsGrouped { get; }
        public IEnumerable<SettingNavigationInfoGroupViewModel> GroupedSettings { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Creates a new <see cref="SettingsGroupPageViewModel"/> corresponding to the specified <see
        /// cref="SettingGroupKind"/>. Called from the view.
        /// </summary>
        public static SettingsGroupPageViewModel CreateFromGroupKind(SettingGroupKind groupKind)
        {
            switch (groupKind)
            {
                case SettingGroupKind.System:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.System,
                        Strings.SystemGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.DisplaySettingName,
                                SettingEditorKind.SystemDisplay,
                                GlyphInfo.TvMonitor,
                                isSelected : true),
                            new SettingNavigationInfoViewModel(Strings.SoundSettingName,
                                SettingEditorKind.SystemSound,
                                GlyphInfo.Volume),
                            new SettingNavigationInfoViewModel(Strings.NotificationsAndActionsSettingName,
                                SettingEditorKind.SystemNotificationsAndActions,
                                GlyphInfo.Message),
                            new SettingNavigationInfoViewModel(Strings.FocusAssistSettingName,
                                SettingEditorKind.SystemFocusAssist,
                                GlyphInfo.QuietHours),
                            new SettingNavigationInfoViewModel(Strings.PowerAndSleepSettingName,
                                SettingEditorKind.SystemPowerAndSleep,
                                GlyphInfo.PowerButton),
                            new SettingNavigationInfoViewModel(Strings.StorageSettingName,
                                SettingEditorKind.SystemStorage,
                                GlyphInfo.HardDrive),
                            new SettingNavigationInfoViewModel(Strings.TabletModeSettingName,
                                SettingEditorKind.SystemTabletMode,
                                GlyphInfo.TabletMode),
                            new SettingNavigationInfoViewModel(Strings.MultitaskingSettingName,
                                SettingEditorKind.SystemMultitasking,
                                GlyphInfo.TaskView),
                            new SettingNavigationInfoViewModel(Strings.ProjectingToThisPCSettingName,
                                SettingEditorKind.SystemProjectingToThisPC,
                                GlyphInfo.Project),
                            new SettingNavigationInfoViewModel(Strings.SharedExperiencesSettingName,
                                SettingEditorKind.SystemSharedExperiences,
                                GlyphInfo.Connected),
                            new SettingNavigationInfoViewModel(Strings.ClipboardSettingName,
                                SettingEditorKind.SystemClipboard,
                                GlyphInfo.Paste),
                            new SettingNavigationInfoViewModel(Strings.RemoteDesktopSettingName,
                                SettingEditorKind.SystemRemoteDesktop,
                                GlyphInfo.Remote),
                            new SettingNavigationInfoViewModel(Strings.AboutSettingName,
                                SettingEditorKind.SystemAbout,
                                GlyphInfo.Info),
                        });

                case SettingGroupKind.Devices:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Devices,
                        Strings.DevicesGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.BluetoothAndOtherDevicesSettingName,
                                SettingEditorKind.DevicesBluetoothAndOtherDevices,
                                GlyphInfo.Devices,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.PrintersAndScannersSettingName,
                                SettingEditorKind.DevicesPrintersAndScanners,
                                GlyphInfo.Print),
                            new SettingNavigationInfoViewModel(Strings.MouseSettingName,
                                SettingEditorKind.DevicesMouse,
                                GlyphInfo.Mouse),
                            new SettingNavigationInfoViewModel(Strings.TypingSettingName,
                                SettingEditorKind.DevicesTyping,
                                GlyphInfo.KeyboardClassic),
                            new SettingNavigationInfoViewModel(Strings.PenAndWindowsInkSettingName,
                                SettingEditorKind.DevicesPenAndWindowsInk,
                                GlyphInfo.PenWorkspace),
                            new SettingNavigationInfoViewModel(Strings.AutoPlaySettingName,
                                SettingEditorKind.DevicesAutoPlay,
                                GlyphInfo.PlaybackRate1x),
                            new SettingNavigationInfoViewModel(Strings.UsbSettingName,
                                SettingEditorKind.DevicesUsb,
                                GlyphInfo.Usb),
                        });

                case SettingGroupKind.Phone:
                    return new SettingsGroupPageViewModel(SettingGroupKind.Phone, Strings.PhoneGroupName);

                case SettingGroupKind.NetworkAndInternet:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.NetworkAndInternet,
                        Strings.NetworkAndInternetGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(Strings.NetworkStatusSettingName,
                                SettingEditorKind.NetworkAndInternetNetworkStatus,
                                GlyphInfo.MyNetwork,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.EthernetSettingName,
                                SettingEditorKind.NetworkAndInternetEthernet,
                                GlyphInfo.Ethernet),
                            new SettingNavigationInfoViewModel(Strings.DialUpSettingName,
                                SettingEditorKind.NetworkAndInternetDialUp,
                                GlyphInfo.DialUp),
                            new SettingNavigationInfoViewModel(Strings.VpnSettingName,
                                SettingEditorKind.NetworkAndInternetVpn,
                                GlyphInfo.Vpn),
                            new SettingNavigationInfoViewModel(Strings.DataUsageSettingName,
                                SettingEditorKind.NetworkAndInternetDataUsage,
                                GlyphInfo.PieSingle),
                            new SettingNavigationInfoViewModel(Strings.ProxySettingName,
                                SettingEditorKind.NetworkAndInternetProxy,
                                GlyphInfo.Globe),
                        });

                case SettingGroupKind.Personalization:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Personalization,
                        Strings.PersonalizationGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.BackgroundSettingName,
                                SettingEditorKind.PersonalizationBackground,
                                GlyphInfo.Photo2,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.ColorsSettingName,
                                SettingEditorKind.PersonalizationColors,
                                GlyphInfo.Color),
                            new SettingNavigationInfoViewModel(Strings.LockScreenSettingName,
                                SettingEditorKind.PersonalizationLockScreen,
                                GlyphInfo.LockScreenDesktop),
                            new SettingNavigationInfoViewModel(Strings.ThemesSettingName,
                                SettingEditorKind.PersonalizationThemes,
                                GlyphInfo.Personalize),
                            new SettingNavigationInfoViewModel(Strings.FontsSettingName,
                                SettingEditorKind.PersonalizationFonts,
                                GlyphInfo.Font),
                            new SettingNavigationInfoViewModel(Strings.StartSettingName,
                                SettingEditorKind.PersonalizationStart,
                                GlyphInfo.Tiles),
                            new SettingNavigationInfoViewModel(Strings.TaskbarSettingName,
                                SettingEditorKind.PersonalizationTaskbar,
                                GlyphInfo.DockBottom),
                        });

                case SettingGroupKind.Apps:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Apps,
                        Strings.AppsGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.AppsAndFeaturesSettingName,
                                SettingEditorKind.AppsAppsAndFeatures,
                                GlyphInfo.AllApps,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.DefaultAppsSettingName,
                                SettingEditorKind.AppsDefaultApps,
                                GlyphInfo.OpenWith),
                            new SettingNavigationInfoViewModel(Strings.OfflineMapsSettingName,
                                SettingEditorKind.AppsOfflineMaps,
                                GlyphInfo.DownloadMap),
                            new SettingNavigationInfoViewModel(Strings.AppsForWebsitesSettingName,
                                SettingEditorKind.AppsAppsForWebsites,
                                GlyphInfo.NewWindow),
                            new SettingNavigationInfoViewModel(Strings.VideoPlaybackSettingName,
                                SettingEditorKind.AppsVideoPlayback,
                                GlyphInfo.Video),
                            new SettingNavigationInfoViewModel(Strings.StartupSettingName,
                                SettingEditorKind.AppsStartup,
                                GlyphInfo.SetLockScreen),
                        });

                case SettingGroupKind.Accounts:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Accounts,
                        Strings.AccountsGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.YourInfoSettingName,
                                SettingEditorKind.AccountsYourInfo,
                                GlyphInfo.ContactInfo,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.EmailAndAccountsSettingName,
                                SettingEditorKind.AccountsEmailAndAccounts,
                                GlyphInfo.Mail),
                            new SettingNavigationInfoViewModel(Strings.SignInOptionsSettingName,
                                SettingEditorKind.AccountsSignInOptions,
                                GlyphInfo.Permissions),
                            new SettingNavigationInfoViewModel(Strings.AccessWorkOrSchoolSettingName,
                                SettingEditorKind.AccountsAccessWorkOrSchool,
                                GlyphInfo.Work),
                            new SettingNavigationInfoViewModel(Strings.FamilyAndOtherUsersSettingName,
                                SettingEditorKind.AccountsFamilyAndOtherUsers,
                                GlyphInfo.AddFriend),
                            new SettingNavigationInfoViewModel(Strings.SyncYourSettingsSettingName,
                                SettingEditorKind.AccountsSyncYourSettings,
                                GlyphInfo.Sync),
                        });

                case SettingGroupKind.TimeAndLanguage:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.TimeAndLanguage,
                        Strings.TimeAndLanguageGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.DateAndTimeSettingName,
                                SettingEditorKind.TimeAndLanguageDateAndTime,
                                GlyphInfo.DateTime,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.RegionSettingName,
                                SettingEditorKind.TimeAndLanguageRegion,
                                GlyphInfo.Globe2),
                            new SettingNavigationInfoViewModel(Strings.LanguageSettingName,
                                SettingEditorKind.TimeAndLanguageLanguage,
                                GlyphInfo.Characters),
                            new SettingNavigationInfoViewModel(Strings.SpeechSettingName,
                                SettingEditorKind.TimeAndLanguageSpeech,
                                GlyphInfo.Microphone),
                        });

                case SettingGroupKind.Gaming:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Gaming,
                        Strings.GamingGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.GameBarSettingName,
                                SettingEditorKind.GamingGameBar,
                                GlyphInfo.GameBar,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.CapturesSettingName,
                                SettingEditorKind.GamingCaptures,
                                GlyphInfo.Captures),
                            new SettingNavigationInfoViewModel(Strings.BroadcastingSettingName,
                                SettingEditorKind.GamingBroadcasting,
                                GlyphInfo.SatelliteDish),
                            new SettingNavigationInfoViewModel(Strings.GameModeSettingName,
                                SettingEditorKind.GamingGameMode,
                                GlyphInfo.SpeedHigh),
                            new SettingNavigationInfoViewModel(Strings.XboxNetworkingSettingName,
                                SettingEditorKind.GamingXboxNetworking,
                                GlyphInfo.XboxLogo),
                        });

                case SettingGroupKind.EaseOfAccess:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.EaseOfAccess,
                        Strings.EaseOfAccessGroupName,
                        new[]
                        {
                            // Vision
                            new SettingNavigationInfoViewModel(
                                Strings.DisplaySettingName,
                                SettingEditorKind.EaseOfAccessDisplay,
                                GlyphInfo.TvMonitor,
                                isSelected: true,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.CursorAndPointerSettingName,
                                SettingEditorKind.EaseOfAccessCursorAndPointer,
                                GlyphInfo.TouchPointer,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.MagnifierSettingName,
                                SettingEditorKind.EaseOfAccessMagnifier,
                                GlyphInfo.ZoomIn,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.ColorFiltersSettingName,
                                SettingEditorKind.EaseOfAccessColorFilters,
                                GlyphInfo.Color,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.HighContrastSettingName,
                                SettingEditorKind.EaseOfAccessHighContrast,
                                GlyphInfo.Brightness,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.NarratorSettingName,
                                SettingEditorKind.EaseOfAccessNarrator,
                                GlyphInfo.Narrator,
                                headerDisplayName: Strings.VisionSettingHeader),

                            // Audio
                            new SettingNavigationInfoViewModel(Strings.AudioSettingName,
                                SettingEditorKind.EaseOfAccessAudio,
                                GlyphInfo.Volume,
                                headerDisplayName: Strings.AudioSettingName),
                            new SettingNavigationInfoViewModel(Strings.ClosedCaptionsSettingName,
                                SettingEditorKind.EaseOfAccessClosedCaptions,
                                GlyphInfo.CC,
                                headerDisplayName: Strings.AudioSettingName),

                            // Interaction
                            new SettingNavigationInfoViewModel(Strings.SpeechSettingName,
                                SettingEditorKind.EaseOfAccessSpeech,
                                GlyphInfo.Microphone,
                                headerDisplayName: Strings.InteractionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.KeyboardSettingName,
                                SettingEditorKind.EaseOfAccessKeyboard,
                                GlyphInfo.KeyboardClassic,
                                headerDisplayName: Strings.InteractionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.MouseSettingName,
                                SettingEditorKind.EaseOfAccessMouse,
                                GlyphInfo.Mouse,
                                headerDisplayName: Strings.InteractionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.EyeControlSettingName,
                                SettingEditorKind.EaseOfAccessEyeControl,
                                GlyphInfo.EyeGaze,
                                headerDisplayName: Strings.InteractionSettingHeader),
                        });

                case SettingGroupKind.Search:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Search,
                        Strings.SearchGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.PermissionsAndHistorySettingName,
                                SettingEditorKind.SearchPermissionsAndHistory,
                                GlyphInfo.SecureApp,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.SearchingWindowsSettingName,
                                SettingEditorKind.SearchSearchingWindows,
                                GlyphInfo.Zoom),
                            new SettingNavigationInfoViewModel(
                                Strings.MoreDetailsSettingName,
                                SettingEditorKind.SearchMoreDetails,
                                GlyphInfo.Info),
                        });

                case SettingGroupKind.Cortana:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Cortana,
                        Strings.CortanaGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.TalkToCortanaSettingName,
                                SettingEditorKind.CortanaTalkToCortana,
                                GlyphInfo.Microphone,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.PermissionsSettingName,
                                SettingEditorKind.CortanaPermissions,
                                GlyphInfo.SecureApp),
                            new SettingNavigationInfoViewModel(
                                Strings.MoreDetailsSettingName,
                                SettingEditorKind.CortanaMoreDetails,
                                GlyphInfo.Info),
                        });

                case SettingGroupKind.Privacy:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Privacy,
                        Strings.PrivacyGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.GeneralSettingName,
                                SettingEditorKind.PrivacyGeneral,
                                GlyphInfo.Lock,
                                isSelected: true,
                                headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.SpeechSettingName,
                                SettingEditorKind.PrivacySpeech,
                                GlyphInfo.Speech,
                                headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.InkingAndTypingPersonalizationSettingName,
                                SettingEditorKind.PrivacyInkingAndTypingPersonalization,
                                GlyphInfo.Trackers,
                                headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.DiagnosticsAndFeedbackSettingName,
                                SettingEditorKind.PrivacyDiagnosticsAndFeedback,
                                GlyphInfo.Feedback,
                                headerDisplayName: Strings.WindowsPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.ActivityHistorySettingName,
                                SettingEditorKind.PrivacyActivityHistory,
                                GlyphInfo.TaskView,
                                headerDisplayName: Strings.WindowsPermissionsSettingHeader),

                            new SettingNavigationInfoViewModel(
                                Strings.LocationSettingName,
                                SettingEditorKind.PrivacyLocation,
                                GlyphInfo.MapPin,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.CameraSettingName,
                                SettingEditorKind.PrivacyCamera,
                                GlyphInfo.Camera,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.MicrophoneSettingName,
                                SettingEditorKind.PrivacyMicrophone,
                                GlyphInfo.Microphone,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.VoiceActivationSettingName,
                                SettingEditorKind.PrivacyVoiceActivation,
                                GlyphInfo.MicrophoneListening,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.NotificationsSettingName,
                                SettingEditorKind.PrivacyNotifications,
                                GlyphInfo.ActionCenter,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.AccountInfoSettingName,
                                SettingEditorKind.PrivacyAccountInfo,
                                GlyphInfo.ContactInfo,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.ContactsSettingName,
                                SettingEditorKind.PrivacyContacts,
                                GlyphInfo.People,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.CalendarSettingName,
                                SettingEditorKind.PrivacyCalendar,
                                GlyphInfo.Calendar,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.PhoneCallsSettingName,
                                SettingEditorKind.PrivacyPhoneCalls,
                                GlyphInfo.Phone,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.CallHistorySettingName,
                                SettingEditorKind.PrivacyCallHistory,
                                GlyphInfo.History,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.EmailSettingName,
                                SettingEditorKind.PrivacyEmail,
                                GlyphInfo.Mail,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.TasksSettingName,
                                SettingEditorKind.PrivacyTasks,
                                GlyphInfo.Trackers,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.MessagingSettingName,
                                SettingEditorKind.PrivacyMessaging,
                                GlyphInfo.Message,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.RadiosSettingName,
                                SettingEditorKind.PrivacyRadios,
                                GlyphInfo.NetworkTower,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.OtherDevicesSettingName,
                                SettingEditorKind.PrivacyOtherDevices,
                                GlyphInfo.Devices2,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.BackgroundAppsSettingName,
                                SettingEditorKind.PrivacyBackgroundApps,
                                GlyphInfo.Diagnostic,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.AppDiagnosticsSettingName,
                                SettingEditorKind.PrivacyAppDiagnostics,
                                GlyphInfo.AreaChart,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.AutomaticFileDownloadsSettingName,
                                SettingEditorKind.PrivacyAutomaticFileDownloads,
                                GlyphInfo.Cloud,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.DocumentsSettingName,
                                SettingEditorKind.PrivacyDocuments,
                                GlyphInfo.Page,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.PicturesSettingName,
                                SettingEditorKind.PrivacyPictures,
                                GlyphInfo.Photo2,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.VideosSettingName,
                                SettingEditorKind.PrivacyVideos,
                                GlyphInfo.Movies,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                            new SettingNavigationInfoViewModel(
                                Strings.FileSystemSettingName,
                                SettingEditorKind.PrivacyFileSystem,
                                GlyphInfo.Page,
                                headerDisplayName: Strings.AppPermissionsSettingHeader),
                        });

                case SettingGroupKind.UpdateAndSecurity:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.UpdateAndSecurity,
                        Strings.UpdateAndSecurityGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                Strings.WindowsUpdateSettingName,
                                SettingEditorKind.UpdateAndSecurityWindowsUpdate,
                                GlyphInfo.Sync,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.DeliveryOptimizationSettingName,
                                SettingEditorKind.UpdateAndSecurityDeliveryOptimization,
                                GlyphInfo.DeliveryOptimization),
                            new SettingNavigationInfoViewModel(
                                Strings.WindowsSecuritySettingName,
                                SettingEditorKind.UpdateAndSecurityWindowsSecurity,
                                GlyphInfo.DefenderApp),
                            new SettingNavigationInfoViewModel(
                                Strings.BackupSettingName,
                                SettingEditorKind.UpdateAndSecurityBackup,
                                GlyphInfo.Upload),
                            new SettingNavigationInfoViewModel(
                                Strings.TroubleshootSettingName,
                                SettingEditorKind.UpdateAndSecurityTroubleshoot,
                                GlyphInfo.Repair),
                            new SettingNavigationInfoViewModel(
                                Strings.RecoverySettingName,
                                SettingEditorKind.UpdateAndSecurityRecovery,
                                GlyphInfo.ResetDrive),
                            new SettingNavigationInfoViewModel(
                                Strings.ActivationSettingName,
                                SettingEditorKind.UpdateAndSecurityActivation,
                                GlyphInfo.Completed),
                            new SettingNavigationInfoViewModel(
                                Strings.FindMyDeviceSettingName,
                                SettingEditorKind.UpdateAndSecurityFindMyDevice,
                                GlyphInfo.MapPin),
                            new SettingNavigationInfoViewModel(
                                Strings.ForDevelopersSettingName,
                                SettingEditorKind.UpdateAndSecurityForDevelopers,
                                GlyphInfo.DeveloperTools),
                            new SettingNavigationInfoViewModel(
                                Strings.WindowsInsiderProgramSettingName,
                                SettingEditorKind.UpdateAndSecurityWindowsInsiderProgram,
                                GlyphInfo.WindowsInsider),
                        });

                default:
                    throw new ArgumentOutOfRangeException(nameof(groupKind), groupKind, null);
            }
        }
    }
}
