// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BonusBar.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Editors
{
    using ViewModels.EditorViewModels;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class BonusBar : UserControl
    {
        public BonusBar()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel",
            typeof(BonusBarViewModel),
            typeof(BonusBar),
            new PropertyMetadata(default(BonusBarViewModel)));

        public BonusBarViewModel ViewModel
        {
            get => (BonusBarViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
