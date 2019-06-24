// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryPageNavigationViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Utility;
    using ViewServices;

    public class CategoryPageNavigationViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private CategoryPageNavigationItem _selectedItem;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public CategoryPageNavigationViewModel(
            INavigationViewService navigationViewService,
            CategoryKind categoryKind,
            string groupName,
            IEnumerable<CategoryPageNavigationItem> settings = null)
        {
            Param.VerifyNotNull(navigationViewService, nameof(navigationViewService));

            CategoryKind = categoryKind;
            GroupName = Param.VerifyString(groupName, nameof(groupName));
            Settings = new ReadOnlyCollection<CategoryPageNavigationItem>(settings?.ToList() ?? new List<CategoryPageNavigationItem>());

            IsGrouped = Settings.Any(vm => vm.HeaderDisplayName != null);
            GroupedSettings = Settings.GroupBy(
                setting => setting.HeaderDisplayName,
                (key, items) => new CategoryPageNavigationGroupedItem(key, items));

            HomeCommand = new RelayCommand(() => navigationViewService.NavigateTo(typeof(HomePageViewModel), null));
        }

        //// ===========================================================================================================
        //// Commands
        //// ===========================================================================================================

        public ICommand HomeCommand { get; }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public CategoryKind CategoryKind { get; }
        public string GroupName { get; }
        public ReadOnlyCollection<CategoryPageNavigationItem> Settings { get; }

        /// <summary>
        /// Gets a value indicating whether the individual settings are grouped in the navigation pane.
        /// </summary>
        public bool IsGrouped { get; }

        /// <summary>
        /// Gets an enumerable of groupings of settings.
        /// </summary>
        public IEnumerable<CategoryPageNavigationGroupedItem> GroupedSettings { get; }

        public CategoryPageNavigationItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                CategoryPageNavigationItem oldSelectedItem = _selectedItem;
                if (SetProperty(ref _selectedItem, value))
                {
                    if (oldSelectedItem != null)
                    {
                        oldSelectedItem.IsSelected = false;
                    }

                    value.IsSelected = true;
                }
            }
        }
    }
}
