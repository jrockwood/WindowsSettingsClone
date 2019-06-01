// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="HomePageViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the ViewModel for the MainPage view.
    /// </summary>
    public class HomePageViewModel : PageViewModel
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public HomePageViewModel()
            : base("Main Page")
        {
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public bool IsWindowsActivated { get; } = true;

        public ReadOnlyObservableCollection<SettingsGroupViewModel> Groups { get; } =
            new ReadOnlyObservableCollection<SettingsGroupViewModel>(
                new ObservableCollection<SettingsGroupViewModel>
                {
                    new SettingsGroupViewModel(
                        "System",
                        "Display, sound, notifications, power",
                        GlyphKind.System,
                        new[] {new SettingPageViewModel("Display", GlyphKind.Accounts),}),
                    new SettingsGroupViewModel(
                        "Devices",
                        "Bluetooth, printers, mouse",
                        GlyphKind.Devices,
                        new SettingPageViewModel[0]),
                    new SettingsGroupViewModel(
                        "Phone",
                        "Link your Android, iPhone",
                        GlyphKind.Phone,
                        new SettingPageViewModel[0]),
                    new SettingsGroupViewModel(
                        "Network & Internet",
                        "Wi-Fi, airplane mode, VPN",
                        GlyphKind.NetworkAndInternet,
                        new SettingPageViewModel[0]),
                    new SettingsGroupViewModel(
                        "Personalization",
                        "Background, lock screen, colors",
                        GlyphKind.Personalization,
                        new SettingPageViewModel[0]),
                    new SettingsGroupViewModel(
                        "Apps",
                        "Uninstall, defaults, optional features",
                        GlyphKind.Apps,
                        new SettingPageViewModel[0]),
                    new SettingsGroupViewModel(
                        "Accounts",
                        "Your accounts, email, sync, work, family",
                        GlyphKind.Accounts,
                        new SettingPageViewModel[0]),
                    new SettingsGroupViewModel(
                        "Time & Language",
                        "Speech, region, date",
                        GlyphKind.TimeAndLanguage,
                        new SettingPageViewModel[0]),
                    new SettingsGroupViewModel(
                        "Gaming",
                        "Game bar, captures, broadcasting, Game Mode",
                        GlyphKind.Gaming,
                        new SettingPageViewModel[0]),
                    new SettingsGroupViewModel(
                        "Ease of Access",
                        "Narrator, magnifier, high contrast",
                        GlyphKind.EaseOfAccess,
                        new SettingPageViewModel[0]),
                    new SettingsGroupViewModel(
                        "Cortana",
                        "Cortana language, permissions, notifications",
                        GlyphKind.Cortana,
                        new SettingPageViewModel[0]),
                    new SettingsGroupViewModel(
                        "Privacy",
                        "Location, camera",
                        GlyphKind.Privacy,
                        new SettingPageViewModel[0]),
                    new SettingsGroupViewModel(
                        "Update & Security",
                        "Windows Update, recovery, backup",
                        GlyphKind.UpdateAndSecurity,
                        new SettingPageViewModel[0]),
                });
    }
}
