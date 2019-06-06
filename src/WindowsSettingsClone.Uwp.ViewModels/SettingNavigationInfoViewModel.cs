﻿// ---------------------------------------------------------------------------------------------------------------------
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

        public SettingNavigationInfoViewModel(SettingEditorKind editorKind, GlyphInfo glyphInfo, string name)
        {
            EditorKind = editorKind;
            GlyphInfo = Param.VerifyNotNull(glyphInfo, nameof(glyphInfo));
            Name = Param.VerifyString(name, nameof(name));
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public SettingEditorKind EditorKind { get; }
        public GlyphInfo GlyphInfo { get; }
        public string Name { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
