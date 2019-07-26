// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FilePathToImageSourceConverter.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.Converters
{
    using System;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// Converts a file path to an <see cref="BitmapSource"/>.
    /// </summary>
    public sealed class FilePathToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var bitmapImage = new BitmapImage();
            SetImageSourceAsync((string)value, bitmapImage);
            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }

        private static async void SetImageSourceAsync(string filePath, BitmapSource bitmapImage)
        {
            if (filePath == null)
            {
                return;
            }

            try
            {
                StorageFile file = await StorageFile.GetFileFromPathAsync(filePath);
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    await bitmapImage.SetSourceAsync(fileStream);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
