// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingGroupNavigationView.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.Views
{
    using System.Linq;
    using Windows.UI.Xaml.Controls;
    using ViewModels;

    public sealed partial class CategoryPageNavigationView : UserControl
    {
        private CategoryPageNavigationViewModel _viewModel;

        public CategoryPageNavigationView()
        {
            InitializeComponent();
        }

        public CategoryPageNavigationViewModel ViewModel
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

        private void OnSettingsNavigationListViewSelectionChanged(object sender, SelectionChangedEventArgs e) =>
            ViewModel.SelectedItem = e.AddedItems.Cast<CategoryPageNavigationItem>().Single();
    }
}
