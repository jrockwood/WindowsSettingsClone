// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsEditorHostControl.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Controls
{
    using Views;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Control for hosting a setting editor view within a <see cref="SettingsGroupPage"/>.
    /// </summary>
    internal class SettingsEditorHostControl : ContentControl
    {
        //// ===========================================================================================================
        //// Dependency Properties
        //// ===========================================================================================================

        public static readonly DependencyProperty IsContentReadyProperty = DependencyProperty.Register(
            "IsContentReady",
            typeof(bool),
            typeof(SettingsEditorHostControl),
            new PropertyMetadata(default(bool)));

        /// <summary>
        /// Shows or hides the content.
        /// </summary>
        public bool IsContentReady
        {
            get => (bool)GetValue(IsContentReadyProperty);
            set => SetValue(IsContentReadyProperty, value);
        }

        public static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.Register(
            "IsUpdating",
            typeof(bool),
            typeof(SettingsEditorHostControl),
            new PropertyMetadata(default(bool)));

        /// <summary>
        /// Shows or hides the progress bar.
        /// </summary>
        public bool IsUpdating
        {
            get => (bool)GetValue(IsUpdatingProperty);
            set => SetValue(IsUpdatingProperty, value);
        }

        public static readonly DependencyProperty TopMarginProperty = DependencyProperty.Register(
            "TopMargin",
            typeof(double),
            typeof(SettingsEditorHostControl),
            new PropertyMetadata(default(double)));

        public double TopMargin
        {
            get => (double)GetValue(TopMarginProperty);
            set => SetValue(TopMarginProperty, value);
        }

        public static readonly DependencyProperty TopPaddingProperty = DependencyProperty.Register(
            "TopPadding",
            typeof(double),
            typeof(SettingsEditorHostControl),
            new PropertyMetadata(default(double)));

        public double TopPadding
        {
            get => (double)GetValue(TopPaddingProperty);
            set => SetValue(TopPaddingProperty, value);
        }
    }
}
