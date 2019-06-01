// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingNavigationInfo.cs" company="Justin Rockwood">
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
    public sealed class SettingNavigationInfo
    {
        public SettingNavigationInfo(string name, GlyphKind glyph, SettingEditorKind editorKind)
        {
            Name = Param.VerifyString(name, nameof(name));
            Glyph = glyph;
            EditorKind = editorKind;
        }

        public string Name { get; }
        public GlyphKind Glyph { get; }
        public SettingEditorKind EditorKind { get; }
    }
}
