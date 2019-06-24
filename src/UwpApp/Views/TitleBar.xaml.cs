// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TitleBar.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Views
{
    using System;
    using ViewModels;
    using Windows.ApplicationModel.Core;
    using Windows.UI;
    using Windows.UI.Core;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    public sealed partial class TitleBar : UserControl
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly AccessibilitySettings _accessibilitySettings = new AccessibilitySettings();
        private readonly CoreApplicationViewTitleBar _coreTitleBar;
        private readonly UISettings _uiSettings = new UISettings();

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TitleBar()
        {
            _coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

            InitializeComponent();

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public TitleBarViewModel ViewModel { get; set; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        private void SetTitleBarControlColors()
        {
            ApplicationViewTitleBar applicationTitleBar = ApplicationView.GetForCurrentView()?.TitleBar;
            if (applicationTitleBar == null)
            {
                return;
            }

            if (_accessibilitySettings.HighContrast)
            {
                // Reset the colors
                applicationTitleBar.ButtonBackgroundColor = null;
                applicationTitleBar.ButtonForegroundColor = null;
                applicationTitleBar.ButtonInactiveBackgroundColor = null;
                applicationTitleBar.ButtonInactiveForegroundColor = null;
                applicationTitleBar.ButtonHoverBackgroundColor = null;
                applicationTitleBar.ButtonHoverForegroundColor = null;
                applicationTitleBar.ButtonPressedBackgroundColor = null;
                applicationTitleBar.ButtonPressedForegroundColor = null;
            }
            else
            {
                ResourceDictionary resourceDictionary = Application.Current.Resources;

                Color backgroundColor = Colors.Transparent;
                Color foregroundColor =
                    ((SolidColorBrush)resourceDictionary["SystemControlPageTextBaseHighBrush"]).Color;
                Color inactiveForegroundColor =
                    ((SolidColorBrush)resourceDictionary["SystemControlForegroundChromeDisabledLowBrush"]).Color;
                Color hoverBackgroundColor =
                    ((SolidColorBrush)resourceDictionary["SystemControlBackgroundListLowBrush"]).Color;
                Color hoverForegroundColor =
                    ((SolidColorBrush)resourceDictionary["SystemControlForegroundBaseHighBrush"]).Color;
                Color pressedBackgroundColor =
                    ((SolidColorBrush)resourceDictionary["SystemControlBackgroundListMediumBrush"]).Color;
                Color pressedForegroundColor =
                    ((SolidColorBrush)resourceDictionary["SystemControlForegroundBaseHighBrush"]).Color;

                applicationTitleBar.ButtonBackgroundColor = backgroundColor;
                applicationTitleBar.ButtonForegroundColor = foregroundColor;
                applicationTitleBar.ButtonInactiveBackgroundColor = backgroundColor;
                applicationTitleBar.ButtonInactiveForegroundColor = inactiveForegroundColor;
                applicationTitleBar.ButtonHoverBackgroundColor = hoverBackgroundColor;
                applicationTitleBar.ButtonHoverForegroundColor = hoverForegroundColor;
                applicationTitleBar.ButtonPressedBackgroundColor = pressedBackgroundColor;
                applicationTitleBar.ButtonPressedForegroundColor = pressedForegroundColor;
            }
        }

        private void SetTitleBarExtendView()
        {
            _coreTitleBar.ExtendViewIntoTitleBar = !_accessibilitySettings.HighContrast;
            Window.Current.SetTitleBar(TitleBarDraggableElement);
        }

        private void SetTitleBarVisibility() => LayoutRoot.Visibility =
            _coreTitleBar.IsVisible && !_accessibilitySettings.HighContrast ? Visibility.Visible : Visibility.Collapsed;

        private void SetTitleBarPadding()
        {
            double leftAddition;
            double rightAddition;

            if (FlowDirection == FlowDirection.LeftToRight)
            {
                leftAddition = _coreTitleBar.SystemOverlayLeftInset;
                rightAddition = _coreTitleBar.SystemOverlayRightInset;
            }
            else
            {
                leftAddition = _coreTitleBar.SystemOverlayRightInset;
                rightAddition = _coreTitleBar.SystemOverlayLeftInset;
            }

            LayoutRoot.Padding = new Thickness(leftAddition, 0, rightAddition, 0);
        }

        //// ===========================================================================================================
        //// Event Handlers
        //// ===========================================================================================================

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Hook up event listeners
            _coreTitleBar.IsVisibleChanged += OnCoreTitleBarIsVisibleChanged;
            _coreTitleBar.LayoutMetricsChanged += OnCoreTitleBarLayoutMetricsChanged;
            _uiSettings.ColorValuesChanged += OnUISettingsColorValuesChanged;
            _accessibilitySettings.HighContrastChanged += OnHighContrastChanged;
            Window.Current.Activated += OnWindowActivated;

            // Set properties
            LayoutRoot.Height = _coreTitleBar.Height;
            SetTitleBarControlColors();
            SetTitleBarExtendView();
            SetTitleBarVisibility();
            SetTitleBarPadding();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            // Unhook event listeners
            _coreTitleBar.IsVisibleChanged -= OnCoreTitleBarIsVisibleChanged;
            _coreTitleBar.LayoutMetricsChanged -= OnCoreTitleBarLayoutMetricsChanged;
            _uiSettings.ColorValuesChanged -= OnUISettingsColorValuesChanged;
            _accessibilitySettings.HighContrastChanged -= OnHighContrastChanged;
            Window.Current.Activated -= OnWindowActivated;
        }

        private async void OnUISettingsColorValuesChanged(UISettings sender, object args) =>
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, SetTitleBarControlColors);

        private void OnCoreTitleBarLayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            LayoutRoot.Height = sender.Height;
            SetTitleBarPadding();
        }

        private void OnCoreTitleBarIsVisibleChanged(CoreApplicationViewTitleBar sender, object args) => SetTitleBarVisibility();

        private async void OnHighContrastChanged(AccessibilitySettings sender, object args) => await Dispatcher.RunAsync(
            CoreDispatcherPriority.Normal, () =>
            {
                SetTitleBarControlColors();
                SetTitleBarExtendView();
                SetTitleBarVisibility();
            });

        private void OnWindowActivated(object sender, WindowActivatedEventArgs e)
        {
            string stateName = e.WindowActivationState == CoreWindowActivationState.Deactivated
                ? WindowNotFocused.Name
                : WindowFocused.Name;

            VisualStateManager.GoToState(this, stateName, useTransitions: false);
        }
    }
}
