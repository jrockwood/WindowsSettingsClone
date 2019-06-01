// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="GlyphKindToFontFamilyConverter.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Converters
{
    using System;
    using ViewModels;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Converts a <see cref="GlyphKind"/> to a <see cref="FontFamily"/> that contains the glyph.
    /// </summary>
    internal sealed class GlyphKindToFontFamilyConverter : IValueConverter
    {
        private static readonly FontFamily s_defaultSymbolFontFamily = new FontFamily("Segoe MDL2 Assets");

        private static readonly FontFamily s_settingsFontFamily =
            new FontFamily("Assets/SetMDL2.ttf#Settings MDL2 Assets");

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((GlyphKind)value)
            {
                case GlyphKind.System:
                case GlyphKind.Devices:
                case GlyphKind.Phone:
                case GlyphKind.NetworkAndInternet:
                case GlyphKind.Personalization:
                case GlyphKind.Apps:
                case GlyphKind.Accounts:
                case GlyphKind.TimeAndLanguage:
                case GlyphKind.EaseOfAccess:
                case GlyphKind.Privacy:
                case GlyphKind.UpdateAndSecurity:
                    return s_defaultSymbolFontFamily;

                case GlyphKind.Gaming:
                case GlyphKind.Cortana:
                    return s_settingsFontFamily;

                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) =>
            throw new NotImplementedException();
    }
}
