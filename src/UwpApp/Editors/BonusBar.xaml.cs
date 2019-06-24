// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BonusBar.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.Editors
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using ViewModels.EditorViewModels;

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

    public sealed class BonusBarSectionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OverviewSectionTemplate { get; set; }
        public DataTemplate RelatedSettingsSectionTemplate { get; set; }
        public DataTemplate PrivacyOptionsSectionTemplate { get; set; }
        public DataTemplate SupportSectionTemplate { get; set; }
        public DataTemplate FeedbackSectionTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            switch (item)
            {
                case BonusBarOverviewSection _:
                    return OverviewSectionTemplate;

                case BonusBarRelatedSettingsSection _:
                    return RelatedSettingsSectionTemplate;

                case BonusBarPrivacyOptionsSection _:
                    return PrivacyOptionsSectionTemplate;

                case BonusBarSupportSection _:
                    return SupportSectionTemplate;

                case BonusBarFeedbackSection _:
                    return FeedbackSectionTemplate;

                default:
                    throw new InvalidOperationException($"Unknown item type '{item.GetType()}'");
            }
        }
    }

    public sealed class BonusBarItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NavigationLinkTemplate { get; set; }
        public DataTemplate WebLinkTemplate { get; set; }
        public DataTemplate LaunchAppLinkTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            switch (item)
            {
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
