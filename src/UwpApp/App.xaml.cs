// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp
{
    using System;
    using System.IO;
    using FullTrustServices;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Logging;
    using ServiceContracts.ViewServices;
    using Shared.Logging;
    using Views;
    using ViewServices;
    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.ApplicationModel.AppService;
    using Windows.ApplicationModel.Background;
    using Windows.Foundation;
    using Windows.Foundation.Metadata;
    using Windows.Storage;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly AppServiceLocator _appServiceLocator = new AppServiceLocator();

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code executed, and as such
        /// is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            CreateLoggerAsync();

            InitializeComponent();
            Suspending += OnSuspending;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public static new App Current => (App)Application.Current;

        public ILogger Logger { get; private set; } = new NullLogger();

        public ICommandBridgeClientService BridgeClientService { get; private set; }

        public IAppServiceLocator AppServiceLocator => _appServiceLocator;

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

            LaunchDesktopServicesBridge();
        }

        /// <summary>
        /// Invoked when our application is activated in the background (most likely due to the application service).
        /// </summary>
        protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            base.OnBackgroundActivated(args);

            // Make sure we've been invoked by the app service.
            if (!(args.TaskInstance.TriggerDetails is AppServiceTriggerDetails triggerDetails))
            {
                return;
            }

            // This line of code looks like it doesn't do anything, however it's critical. The call to GetDeferral() does
            // more than just return a deferral. It informs the system that the background task might continue to perform
            // after it returns. Without this call, the full-trust DesktopServicesApp.exe will be terminated early.
            BackgroundTaskDeferral deferral = args.TaskInstance.GetDeferral();
            BridgeClientService = new CommandBridgeClientService(triggerDetails.AppServiceConnection);

            _appServiceLocator.RegistryReadService = new RegistryReadService(BridgeClientService);
            _appServiceLocator.RegistryWriteService = new RegistryWriteService(BridgeClientService);
            _appServiceLocator.Win32ApiService = new Win32ApiService(BridgeClientService);
            _appServiceLocator.Win32FileSystemService = new Win32FileSystemService(BridgeClientService);
        }

        private static async void LaunchDesktopServicesBridge()
        {
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            }
        }

        private async void CreateLoggerAsync()
        {
            StorageFolder logFolder = ApplicationData.Current.TemporaryFolder;
            Stream fileStream = await logFolder.OpenStreamForWriteAsync(
                "AppLog.log",
                CreationCollisionOption.ReplaceExisting);

            var streamLogger = new StreamLogger(fileStream, LogLevel.Debug);
            Logger = streamLogger;
        }

        private void AddPlatformStyles()
        {
            // We need to load the appropriate styles depending on the platform capabilities.
            // Note: The RevealStyles.xaml is a file deployed with the application and NonRevealStyles.xaml is an
            // embedded resource. We can't embed the reveal styles since our Visual Studio project targets an older
            // platform (Creator's Update) where the reveal brush is not supported. Therefore we get compile-time errors
            // when trying to embed it.
            ResourceDictionary stylesDictionary = AppServiceLocator.PlatformCapabilityService.IsRevealBrushSupported
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
