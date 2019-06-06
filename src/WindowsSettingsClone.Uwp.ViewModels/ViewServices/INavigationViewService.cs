// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="INavigationViewService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.ViewServices
{
    using System;

    /// <summary>
    /// Service contract for navigating to various pages within the application. Typically implemented in the view layer.
    /// </summary>
    public interface INavigationViewService
    {
        void NavigateTo(Type pageViewModelType, string pageViewModelState);
    }
}
