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
    public class HomePageViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public bool IsWindowsActivated { get; } = true;

        public ReadOnlyObservableCollection<SettingsGroupViewModel> Groups { get; } =
            new ReadOnlyObservableCollection<SettingsGroupViewModel>(new ObservableCollection<SettingsGroupViewModel>
            {
                new SettingsGroupViewModel("System", "Display, sound, notifications, power", GlyphKind.System),
                new SettingsGroupViewModel("Devices", "Bluetooth, printers, mouse", GlyphKind.Devices),
                new SettingsGroupViewModel("Phone", "Link your Android, iPhone", GlyphKind.Phone),
                new SettingsGroupViewModel("Network & Internet", "Wi-Fi, airplane mode, VPN",
                    GlyphKind.NetworkAndInternet),
                new SettingsGroupViewModel("Personalization", "Background, lock screen, colors",
                    GlyphKind.Personalization),
                new SettingsGroupViewModel("Apps", "Uninstall, defaults, optional features", GlyphKind.Apps),
                new SettingsGroupViewModel("Accounts", "Your accounts, email, sync, work, family", GlyphKind.Accounts),
                new SettingsGroupViewModel("Time & Language", "Speech, region, date", GlyphKind.TimeAndLanguage),
                new SettingsGroupViewModel("Gaming", "Game bar, captures, broadcasting, Game Mode", GlyphKind.Gaming),
                new SettingsGroupViewModel("Ease of Access", "Narrator, magnifier, high contrast",
                    GlyphKind.EaseOfAccess),
                new SettingsGroupViewModel("Cortana", "Cortana language, permissions, notifications",
                    GlyphKind.Cortana),
                new SettingsGroupViewModel("Privacy", "Location, camera", GlyphKind.Privacy),
                new SettingsGroupViewModel("Update & Security", "Windows Update, recovery, backup",
                    GlyphKind.UpdateAndSecurity),
            });
    }
}
