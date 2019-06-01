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
                new HomePageGroup("System", "Display, sound, notifications, power", GlyphKind.System),
                new HomePageGroup("Devices", "Bluetooth, printers, mouse", GlyphKind.Devices),
                new HomePageGroup("Phone", "Link your Android, iPhone", GlyphKind.Phone),
                new HomePageGroup("Network & Internet", "Wi-Fi, airplane mode, VPN", GlyphKind.NetworkAndInternet),
                new HomePageGroup("Personalization", "Background, lock screen, colors", GlyphKind.Personalization),
                new HomePageGroup("Apps", "Uninstall, defaults, optional features", GlyphKind.Apps),
                new HomePageGroup("Accounts", "Your accounts, email, sync, work, family", GlyphKind.Accounts),
                new HomePageGroup("Time & Language", "Speech, region, date", GlyphKind.TimeAndLanguage),
                new HomePageGroup("Gaming", "Game bar, captures, broadcasting, Game Mode", GlyphKind.Gaming),
                new HomePageGroup("Ease of Access", "Narrator, magnifier, high contrast", GlyphKind.EaseOfAccess),
                new HomePageGroup("Cortana", "Cortana language, permissions, notifications", GlyphKind.Cortana),
                new HomePageGroup("Privacy", "Location, camera", GlyphKind.Privacy),
                new HomePageGroup(
                    "Update & Security",
                    "Windows Update, recovery, backup",
                    GlyphKind.UpdateAndSecurity),
            });
    }
}
