// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RootPage.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Views
{
    using System;
    using ViewModels;
    using ViewModels.ViewServices;
    using ViewServices;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RootPage : Page
    {
        public RootPage()
        {
            InitializeComponent();

            NavigationService = new NavigationService(RootFrame);
            TitleBarViewModel = new TitleBarViewModel(NavigationService);
        }

        public INavigationViewService NavigationService { get; }

        public TitleBarViewModel TitleBarViewModel { get; }

        private void OnLoaded(object sender, RoutedEventArgs e) =>
            // When the navigation stack isn't restored navigate to the first page, configuring the new page by
            // passing required information as a navigation parameter
            NavigationService.NavigateTo(typeof(HomePageViewModel), null);

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e) =>
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
    }
}
