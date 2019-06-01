// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsGroupViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Utility;

    /// <summary>
    /// Represents a group of settings that has its own page and is listed on the main page.
    /// </summary>
    public class SettingsGroupViewModel
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public SettingsGroupViewModel(
            string name,
            string description,
            GlyphKind glyph,
            IEnumerable<SettingPageViewModel> pages)
        {
            Name = Param.VerifyString(name, nameof(name));
            Description = Param.VerifyString(description, nameof(description));
            Glyph = glyph;
            Pages = new ReadOnlyObservableCollection<SettingPageViewModel>(
                new ObservableCollection<SettingPageViewModel>(Param.VerifyNotNull(pages, nameof(pages))));
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public string Name { get; }
        public string Description { get; }
        public GlyphKind Glyph { get; }
        public ReadOnlyObservableCollection<SettingPageViewModel> Pages { get; }
    }
}
