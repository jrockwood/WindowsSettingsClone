// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp
{
    using System;
    using ViewModels.ViewServices;
    using Views;
    using ViewServices;
    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.Foundation;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code executed, and as such
        /// is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public static new App Current => (App)Application.Current;

        public INavigationViewService NavigationService =>
            ((Window.Current.Content as Frame)?.Content as RootPage).NavigationService;

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Invoked when the application is launched normally by the end user. Other entry points will be used such as
        /// when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            AddPlatformStyles();
            SetWindowMinSize();

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (!(Window.Current.Content is Frame rootFrame))
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (!e.PrelaunchActivated)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.SourcePageType = typeof(RootPage);
                }

                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        private void AddPlatformStyles()
        {
            // We need to load the appropriate styles depending on the platform capabilities.
            // Note: The RevealStyles.xaml is a file deployed with the application and NonRevealStyles.xaml is an
            // embedded resource. We can't embed the reveal styles since our Visual Studio project targets an older
            // platform (Creator's Update) where the reveal brush is not supported. Therefore we get compile-time errors
            // when trying to embed it.
            ResourceDictionary stylesDictionary = PlatformCapabilityService.Instance.IsRevealBrushSupported
                ? new ResourceDictionary { Source = new Uri("ms-appx:///Resources/RevealStyles.xaml") }
                : new ResourceDictionary { Source = new Uri("ms-appx:///Resources/NonRevealStyles.xaml") };

            Resources.MergedDictionaries.Add(stylesDictionary);
        }

        private void SetWindowMinSize()
        {
            float minWindowWidth = (float)(double)Resources["AppMinWindowWidth"];
            float minWindowHeight = (float)(double)Resources["AppMinWindowHeight"];
            Size minWindowSize = SizeHelper.FromDimensions(minWindowWidth, minWindowHeight);

            var appView = ApplicationView.GetForCurrentView();
            appView.SetPreferredMinSize(minWindowSize);
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved without knowing whether the
        /// application will be terminated or resumed with the contents of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
