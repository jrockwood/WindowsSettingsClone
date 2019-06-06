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
                        groupKind,
                        Strings.SystemGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Display,
                                GlyphInfo.TvMonitor,
                                Strings.DisplaySettingName) {IsSelected = true},
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Sound,
                                GlyphInfo.Volume,
                                Strings.SoundSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.NotificationsAndActions,
                                GlyphInfo.Message,
                                Strings.NotificationsAndActionsSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.FocusAssist,
                                GlyphInfo.QuietHours,
                                Strings.FocusAssistSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.PowerAndSleep,
                                GlyphInfo.PowerButton,
                                Strings.PowerAndSleepSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Storage,
                                GlyphInfo.HardDrive,
                                Strings.StorageSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.TabletMode,
                                GlyphInfo.TabletMode,
                                Strings.TabletModeSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Multitasking,
                                GlyphInfo.TaskView,
                                Strings.MultitaskingSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.ProjectingToThisPC,
                                GlyphInfo.Project,
                                Strings.ProjectingToThisPCSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.SharedExperiences,
                                GlyphInfo.Connected,
                                Strings.SharedExperiencesSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Clipboard,
                                GlyphInfo.Paste,
                                Strings.ClipboardSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.RemoteDesktop,
                                GlyphInfo.Remote,
                                Strings.RemoteDesktopSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.About,
                                GlyphInfo.Info,
                                Strings.AboutSettingName),
                        });

                case SettingGroupKind.Devices:
                    return new SettingsGroupPageViewModel(
                        groupKind,
                        Strings.DevicesGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.BluetoothAndOtherDevices,
                                GlyphInfo.Devices,
                                Strings.BluetoothAndOtherDevicesSettingName) {IsSelected = true},
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.PrintersAndScanners,
                                GlyphInfo.Print,
                                Strings.PrintersAndScannersSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Mouse,
                                GlyphInfo.Mouse,
                                Strings.MouseSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Typing,
                                GlyphInfo.KeyboardClassic,
                                Strings.TypingSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.PenAndWindowsInk,
                                GlyphInfo.PenWorkspace,
                                Strings.PenAndWindowsInkSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.AutoPlay,
                                GlyphInfo.PlaybackRate1x,
                                Strings.AutoPlaySettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Usb,
                                GlyphInfo.Usb,
                                Strings.UsbSettingName),
                        });

                case SettingGroupKind.Phone:
                    return new SettingsGroupPageViewModel(groupKind, Strings.PhoneGroupName);

                case SettingGroupKind.NetworkAndInternet:
                    return new SettingsGroupPageViewModel(
                        groupKind,
                        Strings.NetworkAndInternetGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.NetworkStatus,
                                GlyphInfo.MyNetwork,
                                Strings.NetworkStatusSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Ethernet,
                                GlyphInfo.Ethernet,
                                Strings.EthernetSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.DialUp,
                                GlyphInfo.DialUp,
                                Strings.DialUpSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Vpn,
                                GlyphInfo.Vpn,
                                Strings.VpnSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.DataUsage,
                                GlyphInfo.PieSingle,
                                Strings.DataUsageSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Proxy,
                                GlyphInfo.Globe,
                                Strings.ProxySettingName),
                        });

                case SettingGroupKind.Personalization:
                    return new SettingsGroupPageViewModel(
                        groupKind,
                        Strings.PersonalizationGroupName,
                        new[]
                        {
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Background,
                                GlyphInfo.Photo2,
                                Strings.BackgroundSettingName) {IsSelected = true},
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Colors,
                                GlyphInfo.Color,
                                Strings.ColorsSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.LockScreen,
                                GlyphInfo.LockscreenDesktop,
                                Strings.LockScreenSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Themes,
                                GlyphInfo.Personalize,
                                Strings.ThemesSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Fonts,
                                GlyphInfo.Font,
                                Strings.FontsSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Start,
                                GlyphInfo.Tiles,
                                Strings.StartSettingName),
                            new SettingNavigationInfoViewModel(
                                SettingEditorKind.Taskbar,
                                GlyphInfo.DockBottom,
                                Strings.TaskbarSettingName),
                        });

                case SettingGroupKind.Apps:
                    break;

                case SettingGroupKind.Accounts:
                    break;

                case SettingGroupKind.TimeAndLanguage:
                    break;

                case SettingGroupKind.Gaming:
                    break;

                case SettingGroupKind.EaseOfAccess:
                    break;

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
