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

        public HomePageViewModel(INavigationViewService navigationService)
        {
            Param.VerifyNotNull(navigationService, nameof(navigationService));
            GroupClick = new RelayCommand<HomePageGroup>(
                group => navigationService.NavigateTo(typeof(SettingsGroupPageViewModel), group.GroupKind.ToString()));
        }

        //// ===========================================================================================================
        //// Commands
        //// ===========================================================================================================

        public ICommand<HomePageGroup> GroupClick { get; }

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
                    GlyphInfo.System),
                new HomePageGroup(
                    Strings.DevicesGroupName,
                    Strings.DevicesGroupDescription,
                    SettingGroupKind.Devices,
                    GlyphInfo.Devices),
                new HomePageGroup(
                    Strings.PhoneGroupName,
                    Strings.PhoneGroupDescription,
                    SettingGroupKind.Phone,
                    GlyphInfo.CellPhone),
                new HomePageGroup(
                    Strings.NetworkAndInternetGroupName,
                    Strings.NetworkAndInternetGroupDescription,
                    SettingGroupKind.NetworkAndInternet,
                    GlyphInfo.Globe),
                new HomePageGroup(
                    Strings.PersonalizationGroupName,
                    Strings.PersonalizationGroupDescription,
                    SettingGroupKind.Personalization,
                    GlyphInfo.Personalize),
                new HomePageGroup(
                    Strings.AppsGroupName,
                    Strings.AppsGroupDescription,
                    SettingGroupKind.Apps,
                    GlyphInfo.AllApps),
                new HomePageGroup(
                    Strings.AccountsGroupName,
                    Strings.AccountsGroupDescription,
                    SettingGroupKind.Accounts,
                    GlyphInfo.Contact),
                new HomePageGroup(
                    Strings.TimeAndLanguageGroupName,
                    Strings.TimeAndLanguageGroupDescription,
                    SettingGroupKind.TimeAndLanguage,
                    GlyphInfo.TimeLanguage),
                new HomePageGroup(
                    Strings.GamingGroupName,
                    Strings.GamingGroupDescription,
                    SettingGroupKind.Gaming,
                    GlyphInfo.XboxLogo),
                new HomePageGroup(
                    Strings.EaseOfAccessGroupName,
                    Strings.EaseOfAccessGroupDescription,
                    SettingGroupKind.EaseOfAccess,
                    GlyphInfo.EaseOfAccess),
                new HomePageGroup(
                    Strings.CortanaGroupName,
                    Strings.CortanaGroupDescription,
                    SettingGroupKind.Cortana,
                    GlyphInfo.Cortana),
                new HomePageGroup(
                    Strings.PrivacyGroupName,
                    Strings.PrivacyGroupDescription,
                    SettingGroupKind.Privacy,
                    GlyphInfo.Lock),
                new HomePageGroup(
                    Strings.UpdateAndSecurityGroupName,
                    Strings.UpdateAndSecurityGroupDescription,
                    SettingGroupKind.UpdateAndSecurity,
                    GlyphInfo.Sync),
            });
    }
}
