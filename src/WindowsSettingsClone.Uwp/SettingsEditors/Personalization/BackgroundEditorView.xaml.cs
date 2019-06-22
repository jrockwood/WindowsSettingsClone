// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BackgroundEditorView.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.SettingsEditors.Personalization
{
    using ViewModels.SettingsEditorViewModels.Personalization;
    using Windows.UI.Xaml.Controls;

    public sealed partial class BackgroundEditorView : UserControl
    {
        public BackgroundEditorView()
        {
            InitializeComponent();
        }

        public BackgroundEditorViewModel ViewModel { get; } = new BackgroundEditorViewModel();
    }
}
