// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="HeadlessProgram.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.HeadlessApps
{
    using System.IO;
    using Logging;
    using ServiceContracts.Logging;

    public abstract class HeadlessProgram
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        protected HeadlessProgram(string assemblyPath)
        {
            Logger = CreateLogger(assemblyPath);
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        protected ILogger Logger { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public virtual void Run()
        {
            // To debug this app, you'll need to have it started in console mode. Uncomment the lines below and then
            // right-click on the project file to get to project settings. Select the Application tab and change the
            // Output Type from Windows Application to Console Application. A "Windows Application" is simply a headless
            // console app.

            //Console.WriteLine("Detach your debugger from the UWP app and attach it to DesktopServices.");
            //Console.WriteLine("Set your breakpoint in DesktopServices and then press Enter to continue.");
            //Console.ReadLine();

            InitializeServices();

            // Let the app service connection handlers respond to events. If this Win32 app had a Window, this would be a
            // message loop. The app ends when the app service connection to the UWP app is closed and our
            // Connection_ServiceClosed event handler is fired.
            while (true)
            {
                // the below is necessary if this were calling COM and this was STAThread
                // pump the underlying STA thread
                // https://blogs.msdn.microsoft.com/cbrumme/2004/02/02/apartments-and-pumping-in-the-clr/
                // Thread.CurrentThread.Join(0);
            }
        }

        protected abstract bool InitializeServices();

        protected void OnUnhandledException(object exceptionObject)
        {
            Logger.LogError($"Terminating: {exceptionObject}");
#if DEBUG
            // ReSharper disable once LocalizableElement
            System.Console.WriteLine("Waiting for key press before exiting");
            System.Console.ReadLine();
#endif
        }

        private static ILogger CreateLogger(string assemblyPath)
        {
#if DEBUG
            const LogLevel minimumLogLevel = LogLevel.Debug;
#else
            const LogLevel minimumLogLevel = LogLevel.Warning;
#endif

            var consoleLogger = new ConsoleLogger(minimumLogLevel);
            var fileLogger = new FileLogger(
                Path.Combine(Path.GetDirectoryName(assemblyPath), "debug.log"),
                LogLevel.Debug);
            ILogger logger = new AggregateLogger(consoleLogger, fileLogger);
            return logger;
        }
    }
}
