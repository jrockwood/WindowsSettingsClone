// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonalizePreviewControl.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Editors.Personalization
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class PersonalizePreviewControl : UserControl
    {
        public PersonalizePreviewControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ShowWindowPreviewProperty = DependencyProperty.Register(
            "ShowWindowPreview",
            typeof(bool),
            typeof(PersonalizePreviewControl),
            new PropertyMetadata(default(bool)));

        public bool ShowWindowPreview
        {
            get => (bool)GetValue(ShowWindowPreviewProperty);
            set => SetValue(ShowWindowPreviewProperty, value);
        }
    }
}
