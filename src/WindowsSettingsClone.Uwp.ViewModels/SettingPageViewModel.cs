// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingPageViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    /// <summary>
    /// Represents a single setting page within a settings group.
    /// </summary>
    public sealed class SettingPageViewModel : PageViewModel
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public SettingPageViewModel(string name, GlyphKind glyph)
            : base(name) => Glyph = glyph;

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public GlyphKind Glyph { get; }
    }
}
