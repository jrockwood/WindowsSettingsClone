// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsGroupViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
{
    /// <summary>
    /// Represents a group of settings that has its own page and is listed on the main page.
    /// </summary>
    public class SettingsGroupViewModel
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public SettingsGroupViewModel(string name, string description, GlyphKind glyph)
        {
            Name = name;
            Description = description;
            Glyph = glyph;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public string Name { get; }
        public string Description { get; }
        public GlyphKind Glyph { get; }
    }
}
