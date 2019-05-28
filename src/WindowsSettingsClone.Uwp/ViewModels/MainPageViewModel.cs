// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPageViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
{
    using System.Collections.ObjectModel;

    internal class MainPageViewModel
    {
        public ReadOnlyObservableCollection<SettingsGroupViewModel> Groups { get; } =
            new ReadOnlyObservableCollection<SettingsGroupViewModel>(new ObservableCollection<SettingsGroupViewModel>
            {
                new SettingsGroupViewModel("System", "Display, sound, notifications, power", "\uE770"),
                new SettingsGroupViewModel("Devices", "Bluetooth, printers, mouse", "\uE772"),
                new SettingsGroupViewModel("Phone", "Link your Android, iPhone", "\uE8EA"),
                new SettingsGroupViewModel("Network & Internet", "Wi-Fi, airplane mode, VPN", "\uE774"),
                new SettingsGroupViewModel("Personalization", "Background, lock screen, colors", "\uE771"),
                new SettingsGroupViewModel("Apps", "Uninstall, defaults, optional features", "\uE71D"),
                new SettingsGroupViewModel("Accounts", "Your accounts, email, sync, work, family", "\uE77B"),
                new SettingsGroupViewModel("Time & Language", "Speech, region, date", "\uE775"),
                new SettingsGroupViewModel("Gaming", "Game bar, captures, broadcasting, Game Mode", "\uE7FC"),
                new SettingsGroupViewModel("Ease of Access", "Narrator, magnifier, high contrast", "\uE776"),
                new SettingsGroupViewModel("Cortana", "Cortana language, permissions, notifications", "\uEA3A"),
                new SettingsGroupViewModel("Privacy", "Location, camera", "\uE72E"),
                new SettingsGroupViewModel("Update & Security", "Windows Update, recovery, backup", "\uE895"),
            });
    }
}