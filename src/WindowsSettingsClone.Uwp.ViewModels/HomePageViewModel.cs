// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="HomePageViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Utility;
    using ViewServices;

    /// <summary>
    /// Represents the ViewModel for the MainPage view.
    /// </summary>
    public class HomePageViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public HomePageViewModel(INavigationViewService navigationService) =>
            Param.VerifyNotNull(navigationService, nameof(navigationService));

        //// ===========================================================================================================
        //// Commands
        //// ===========================================================================================================

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public bool IsWindowsActivated { get; } = true;

        public IReadOnlyList<HomePageGroup> Groups { get; } = new ReadOnlyCollection<HomePageGroup>(
            new[]
            {
                new HomePageGroup(
                    Strings.SystemGroupName,
                    Strings.SystemGroupDescription,
                    SettingGroupKind.System,
                    GlyphKind.System),
                new HomePageGroup(
                    Strings.DevicesGroupName,
                    Strings.DevicesGroupDescription,
                    SettingGroupKind.Devices,
                    GlyphKind.Devices),
                new HomePageGroup(
                    Strings.PhoneGroupName,
                    Strings.PhoneGroupDescription,
                    SettingGroupKind.Phone,
                    GlyphKind.Phone),
                new HomePageGroup(
                    Strings.NetworkAndInternetGroupName,
                    Strings.NetworkAndInternetGroupDescription,
                    SettingGroupKind.NetworkAndInternet,
                    GlyphKind.NetworkAndInternet),
                new HomePageGroup(
                    Strings.PersonalizationGroupName,
                    Strings.PersonalizationGroupDescription,
                    SettingGroupKind.Personalization,
                    GlyphKind.Personalization),
                new HomePageGroup(
                    Strings.AppsGroupName,
                    Strings.AppsGroupDescription,
                    SettingGroupKind.Apps,
                    GlyphKind.Apps),
                new HomePageGroup(
                    Strings.AccountsGroupName,
                    Strings.AccountsGroupDescription,
                    SettingGroupKind.Accounts,
                    GlyphKind.Accounts),
                new HomePageGroup(
                    Strings.TimeAndLanguageGroupName,
                    Strings.TimeAndLanguageGroupDescription,
                    SettingGroupKind.TimeAndLanguage,
                    GlyphKind.TimeAndLanguage),
                new HomePageGroup(
                    Strings.GamingGroupName,
                    Strings.GamingGroupDescription,
                    SettingGroupKind.Gaming,
                    GlyphKind.Gaming),
                new HomePageGroup(
                    Strings.EaseOfAccessGroupName,
                    Strings.EaseOfAccessGroupDescription,
                    SettingGroupKind.EaseOfAccess,
                    GlyphKind.EaseOfAccess),
                new HomePageGroup(
                    Strings.CortanaGroupName,
                    Strings.CortanaGroupDescription,
                    SettingGroupKind.Cortana,
                    GlyphKind.Cortana),
                new HomePageGroup(
                    Strings.PrivacyGroupName,
                    Strings.PrivacyGroupDescription,
                    SettingGroupKind.Privacy,
                    GlyphKind.Privacy),
                new HomePageGroup(
                    Strings.UpdateAndSecurityGroupName,
                    Strings.UpdateAndSecurityGroupDescription,
                    SettingGroupKind.UpdateAndSecurity,
                    GlyphKind.UpdateAndSecurity),
            });
    }
}
