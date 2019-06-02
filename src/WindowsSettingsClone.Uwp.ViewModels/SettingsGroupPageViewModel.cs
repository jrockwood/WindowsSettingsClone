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

        private static readonly SettingNavigationInfo[] s_systemGroupSettings =
        {
            new SettingNavigationInfo(SettingEditorKind.Display, GlyphInfo.TvMonitor, Strings.DisplaySettingName),
            new SettingNavigationInfo(SettingEditorKind.Sound, GlyphInfo.Volume, Strings.SoundSettingName),
            new SettingNavigationInfo(
                SettingEditorKind.NotificationsAndActions,
                GlyphInfo.Message,
                Strings.NotificationsAndActionsSettingName),
            new SettingNavigationInfo(
                SettingEditorKind.FocusAssist,
                GlyphInfo.QuietHours,
                Strings.FocusAssistSettingName),
            new SettingNavigationInfo(
                SettingEditorKind.PowerAndSleep,
                GlyphInfo.PowerButton,
                Strings.PowerAndSleepSettingName),
            new SettingNavigationInfo(SettingEditorKind.Storage, GlyphInfo.HardDrive, Strings.StorageSettingName),
            new SettingNavigationInfo(
                SettingEditorKind.TabletMode,
                GlyphInfo.TabletMode,
                Strings.TabletModeSettingName),
            new SettingNavigationInfo(
                SettingEditorKind.Multitasking,
                GlyphInfo.Multitasking,
                Strings.MultitaskingSettingName),
            new SettingNavigationInfo(
                SettingEditorKind.ProjectingToThisPC,
                GlyphInfo.Project,
                Strings.ProjectingToThisPCSettingName),
            new SettingNavigationInfo(
                SettingEditorKind.SharedExperiences,
                GlyphInfo.Connected,
                Strings.SharedExperiencesSettingName),
            new SettingNavigationInfo(SettingEditorKind.Clipboard, GlyphInfo.Paste, Strings.ClipboardSettingName),
            new SettingNavigationInfo(
                SettingEditorKind.RemoteDesktop,
                GlyphInfo.Remote,
                Strings.RemoteDesktopSettingName),
            new SettingNavigationInfo(SettingEditorKind.About, GlyphInfo.Info, Strings.AboutSettingName),
        };

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private SettingsGroupPageViewModel(
            SettingGroupKind groupKind,
            string groupName,
            IEnumerable<SettingNavigationInfo> settings)
        {
            GroupKind = groupKind;
            GroupName = Param.VerifyString(groupName, nameof(groupName));
            Settings = new ReadOnlyCollection<SettingNavigationInfo>(
                Param.VerifyNotNull(settings, nameof(settings)).ToList());
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public SettingGroupKind GroupKind { get; }
        public string GroupName { get; }
        public ReadOnlyCollection<SettingNavigationInfo> Settings { get; }

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
                    return new SettingsGroupPageViewModel(groupKind, Strings.SystemGroupName, s_systemGroupSettings);

                case SettingGroupKind.Devices:
                    break;

                case SettingGroupKind.Phone:
                    break;

                case SettingGroupKind.NetworkAndInternet:
                    break;

                case SettingGroupKind.Personalization:
                    break;

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

            return new SettingsGroupPageViewModel(SettingGroupKind.Accounts, "Accounts", new SettingNavigationInfo[0]);
        }
    }
}
