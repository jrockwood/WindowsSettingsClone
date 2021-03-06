﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorView.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.Editors
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using ViewModels.EditorViewModels;

    public sealed partial class EditorView : UserControl
    {
        public EditorView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel",
            typeof(EditorViewModel),
            typeof(EditorView),
            new PropertyMetadata(default(EditorViewModel)));

        public EditorViewModel ViewModel
        {
            get => (EditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
