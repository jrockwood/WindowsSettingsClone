// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsGroupViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
{
    internal class SettingsGroupViewModel
    {
        public SettingsGroupViewModel(string name, string description, string glyph)
        {
            Name = name;
            Description = description;
            Glyph = glyph;
        }

        public string Name { get; }
        public string Description { get; }
        public string Glyph { get; }
    }
}