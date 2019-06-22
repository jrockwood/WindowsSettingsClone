// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BonusBar.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Editors
{
    using System;
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

    public sealed class BonusBarItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DescriptionTemplate { get; set; }
        public DataTemplate NavigationLinkTemplate { get; set; }
        public DataTemplate WebLinkTemplate { get; set; }
        public DataTemplate LaunchAppLinkTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            switch (item)
            {
                case BonusBarDescriptionItem _:
                    return DescriptionTemplate;

                case BonusBarNavigationLink _:
                    return NavigationLinkTemplate;

                case BonusBarWebLink _:
                    return WebLinkTemplate;

                case BonusBarLaunchAppLink _:
                    return LaunchAppLinkTemplate;

                default:
                    throw new InvalidOperationException($"Unknown item type '{item.GetType()}'");
            }
        }
    }
}
