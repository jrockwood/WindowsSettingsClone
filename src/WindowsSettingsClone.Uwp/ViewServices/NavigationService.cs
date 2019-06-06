// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewServices
{
    using System;
    using System.Collections.Generic;
    using ViewModels;
    using ViewModels.Utility;
    using ViewModels.ViewServices;
    using Views;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Implementation of <see cref="INavigationViewService"/> that uses the root frame to navigate to different pages.
    /// </summary>
    internal class NavigationService : INavigationViewService
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private static readonly Dictionary<Type, Type> s_viewModelToViewMap = new Dictionary<Type, Type>
        {
            [typeof(HomePageViewModel)] = typeof(HomePage),
            [typeof(SettingsGroupPageViewModel)] = typeof(SettingsGroupPage),
        };

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public NavigationService(Frame rootFrame) => RootFrame = Param.VerifyNotNull(rootFrame, nameof(rootFrame));

        //// ===========================================================================================================
        //// Events
        //// ===========================================================================================================

        public event EventHandler BackStackDepthChange;

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public int BackStackDepth => RootFrame.BackStackDepth;

        public bool CanGoBack => RootFrame.CanGoBack;

        public Frame RootFrame { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void NavigateTo(Type pageViewModelType, string pageViewModelState)
        {
            if (!s_viewModelToViewMap.TryGetValue(pageViewModelType, out Type viewType))
            {
                throw new InvalidOperationException($"Unknown view model type '{pageViewModelType.Name}'.");
            }

            RootFrame.Navigate(viewType, pageViewModelState);
            RaiseBackStackDepthChange();
        }

        public void GoBack()
        {
            RootFrame.GoBack();
            RaiseBackStackDepthChange();
        }

        private void RaiseBackStackDepthChange() => BackStackDepthChange?.Invoke(this, EventArgs.Empty);
    }
}
