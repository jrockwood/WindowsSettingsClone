﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="HomePageGroup.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    using Utility;

    public class HomePageGroup
    {
        public HomePageGroup(string groupName, string description, SettingGroupKind groupKind, GlyphKind glyph)
        {
            GroupName = Param.VerifyString(groupName, nameof(groupName));
            Description = Param.VerifyString(description, nameof(description));
            GroupKind = groupKind;
            Glyph = glyph;
        }

        public SettingGroupKind GroupKind { get; }
        public string GroupName { get; }
        public string Description { get; }
        public GlyphKind Glyph { get; }
    }
}
