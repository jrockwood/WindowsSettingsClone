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
                            new SettingNavigationInfo("Display", GlyphKind.Display, SettingEditorKind.Display),
                            new SettingNavigationInfo("Sound", GlyphKind.Speaker, SettingEditorKind.Sound),
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

            return new SettingsGroupPageViewModel(SettingGroupKind.Accounts, "Accounts", new SettingNavigationInfo[0]);
        }
    }
}
