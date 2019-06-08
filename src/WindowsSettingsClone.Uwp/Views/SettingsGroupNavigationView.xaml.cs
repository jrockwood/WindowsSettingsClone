// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingGroupNavigationView.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Views
{
    using ViewModels;
    using Windows.UI.Xaml.Controls;

    public sealed partial class SettingsGroupNavigationView : UserControl
    {
        private SettingsGroupPageViewModel _viewModel;

        public SettingsGroupNavigationView() => InitializeComponent();

        public SettingsGroupPageViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                if (_viewModel == value)
                {
                    return;
                }

                _viewModel = value;

                NavigationItemsCollectionViewSource.IsSourceGrouped = ViewModel.IsGrouped;
                NavigationItemsCollectionViewSource.Source =
                    ViewModel.IsGrouped ? ViewModel.GroupedSettings : (object)ViewModel.Settings;

                SettingsNavigationListView.ItemsSource = NavigationItemsCollectionViewSource.View;
            }
        }
    }
}
