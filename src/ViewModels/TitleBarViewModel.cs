// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TitleBarViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
{
    using System;
    using System.Windows.Input;
    using ServiceContracts.ViewServices;
    using Shared.Diagnostics;
    using Utility;

    public sealed class TitleBarViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly RelayCommand _backCommand;
        private readonly INavigationViewService _navigationViewService;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TitleBarViewModel(INavigationViewService navigationViewService)
        {
            _navigationViewService = Param.VerifyNotNull(navigationViewService, nameof(navigationViewService));

            _navigationViewService.BackStackDepthChange += OnBackStackDepthChange;

            _backCommand = new RelayCommand(ExecuteBackAction, CanGoBack);
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ICommand BackCommand => _backCommand;

        public bool CanGoBack => _navigationViewService.CanGoBack;

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        private void ExecuteBackAction()
        {
            if (_navigationViewService.CanGoBack)
            {
                _navigationViewService.GoBack();
            }
        }

        private void OnBackStackDepthChange(object sender, EventArgs args)
        {
            OnPropertyChanged(nameof(CanGoBack));
            _backCommand.Enabled = CanGoBack;
        }
    }
}
