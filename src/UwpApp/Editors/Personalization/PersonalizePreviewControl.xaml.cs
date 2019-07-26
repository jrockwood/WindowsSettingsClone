// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonalizePreviewControl.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.Editors.Personalization
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    public sealed partial class PersonalizePreviewControl : UserControl
    {
        public PersonalizePreviewControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ShowWindowPreviewProperty = DependencyProperty.Register(
            nameof(ShowWindowPreview),
            typeof(bool),
            typeof(PersonalizePreviewControl),
            new PropertyMetadata(default(bool)));

        public bool ShowWindowPreview
        {
            get => (bool)GetValue(ShowWindowPreviewProperty);
            set => SetValue(ShowWindowPreviewProperty, value);
        }

        public static readonly DependencyProperty DesktopWallpaperImageSourceProperty = DependencyProperty.Register(
            nameof(DesktopWallpaperImageSource),
            typeof(ImageSource),
            typeof(PersonalizePreviewControl),
            new PropertyMetadata(default(ImageSource)));

        public ImageSource DesktopWallpaperImageSource
        {
            get => (ImageSource)GetValue(DesktopWallpaperImageSourceProperty);
            set => SetValue(DesktopWallpaperImageSourceProperty, value);
        }
    }
}
