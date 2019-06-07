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
                                SettingEditorKind.Display,
                                GlyphInfo.TvMonitor,
                                isSelected : true),
                            new SettingNavigationInfoViewModel(Strings.SoundSettingName,
                                SettingEditorKind.Sound,
                                GlyphInfo.Volume),
                            new SettingNavigationInfoViewModel(Strings.NotificationsAndActionsSettingName,
                                SettingEditorKind.NotificationsAndActions,
                                GlyphInfo.Message),
                            new SettingNavigationInfoViewModel(Strings.FocusAssistSettingName,
                                SettingEditorKind.FocusAssist,
                                GlyphInfo.QuietHours),
                            new SettingNavigationInfoViewModel(Strings.PowerAndSleepSettingName,
                                SettingEditorKind.PowerAndSleep,
                                GlyphInfo.PowerButton),
                            new SettingNavigationInfoViewModel(Strings.StorageSettingName,
                                SettingEditorKind.Storage,
                                GlyphInfo.HardDrive),
                            new SettingNavigationInfoViewModel(Strings.TabletModeSettingName,
                                SettingEditorKind.TabletMode,
                                GlyphInfo.TabletMode),
                            new SettingNavigationInfoViewModel(Strings.MultitaskingSettingName,
                                SettingEditorKind.Multitasking,
                                GlyphInfo.TaskView),
                            new SettingNavigationInfoViewModel(Strings.ProjectingToThisPCSettingName,
                                SettingEditorKind.ProjectingToThisPC,
                                GlyphInfo.Project),
                            new SettingNavigationInfoViewModel(Strings.SharedExperiencesSettingName,
                                SettingEditorKind.SharedExperiences,
                                GlyphInfo.Connected),
                            new SettingNavigationInfoViewModel(Strings.ClipboardSettingName,
                                SettingEditorKind.Clipboard,
                                GlyphInfo.Paste),
                            new SettingNavigationInfoViewModel(Strings.RemoteDesktopSettingName,
                                SettingEditorKind.RemoteDesktop,
                                GlyphInfo.Remote),
                            new SettingNavigationInfoViewModel(Strings.AboutSettingName,
                                SettingEditorKind.About,
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
                                SettingEditorKind.BluetoothAndOtherDevices,
                                GlyphInfo.Devices,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.PrintersAndScannersSettingName,
                                SettingEditorKind.PrintersAndScanners,
                                GlyphInfo.Print),
                            new SettingNavigationInfoViewModel(Strings.MouseSettingName,
                                SettingEditorKind.Mouse,
                                GlyphInfo.Mouse),
                            new SettingNavigationInfoViewModel(Strings.TypingSettingName,
                                SettingEditorKind.Typing,
                                GlyphInfo.KeyboardClassic),
                            new SettingNavigationInfoViewModel(Strings.PenAndWindowsInkSettingName,
                                SettingEditorKind.PenAndWindowsInk,
                                GlyphInfo.PenWorkspace),
                            new SettingNavigationInfoViewModel(Strings.AutoPlaySettingName,
                                SettingEditorKind.AutoPlay,
                                GlyphInfo.PlaybackRate1x),
                            new SettingNavigationInfoViewModel(Strings.UsbSettingName,
                                SettingEditorKind.Usb,
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
                                SettingEditorKind.NetworkStatus,
                                GlyphInfo.MyNetwork,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.EthernetSettingName,
                                SettingEditorKind.Ethernet,
                                GlyphInfo.Ethernet),
                            new SettingNavigationInfoViewModel(Strings.DialUpSettingName,
                                SettingEditorKind.DialUp,
                                GlyphInfo.DialUp),
                            new SettingNavigationInfoViewModel(Strings.VpnSettingName,
                                SettingEditorKind.Vpn,
                                GlyphInfo.Vpn),
                            new SettingNavigationInfoViewModel(Strings.DataUsageSettingName,
                                SettingEditorKind.DataUsage,
                                GlyphInfo.PieSingle),
                            new SettingNavigationInfoViewModel(Strings.ProxySettingName,
                                SettingEditorKind.Proxy,
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
                                SettingEditorKind.Background,
                                GlyphInfo.Photo2,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.ColorsSettingName,
                                SettingEditorKind.Colors,
                                GlyphInfo.Color),
                            new SettingNavigationInfoViewModel(Strings.LockScreenSettingName,
                                SettingEditorKind.LockScreen,
                                GlyphInfo.LockScreenDesktop),
                            new SettingNavigationInfoViewModel(Strings.ThemesSettingName,
                                SettingEditorKind.Themes,
                                GlyphInfo.Personalize),
                            new SettingNavigationInfoViewModel(Strings.FontsSettingName,
                                SettingEditorKind.Fonts,
                                GlyphInfo.Font),
                            new SettingNavigationInfoViewModel(Strings.StartSettingName,
                                SettingEditorKind.Start,
                                GlyphInfo.Tiles),
                            new SettingNavigationInfoViewModel(Strings.TaskbarSettingName,
                                SettingEditorKind.Taskbar,
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
                                SettingEditorKind.AppsAndFeatures,
                                GlyphInfo.AllApps,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.DefaultAppsSettingName,
                                SettingEditorKind.DefaultApps,
                                GlyphInfo.OpenWith),
                            new SettingNavigationInfoViewModel(Strings.OfflineMapsSettingName,
                                SettingEditorKind.OfflineMaps,
                                GlyphInfo.DownloadMap),
                            new SettingNavigationInfoViewModel(Strings.AppsForWebsitesSettingName,
                                SettingEditorKind.AppsForWebsites,
                                GlyphInfo.NewWindow),
                            new SettingNavigationInfoViewModel(Strings.VideoPlaybackSettingName,
                                SettingEditorKind.VideoPlayback,
                                GlyphInfo.Video),
                            new SettingNavigationInfoViewModel(Strings.StartupSettingName,
                                SettingEditorKind.Startup,
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
                                SettingEditorKind.YourInfo,
                                GlyphInfo.ContactInfo,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.EmailAndAccountsSettingName,
                                SettingEditorKind.EmailAndAccounts,
                                GlyphInfo.Mail),
                            new SettingNavigationInfoViewModel(Strings.SignInOptionsSettingName,
                                SettingEditorKind.SignInOptions,
                                GlyphInfo.Permissions),
                            new SettingNavigationInfoViewModel(Strings.AccessWorkOrSchoolSettingName,
                                SettingEditorKind.AccessWorkOrSchool,
                                GlyphInfo.Work),
                            new SettingNavigationInfoViewModel(Strings.FamilyAndOtherUsersSettingName,
                                SettingEditorKind.FamilyAndOtherUsers,
                                GlyphInfo.AddFriend),
                            new SettingNavigationInfoViewModel(Strings.SyncYourSettingsSettingName,
                                SettingEditorKind.SyncYourSettings,
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
                                SettingEditorKind.DateAndTime,
                                GlyphInfo.DateTime,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.RegionSettingName,
                                SettingEditorKind.Region,
                                GlyphInfo.World),
                            new SettingNavigationInfoViewModel(Strings.LanguageSettingName,
                                SettingEditorKind.Language,
                                GlyphInfo.Characters),
                            new SettingNavigationInfoViewModel(Strings.SpeechSettingName,
                                SettingEditorKind.Speech,
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
                                SettingEditorKind.GameBar,
                                GlyphInfo.GameBar,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(Strings.CapturesSettingName,
                                SettingEditorKind.Captures,
                                GlyphInfo.Captures),
                            new SettingNavigationInfoViewModel(Strings.BroadcastingSettingName,
                                SettingEditorKind.Broadcasting,
                                GlyphInfo.SatelliteDish),
                            new SettingNavigationInfoViewModel(Strings.GameModeSettingName,
                                SettingEditorKind.GameMode,
                                GlyphInfo.SpeedHigh),
                            new SettingNavigationInfoViewModel(Strings.XboxNetworkingSettingName,
                                SettingEditorKind.XboxNetworking,
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
                                SettingEditorKind.CursorAndPointer,
                                GlyphInfo.TouchPointer,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.MagnifierSettingName,
                                SettingEditorKind.Magnifier,
                                GlyphInfo.ZoomIn,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.ColorFiltersSettingName,
                                SettingEditorKind.ColorFilters,
                                GlyphInfo.Color,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.HighContrastSettingName,
                                SettingEditorKind.HighContrast,
                                GlyphInfo.Brightness,
                                headerDisplayName: Strings.VisionSettingHeader),
                            new SettingNavigationInfoViewModel(Strings.NarratorSettingName,
                                SettingEditorKind.Narrator,
                                GlyphInfo.Narrator,
                                headerDisplayName: Strings.VisionSettingHeader),

                            // Audio
                            new SettingNavigationInfoViewModel(Strings.AudioSettingName,
                                SettingEditorKind.Audio,
                                GlyphInfo.Volume,
                                headerDisplayName: Strings.AudioSettingName),
                            new SettingNavigationInfoViewModel(Strings.ClosedCaptionsSettingName,
                                SettingEditorKind.ClosedCaptions,
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
                                SettingEditorKind.EyeControl,
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
                                SettingEditorKind.PermissionsAndHistory,
                                GlyphInfo.SecureApp,
                                isSelected: true),
                            new SettingNavigationInfoViewModel(
                                Strings.SearchingWindowsSettingName,
                                SettingEditorKind.SearchingWindows,
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
                                SettingEditorKind.TalkToCortana,
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
                    break;

                case SettingGroupKind.UpdateAndSecurity:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(groupKind), groupKind, null);
            }

            return new SettingsGroupPageViewModel(
                groupKind,
                groupKind.ToString(),
                new SettingNavigationInfoViewModel[0]);
        }
    }
}
