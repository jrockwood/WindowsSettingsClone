// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseLogger.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Logging
{
    using System;
    using System.Globalization;
    using ServiceContracts.Logging;

    /// <summary>
    /// Abstract base class for all loggers.
    /// </summary>
    public abstract class BaseLogger : ILogger
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        protected BaseLogger(LogLevel minimumLogLevel = LogLevel.Success)
        {
            MinimumLogLevel = minimumLogLevel;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public LogLevel MinimumLogLevel { get; set; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static bool ShouldLog(LogLevel logLevel, LogLevel minimumLogLevel)
        {
            return logLevel >= minimumLogLevel;
        }

        public void Log(LogLevel level, string message, params object[] args)
        {
            if (!ShouldLog(level))
            {
                return;
            }

            string fullMessage = $"{FormatPrefix(level)}: {string.Format(CultureInfo.InvariantCulture, message, args)}";
            LogInternal(level, fullMessage);
        }

        /// <summary>
        /// Implemented by sub classes to do the actual logging. This will only be called if the logging should happen
        /// (if it cleared the minimum logging level).
        /// </summary>
        /// <param name="level">
        /// The log level that should be logged. Useful for showing levels in different colors or other differentiation.
        /// </param>
        /// <param name="message">The exact string to log.</param>
        protected abstract void LogInternal(LogLevel level, string message);

        protected bool ShouldLog(LogLevel logLevel)
        {
            return ShouldLog(logLevel, MinimumLogLevel);
        }

        protected virtual string FormatPrefix(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return "debug";

                case LogLevel.Informational:
                    return "info";

                case LogLevel.Success:
                    return "success";

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
