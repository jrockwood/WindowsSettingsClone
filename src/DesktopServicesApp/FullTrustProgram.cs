// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FullTrustProgram.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.DesktopServicesApp
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Shared.HeadlessApps;
    using Shared.Logging;

    internal class FullTrustProgram : HeadlessProgram
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public FullTrustProgram()
            : base(Assembly.GetExecutingAssembly().Location)
        {
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Run()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            base.Run();
        }

        protected override bool InitializeServices()
        {
            var bridge = new UwpCommunicationBridge(Logger);

            // We can't have an async entry point, so do this on the thread pool.
            bool success = Task.Run(async () => await bridge.InitializeAsync()).GetAwaiter().GetResult();
            if (success)
            {
                Logger.LogSuccess("Established app service connection");
            }
            else
            {
                Logger.LogError("Cannot establish app service connection");
            }

            return success;
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnUnhandledException(e.ExceptionObject);
        }

        private static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }
    }
}
