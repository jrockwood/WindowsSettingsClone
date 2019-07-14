// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryPage.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.Views
{
    using System;
    using ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class CategoryPage : Page
    {
        public CategoryPage()
        {
            InitializeComponent();
        }

        public CategoryPageViewModel ViewModel { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // The name of the group is passed in via the event args, which we can use to construct a view model.
            var category = (CategoryKind)Enum.Parse(typeof(CategoryKind), (string)e.Parameter);
            ViewModel = CategoryPageViewModel.CreateFromCategoryKind(
                category,
                App.Current.Logger,
                App.Current.NavigationService,
                App.Current.ThreadDispatcher,
                App.Current.RegistryReadService,
                App.Current.RegistryWriteService);

            SettingsGroupNavigationView.ViewModel = ViewModel.Navigation;
        }
    }
}
