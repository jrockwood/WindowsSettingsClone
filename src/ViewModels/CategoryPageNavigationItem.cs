// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryPageNavigationItem.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
{
    using Shared.Utility;

    /// <summary>
    /// Represents enough information about an individual setting that it can be displayed on a page navigation control
    /// to invoke the proper setting editor.
    /// </summary>
    public sealed class CategoryPageNavigationItem : BaseViewModel
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private bool _isSelected;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public CategoryPageNavigationItem(
            string displayName,
            EditorKind editorKind,
            GlyphInfo glyphInfo,
            string headerDisplayName = null)
        {
            DisplayName = Param.VerifyString(displayName, nameof(displayName));
            EditorKind = editorKind;
            GlyphInfo = Param.VerifyNotNull(glyphInfo, nameof(glyphInfo));
            HeaderDisplayName = headerDisplayName;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public EditorKind EditorKind { get; }
        public GlyphInfo GlyphInfo { get; }
        public string DisplayName { get; }

        /// <summary>
        /// The display name of the grouped section if the setting is within a grouped section in the navigation view, or
        /// null if the setting is not within a grouped section.
        /// </summary>
        public string HeaderDisplayName { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
