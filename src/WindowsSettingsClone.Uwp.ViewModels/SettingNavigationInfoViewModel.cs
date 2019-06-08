// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingNavigationInfoViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    using Utility;

    /// <summary>
    /// Represents enough information about an individual setting that it can be displayed on a page navigation control
    /// to invoke the proper setting editor.
    /// </summary>
    public sealed class SettingNavigationInfoViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private bool _isSelected;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public SettingNavigationInfoViewModel(
            string displayName,
            SettingsEditorKind editorKind,
            GlyphInfo glyphInfo,
            bool isSelected = false,
            string headerDisplayName = null)
        {
            DisplayName = Param.VerifyString(displayName, nameof(displayName));
            EditorKind = editorKind;
            GlyphInfo = Param.VerifyNotNull(glyphInfo, nameof(glyphInfo));
            IsSelected = isSelected;
            HeaderDisplayName = headerDisplayName;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public SettingsEditorKind EditorKind { get; }
        public GlyphInfo GlyphInfo { get; }
        public string DisplayName { get; }
        public string HeaderDisplayName { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
