// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="GlyphInfoToFontFamilyConverter.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.Converters
{
    using System;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;
    using ViewModels;

    /// <summary>
    /// Converts a <see cref="GlyphInfo"/> to a <see cref="FontFamily"/> that contains the glyph.
    /// </summary>
    internal sealed class GlyphInfoToFontFamilyConverter : IValueConverter
    {
        private static readonly FontFamily s_defaultSymbolFontFamily = new FontFamily("Segoe MDL2 Assets");

        private static readonly FontFamily s_settingsFontFamily =
            new FontFamily("Assets/SetMDL2.ttf#Settings MDL2 Assets");

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var glyphInfo = (GlyphInfo)value;
            return glyphInfo.FontFamilyName == GlyphInfo.SettingsMdl2FontFamilyName
                ? s_settingsFontFamily
                : s_defaultSymbolFontFamily;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) =>
            throw new NotImplementedException();
    }
}
