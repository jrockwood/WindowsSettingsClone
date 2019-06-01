// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="HomePage.xaml.cs" company="Justin Rockwood">
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

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage() => InitializeComponent();

        public HomePageViewModel ViewModel { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e) =>
            ViewModel = e.Parameter as HomePageViewModel ??
                        throw new InvalidOperationException($"Missing required {nameof(HomePageViewModel)}");

        private void OnSettingsGridViewSelectionChanged(object sender, SelectionChangedEventArgs e) =>
            // Don't actually select anything - treat it as a click
            SettingsGridView.SelectedItem = null;
    }
}
