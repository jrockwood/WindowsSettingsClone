// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseLogger.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Logging
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Abstract base class for all loggers.
    /// </summary>
    public abstract class BaseLogger : ILogger
    {
        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Warning;

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void Log(LogLevel level, string message, params object[] args)
        {
            if (ShouldLog(level))
            {
                string fullMessage =
                    $"{FormatPrefix(level)}: {string.Format(CultureInfo.InvariantCulture, message, args)}";
                LogInternal(fullMessage);
            }
        }

        /// <summary>
        /// Implemented by sub classes to do the actual logging. This will only be called if the logging should happen
        /// (if it cleared the minimum logging level).
        /// </summary>
        /// <param name="message">The exact string to log.</param>
        protected abstract void LogInternal(string message);

        protected bool ShouldLog(LogLevel logLevel)
        {
            return logLevel >= MinimumLogLevel;
        }

        protected virtual string FormatPrefix(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return "debug";

                case LogLevel.Informational:
                    return "info";

                case LogLevel.Warning:
                    return "warn";

                case LogLevel.Error:
                    return "error";

                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }
    }
}
