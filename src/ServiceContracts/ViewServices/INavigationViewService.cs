// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="INavigationViewService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.ViewServices
{
    using System;

    /// <summary>
    /// Service contract for navigating to various pages within the application. Typically implemented in the view layer.
    /// </summary>
    public interface INavigationViewService
    {
        //// ===========================================================================================================
        //// Events
        //// ===========================================================================================================

        event EventHandler BackStackDepthChange;

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        /// <summary>
        /// Gets a value that indicates whether there is at least one entry in back navigation history.
        /// </summary>
        bool CanGoBack { get; }

        int BackStackDepth { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        void GoBack();

        void NavigateTo(Type pageViewModelType, string pageViewModelState);
    }
}
