﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsGroupPage.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Views
{
    using System;
    using ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class SettingsGroupPage : Page
    {
        public SettingsGroupPage() => InitializeComponent();

        public SettingsGroupPageViewModel ViewModel { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // The name of the group is passed in via the event args, which we can use to construct a view model.
            var groupKind = (SettingGroupKind)Enum.Parse(typeof(SettingGroupKind), (string)e.Parameter);
            ViewModel = SettingsGroupPageViewModel.CreateFromGroupKind(groupKind, App.Current.NavigationService);

            SettingsGroupNavigationView.ViewModel = ViewModel;
        }
    }
}
