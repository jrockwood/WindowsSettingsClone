// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="PageViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    using Utility;

    /// <summary>
    /// Abstract base class for all page ViewModels.
    /// </summary>
    public abstract class PageViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        protected PageViewModel(string name) => Name = Param.VerifyString(name, nameof(name));

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public string Name { get; }
    }
}
