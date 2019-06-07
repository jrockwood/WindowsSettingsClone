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
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public SettingGroupKind GroupKind { get; }
        public string GroupName { get; }
        public ReadOnlyCollection<SettingNavigationInfoViewModel> Settings { get; }

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
                            SettingNavigationInfoViewModel.Create(
                                Strings.DisplaySettingName,
                                SettingEditorKind.Display,
                                GlyphInfo.TvMonitor,
                                isSelected : true),
                            SettingNavigationInfoViewModel.Create(Strings.SoundSettingName,
                                SettingEditorKind.Sound,
                                GlyphInfo.Volume),
                            SettingNavigationInfoViewModel.Create(Strings.NotificationsAndActionsSettingName,
                                SettingEditorKind.NotificationsAndActions,
                                GlyphInfo.Message),
                            SettingNavigationInfoViewModel.Create(Strings.FocusAssistSettingName,
                                SettingEditorKind.FocusAssist,
                                GlyphInfo.QuietHours),
                            SettingNavigationInfoViewModel.Create(Strings.PowerAndSleepSettingName,
                                SettingEditorKind.PowerAndSleep,
                                GlyphInfo.PowerButton),
                            SettingNavigationInfoViewModel.Create(Strings.StorageSettingName,
                                SettingEditorKind.Storage,
                                GlyphInfo.HardDrive),
                            SettingNavigationInfoViewModel.Create(Strings.TabletModeSettingName,
                                SettingEditorKind.TabletMode,
                                GlyphInfo.TabletMode),
                            SettingNavigationInfoViewModel.Create(Strings.MultitaskingSettingName,
                                SettingEditorKind.Multitasking,
                                GlyphInfo.TaskView),
                            SettingNavigationInfoViewModel.Create(Strings.ProjectingToThisPCSettingName,
                                SettingEditorKind.ProjectingToThisPC,
                                GlyphInfo.Project),
                            SettingNavigationInfoViewModel.Create(Strings.SharedExperiencesSettingName,
                                SettingEditorKind.SharedExperiences,
                                GlyphInfo.Connected),
                            SettingNavigationInfoViewModel.Create(Strings.ClipboardSettingName,
                                SettingEditorKind.Clipboard,
                                GlyphInfo.Paste),
                            SettingNavigationInfoViewModel.Create(Strings.RemoteDesktopSettingName,
                                SettingEditorKind.RemoteDesktop,
                                GlyphInfo.Remote),
                            SettingNavigationInfoViewModel.Create(Strings.AboutSettingName,
                                SettingEditorKind.About,
                                GlyphInfo.Info),
                        });

                case SettingGroupKind.Devices:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Devices,
                        Strings.DevicesGroupName,
                        new[]
                        {
                            SettingNavigationInfoViewModel.Create(
                                Strings.BluetoothAndOtherDevicesSettingName,
                                SettingEditorKind.BluetoothAndOtherDevices,
                                GlyphInfo.Devices,
                                isSelected: true),
                            SettingNavigationInfoViewModel.Create(Strings.PrintersAndScannersSettingName,
                                SettingEditorKind.PrintersAndScanners,
                                GlyphInfo.Print),
                            SettingNavigationInfoViewModel.Create(Strings.MouseSettingName,
                                SettingEditorKind.Mouse,
                                GlyphInfo.Mouse),
                            SettingNavigationInfoViewModel.Create(Strings.TypingSettingName,
                                SettingEditorKind.Typing,
                                GlyphInfo.KeyboardClassic),
                            SettingNavigationInfoViewModel.Create(Strings.PenAndWindowsInkSettingName,
                                SettingEditorKind.PenAndWindowsInk,
                                GlyphInfo.PenWorkspace),
                            SettingNavigationInfoViewModel.Create(Strings.AutoPlaySettingName,
                                SettingEditorKind.AutoPlay,
                                GlyphInfo.PlaybackRate1x),
                            SettingNavigationInfoViewModel.Create(Strings.UsbSettingName,
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
                            SettingNavigationInfoViewModel.Create(Strings.NetworkStatusSettingName,
                                SettingEditorKind.NetworkStatus,
                                GlyphInfo.MyNetwork,
                                isSelected: true),
                            SettingNavigationInfoViewModel.Create(Strings.EthernetSettingName,
                                SettingEditorKind.Ethernet,
                                GlyphInfo.Ethernet),
                            SettingNavigationInfoViewModel.Create(Strings.DialUpSettingName,
                                SettingEditorKind.DialUp,
                                GlyphInfo.DialUp),
                            SettingNavigationInfoViewModel.Create(Strings.VpnSettingName,
                                SettingEditorKind.Vpn,
                                GlyphInfo.Vpn),
                            SettingNavigationInfoViewModel.Create(Strings.DataUsageSettingName,
                                SettingEditorKind.DataUsage,
                                GlyphInfo.PieSingle),
                            SettingNavigationInfoViewModel.Create(Strings.ProxySettingName,
                                SettingEditorKind.Proxy,
                                GlyphInfo.Globe),
                        });

                case SettingGroupKind.Personalization:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Personalization,
                        Strings.PersonalizationGroupName,
                        new[]
                        {
                            SettingNavigationInfoViewModel.Create(
                                Strings.BackgroundSettingName,
                                SettingEditorKind.Background,
                                GlyphInfo.Photo2,
                                isSelected: true),
                            SettingNavigationInfoViewModel.Create(Strings.ColorsSettingName,
                                SettingEditorKind.Colors,
                                GlyphInfo.Color),
                            SettingNavigationInfoViewModel.Create(Strings.LockScreenSettingName,
                                SettingEditorKind.LockScreen,
                                GlyphInfo.LockScreenDesktop),
                            SettingNavigationInfoViewModel.Create(Strings.ThemesSettingName,
                                SettingEditorKind.Themes,
                                GlyphInfo.Personalize),
                            SettingNavigationInfoViewModel.Create(Strings.FontsSettingName,
                                SettingEditorKind.Fonts,
                                GlyphInfo.Font),
                            SettingNavigationInfoViewModel.Create(Strings.StartSettingName,
                                SettingEditorKind.Start,
                                GlyphInfo.Tiles),
                            SettingNavigationInfoViewModel.Create(Strings.TaskbarSettingName,
                                SettingEditorKind.Taskbar,
                                GlyphInfo.DockBottom),
                        });

                case SettingGroupKind.Apps:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Apps,
                        Strings.AppsGroupName,
                        new[]
                        {
                            SettingNavigationInfoViewModel.Create(
                                Strings.AppsAndFeaturesSettingName,
                                SettingEditorKind.AppsAndFeatures,
                                GlyphInfo.AllApps,
                                isSelected: true),
                            SettingNavigationInfoViewModel.Create(Strings.DefaultAppsSettingName,
                                SettingEditorKind.DefaultApps,
                                GlyphInfo.OpenWith),
                            SettingNavigationInfoViewModel.Create(Strings.OfflineMapsSettingName,
                                SettingEditorKind.OfflineMaps,
                                GlyphInfo.DownloadMap),
                            SettingNavigationInfoViewModel.Create(Strings.AppsForWebsitesSettingName,
                                SettingEditorKind.AppsForWebsites,
                                GlyphInfo.NewWindow),
                            SettingNavigationInfoViewModel.Create(Strings.VideoPlaybackSettingName,
                                SettingEditorKind.VideoPlayback,
                                GlyphInfo.Video),
                            SettingNavigationInfoViewModel.Create(Strings.StartupSettingName,
                                SettingEditorKind.Startup,
                                GlyphInfo.SetLockScreen),
                        });

                case SettingGroupKind.Accounts:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Accounts,
                        Strings.AccountsGroupName,
                        new[]
                        {
                            SettingNavigationInfoViewModel.Create(
                                Strings.YourInfoSettingName,
                                SettingEditorKind.YourInfo,
                                GlyphInfo.ContactInfo,
                                isSelected: true),
                            SettingNavigationInfoViewModel.Create(Strings.EmailAndAccountsSettingName,
                                SettingEditorKind.EmailAndAccounts,
                                GlyphInfo.Mail),
                            SettingNavigationInfoViewModel.Create(Strings.SignInOptionsSettingName,
                                SettingEditorKind.SignInOptions,
                                GlyphInfo.Permissions),
                            SettingNavigationInfoViewModel.Create(Strings.AccessWorkOrSchoolSettingName,
                                SettingEditorKind.AccessWorkOrSchool,
                                GlyphInfo.Work),
                            SettingNavigationInfoViewModel.Create(Strings.FamilyAndOtherUsersSettingName,
                                SettingEditorKind.FamilyAndOtherUsers,
                                GlyphInfo.AddFriend),
                            SettingNavigationInfoViewModel.Create(Strings.SyncYourSettingsSettingName,
                                SettingEditorKind.SyncYourSettings,
                                GlyphInfo.Sync),
                        });

                case SettingGroupKind.TimeAndLanguage:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.TimeAndLanguage,
                        Strings.TimeAndLanguageGroupName,
                        new[]
                        {
                            SettingNavigationInfoViewModel.Create(
                                Strings.DateAndTimeSettingName,
                                SettingEditorKind.DateAndTime,
                                GlyphInfo.DateTime,
                                isSelected: true),
                            SettingNavigationInfoViewModel.Create(Strings.RegionSettingName,
                                SettingEditorKind.Region,
                                GlyphInfo.World),
                            SettingNavigationInfoViewModel.Create(Strings.LanguageSettingName,
                                SettingEditorKind.Language,
                                GlyphInfo.Characters),
                            SettingNavigationInfoViewModel.Create(Strings.SpeechSettingName,
                                SettingEditorKind.Speech,
                                GlyphInfo.Microphone),
                        });

                case SettingGroupKind.Gaming:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.Gaming,
                        Strings.GamingGroupName,
                        new[]
                        {
                            SettingNavigationInfoViewModel.Create(
                                Strings.GameBarSettingName,
                                SettingEditorKind.GameBar,
                                GlyphInfo.GameBar,
                                isSelected: true),
                            SettingNavigationInfoViewModel.Create(Strings.CapturesSettingName,
                                SettingEditorKind.Captures,
                                GlyphInfo.Captures),
                            SettingNavigationInfoViewModel.Create(Strings.BroadcastingSettingName,
                                SettingEditorKind.Broadcasting,
                                GlyphInfo.SatelliteDish),
                            SettingNavigationInfoViewModel.Create(Strings.GameModeSettingName,
                                SettingEditorKind.GameMode,
                                GlyphInfo.SpeedHigh),
                            SettingNavigationInfoViewModel.Create(Strings.XboxNetworkingSettingName,
                                SettingEditorKind.XboxNetworking,
                                GlyphInfo.XboxLogo),
                        });

                case SettingGroupKind.EaseOfAccess:
                    return new SettingsGroupPageViewModel(
                        SettingGroupKind.EaseOfAccess,
                        Strings.EaseOfAccessGroupName,
                        new[]
                        {
                            SettingNavigationInfoViewModel.CreateHeader(Strings.VisionSettingHeader),
                            SettingNavigationInfoViewModel.Create(
                                Strings.DisplaySettingName,
                                SettingEditorKind.EaseOfAccessDisplay,
                                GlyphInfo.TvMonitor,
                                isSelected: true),
                            SettingNavigationInfoViewModel.Create(Strings.CursorAndPointerSettingName,
                                SettingEditorKind.CursorAndPointer,
                                GlyphInfo.TouchPointer),
                            SettingNavigationInfoViewModel.Create(Strings.MagnifierSettingName,
                                SettingEditorKind.Magnifier,
                                GlyphInfo.ZoomIn),
                            SettingNavigationInfoViewModel.Create(Strings.ColorFiltersSettingName,
                                SettingEditorKind.ColorFilters,
                                GlyphInfo.Color),
                            SettingNavigationInfoViewModel.Create(Strings.HighContrastSettingName,
                                SettingEditorKind.HighContrast,
                                GlyphInfo.Brightness),
                            SettingNavigationInfoViewModel.Create(Strings.NarratorSettingName,
                                SettingEditorKind.Narrator,
                                GlyphInfo.Narrator),
                            SettingNavigationInfoViewModel.CreateHeader(Strings.AudioSettingName),
                            SettingNavigationInfoViewModel.Create(Strings.AudioSettingName,
                                SettingEditorKind.Audio,
                                GlyphInfo.Volume),
                            SettingNavigationInfoViewModel.Create(Strings.ClosedCaptionsSettingName,
                                SettingEditorKind.ClosedCaptions,
                                GlyphInfo.CC),
                            SettingNavigationInfoViewModel.CreateHeader(Strings.InteractionSettingHeader),
                            SettingNavigationInfoViewModel.Create(Strings.SpeechSettingName,
                                SettingEditorKind.EaseOfAccessSpeech,
                                GlyphInfo.Microphone),
                            SettingNavigationInfoViewModel.Create(Strings.KeyboardSettingName,
                                SettingEditorKind.EaseOfAccessKeyboard,
                                GlyphInfo.KeyboardClassic),
                            SettingNavigationInfoViewModel.Create(Strings.MouseSettingName,
                                SettingEditorKind.EaseOfAccessMouse,
                                GlyphInfo.Mouse),
                            SettingNavigationInfoViewModel.Create(Strings.EyeControlSettingName,
                                SettingEditorKind.EyeControl,
                                GlyphInfo.EyeGaze),
                        });

                case SettingGroupKind.Search:
                    break;

                case SettingGroupKind.Cortana:
                    break;

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
