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
    using Windows.UI.Xaml.Media.Animation;

    /// <summary>
    /// Implementation of <see cref="INavigationViewService"/> that uses the root frame to navigate to different pages.
    /// </summary>
    internal class NavigationService : INavigationViewService
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private static readonly Dictionary<string, Type> s_viewModelToViewMap = new Dictionary<string, Type>
        {
            [nameof(HomePageViewModel)] = typeof(HomePage),
        };

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public NavigationService(Frame rootFrame) => RootFrame = Param.VerifyNotNull(rootFrame, nameof(rootFrame));

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public Frame RootFrame { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void NavigateTo(string pageViewModelTypeName, string pageViewModelState)

        {
            if (!s_viewModelToViewMap.TryGetValue(pageViewModelTypeName, out Type viewType))
            {
                throw new InvalidOperationException($"Unknown view model type '{pageViewModelTypeName}'.");
            }

            RootFrame.Navigate(viewType, pageViewModelState, new EntranceNavigationTransitionInfo());
        }
    }
}
