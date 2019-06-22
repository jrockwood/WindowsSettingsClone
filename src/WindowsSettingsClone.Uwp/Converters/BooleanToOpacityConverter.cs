// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanToOpacityConverter.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Converts true to opaque (1.0) and false to transparent (0.0).
    /// </summary>
    internal class BooleanToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isOpaque = value is string s ? bool.Parse(s) : (bool)value;
            return isOpaque ? 1.0 : 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double opacity = value is string s ? double.Parse(s) : (double)value;
            return Math.Abs(opacity - 1.0) < double.Epsilon;
        }
    }
}
