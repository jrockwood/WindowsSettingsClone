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
            IEnumerable<SettingNavigationInfoViewModel> settings)
        {
            GroupKind = groupKind;
            GroupName = Param.VerifyString(groupName, nameof(groupName));
            Settings = new ReadOnlyCollection<SettingNavigationInfoViewModel>(
                Param.VerifyNotNull(settings, nameof(settings)).ToList());
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
                                Strings.DisplaySettingName),
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
                                GlyphInfo.Multitasking,
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

            return new SettingsGroupPageViewModel(SettingGroupKind.Accounts, "Accounts", new SettingNavigationInfoViewModel[0]);
        }
    }
}
