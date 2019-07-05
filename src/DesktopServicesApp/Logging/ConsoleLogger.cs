// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleLogger.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.DesktopServicesApp.Logging
{
    using System;
    using ServiceContracts.Logging;
    using Shared.Logging;

    internal class ConsoleLogger : BaseLogger
    {
        public ConsoleLogger(LogLevel minimumLogLevel = LogLevel.Warning)
            : base(minimumLogLevel)
        {
        }

        protected override void LogInternal(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Informational:
                    Console.WriteLine(message);
                    break;

                case LogLevel.Success:
                    ColoredConsole.WriteSuccess(message);
                    break;

                case LogLevel.Warning:
                    ColoredConsole.WriteWarning(message);
                    break;

                case LogLevel.Error:
                    ColoredConsole.WriteError(message);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }
}
