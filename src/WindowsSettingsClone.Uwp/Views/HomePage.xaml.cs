// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="HomePage.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Views
{
    using ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            // Cache the home page since there's no changing state and we should make it snappy.
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        public HomePageViewModel ViewModel { get; } = new HomePageViewModel(App.Current.NavigationService);

        private void OnSettingsGridViewItemClick(object sender, ItemClickEventArgs e)
        {
            var homePageGroup = (HomePageGroup)e.ClickedItem;
            ViewModel.GroupClick.Execute(homePageGroup);
        }
    }
}
