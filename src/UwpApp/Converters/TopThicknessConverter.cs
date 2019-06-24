// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TopThicknessConverter.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.Converters
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Converts a <see cref="double"/> (or a string representing a <see cref="double"/> to a <see cref="Thickness"/>
    /// with the top set to the converted value.
    /// </summary>
    internal class TopThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double topValue = value is string s ? double.Parse(s) : (double)value;
            return new Thickness(0, topValue, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
