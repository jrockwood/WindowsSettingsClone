// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="HomePageCategory.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
{
    using Shared.Utility;

    public class HomePageCategory
    {
        public HomePageCategory(
            string categoryDisplayName,
            string description,
            CategoryKind category,
            GlyphInfo glyphInfo)
        {
            CategoryDisplayName = Param.VerifyString(categoryDisplayName, nameof(categoryDisplayName));
            Description = Param.VerifyString(description, nameof(description));
            Category = category;
            GlyphInfo = glyphInfo;
        }

        public CategoryKind Category { get; }
        public string CategoryDisplayName { get; }
        public string Description { get; }
        public GlyphInfo GlyphInfo { get; }
    }
}
