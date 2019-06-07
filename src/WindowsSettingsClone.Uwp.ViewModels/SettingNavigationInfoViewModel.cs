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

        private SettingNavigationInfoViewModel(
            string displayName,
            SettingEditorKind editorKind,
            GlyphInfo glyphInfo,
            bool isSelected,
            bool isHeader)
        {
            DisplayName = Param.VerifyString(displayName, nameof(displayName));
            EditorKind = editorKind;
            GlyphInfo = glyphInfo;
            IsSelected = isSelected;
            IsHeader = isHeader;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public SettingEditorKind EditorKind { get; }
        public GlyphInfo GlyphInfo { get; }
        public string DisplayName { get; }
        public bool IsHeader { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static SettingNavigationInfoViewModel Create(
            string displayName,
            SettingEditorKind editorKind,
            GlyphInfo glyphInfo,
            bool isSelected = false) =>
            // The auto-formatter doesn't put a space here and it's hard to read
            new SettingNavigationInfoViewModel(
                displayName,
                editorKind,
                Param.VerifyNotNull(glyphInfo, nameof(glyphInfo)),
                isSelected,
                isHeader: false);

        public static SettingNavigationInfoViewModel CreateHeader(string displayName) =>
            new SettingNavigationInfoViewModel(
                displayName,
                SettingEditorKind.About,
                glyphInfo: null,
                isSelected: false,
                isHeader: true);
    }
}
