// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="GlyphKindToGlyphConverter.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Converters
{
    using System;
    using ViewModels;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Converts a <see cref="GlyphKind"/> to a <see cref="string"/> representing the value in
    /// Unicode Private Use Area (PUA) of either the <c>Segoe MDL2</c> or <c>SetMDL2</c> font.
    /// </summary>
    internal class GlyphKindToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((GlyphKind)value)
            {
                case GlyphKind.System:
                    return "\uE770";

                case GlyphKind.Devices:
                    return "\uE772";

                case GlyphKind.Phone:
                    return "\uE8EA";

                case GlyphKind.NetworkAndInternet:
                    return "\uE774";

                case GlyphKind.Personalization:
                    return "\uE771";

                case GlyphKind.Apps:
                    return "\uE71D";

                case GlyphKind.Accounts:
                    return "\uE77B";

                case GlyphKind.TimeAndLanguage:
                    return "\uE775";

                case GlyphKind.Gaming:
                    return "\uF20B";

                case GlyphKind.EaseOfAccess:
                    return "\uE776";

                case GlyphKind.Cortana:
                    return "\uE832";

                case GlyphKind.Privacy:
                    return "\uE72E";

                case GlyphKind.UpdateAndSecurity:
                    return "\uE895";

                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) =>
            throw new NotImplementedException();
    }
}
