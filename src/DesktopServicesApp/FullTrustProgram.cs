// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FullTrustProgram.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.DesktopServicesApp
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using ServiceContracts.Logging;
    using Shared.Logging;

    internal sealed class FullTrustProgram
    {
        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static void Main(string[] args)
        {
            // To debug this app, you'll need to have it started in console mode. Uncomment the lines below and then
            // right-click on the project file to get to project settings. Select the Application tab and change the
            // Output Type from Windows Application to Console Application. A "Windows Application" is simply a headless
            // console app.

            //Console.WriteLine("Detach your debugger from the UWP app and attach it to DesktopServices.");
            //Console.WriteLine("Set your breakpoint in DesktopServices and then press Enter to continue.");
            //Console.ReadLine();

            ILogger logger = CreateLogger();

            AppDomain.CurrentDomain.UnhandledException +=
                (sender, e) => logger.LogError($"Terminating: {e.ExceptionObject}");

            InitializeServices(logger);

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

        private static void InitializeServices(ILogger logger)
        {
            var bridge = new UwpCommunicationBridge(logger);

            // We can't have an async entry point, so do this on the thread pool.
            bool success = Task.Run(async () => await bridge.InitializeAsync()).GetAwaiter().GetResult();
            if (success)
            {
                logger.LogSuccess("Established app service connection");
            }
            else
            {
                logger.LogError("Cannot establish app service connection");
            }
        }

        private static ILogger CreateLogger()
        {
#if DEBUG
            const LogLevel minimumLogLevel = LogLevel.Debug;
#else
            const LogLevel minimumLogLevel = LogLevel.Warning;
#endif

            var consoleLogger = new ConsoleLogger(minimumLogLevel);
            var fileLogger = new FileLogger(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "FullTrustProgram.log"),
                LogLevel.Debug);
            ILogger logger = new AggregateLogger(consoleLogger, fileLogger);
            return logger;
        }
    }
}
