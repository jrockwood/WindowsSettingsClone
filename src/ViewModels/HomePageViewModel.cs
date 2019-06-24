// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="HomePageViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
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
            CategoryClick = new RelayCommand<HomePageCategory>(
                category => navigationService.NavigateTo(
                    typeof(CategoryPageViewModel),
                    category.Category.ToString()));
        }

        //// ===========================================================================================================
        //// Commands
        //// ===========================================================================================================

        public ICommand<HomePageCategory> CategoryClick { get; }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public bool IsWindowsActivated { get; } = true;

        public IReadOnlyList<HomePageCategory> Categories { get; } = new ReadOnlyCollection<HomePageCategory>(
            new[]
            {
                new HomePageCategory(
                    Strings.SystemCategoryName,
                    Strings.SystemCategoryDescription,
                    CategoryKind.System,
                    GlyphInfo.System),
                new HomePageCategory(
                    Strings.DevicesCategoryName,
                    Strings.DevicesCategoryDescription,
                    CategoryKind.Devices,
                    GlyphInfo.Devices),
                new HomePageCategory(
                    Strings.PhoneCategoryName,
                    Strings.PhoneCategoryDescription,
                    CategoryKind.Phone,
                    GlyphInfo.CellPhone),
                new HomePageCategory(
                    Strings.NetworkAndInternetCategoryName,
                    Strings.NetworkAndInternetCategoryDescription,
                    CategoryKind.NetworkAndInternet,
                    GlyphInfo.Globe),
                new HomePageCategory(
                    Strings.PersonalizationCategoryName,
                    Strings.PersonalizationCategoryDescription,
                    CategoryKind.Personalization,
                    GlyphInfo.Personalize),
                new HomePageCategory(
                    Strings.AppsCategoryName,
                    Strings.AppsCategoryDescription,
                    CategoryKind.Apps,
                    GlyphInfo.AllApps),
                new HomePageCategory(
                    Strings.AccountsCategoryName,
                    Strings.AccountsCategoryDescription,
                    CategoryKind.Accounts,
                    GlyphInfo.Contact),
                new HomePageCategory(
                    Strings.TimeAndLanguageCategoryName,
                    Strings.TimeAndLanguageCategoryDescription,
                    CategoryKind.TimeAndLanguage,
                    GlyphInfo.TimeLanguage),
                new HomePageCategory(
                    Strings.GamingCategoryName,
                    Strings.GamingCategoryDescription,
                    CategoryKind.Gaming,
                    GlyphInfo.XboxLogo),
                new HomePageCategory(
                    Strings.EaseOfAccessCategoryName,
                    Strings.EaseOfAccessCategoryDescription,
                    CategoryKind.EaseOfAccess,
                    GlyphInfo.EaseOfAccess),
                new HomePageCategory(
                    Strings.SearchCategoryName,
                    Strings.SearchCategoryDescription,
                    CategoryKind.Search,
                    GlyphInfo.Search),
                new HomePageCategory(
                    Strings.CortanaCategoryName,
                    Strings.CortanaCategoryDescription,
                    CategoryKind.Cortana,
                    GlyphInfo.Cortana),
                new HomePageCategory(
                    Strings.PrivacyCategoryName,
                    Strings.PrivacyCategoryDescription,
                    CategoryKind.Privacy,
                    GlyphInfo.Lock),
                new HomePageCategory(
                    Strings.UpdateAndSecurityCategoryName,
                    Strings.UpdateAndSecurityCategoryDescription,
                    CategoryKind.UpdateAndSecurity,
                    GlyphInfo.Sync),
            });
    }
}
