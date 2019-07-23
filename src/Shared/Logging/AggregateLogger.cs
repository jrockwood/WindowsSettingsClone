// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="AggregateLogger.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Logging
{
    using System.Collections.Generic;
    using System.Linq;
    using ServiceContracts.Logging;

    /// <summary>
    /// Implementation of an <see cref="ILogger"/> that wraps multiple loggers and forwards all log messages to each
    /// wrapped logger.
    /// </summary>
    public class AggregateLogger : ILogger
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly List<ILogger> _loggers = new List<ILogger>();

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public AggregateLogger(params ILogger[] wrappedLoggers)
        {
            _loggers.AddRange(wrappedLoggers ?? Enumerable.Empty<ILogger>());
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public LogLevel MinimumLogLevel { get; set; }

        /// <summary>
        /// True - this logger controls whether messages get logged to each aggregate logger;
        /// False - each logger controls whether it writes messages
        /// </summary>
        public bool UseAggregateMinimumLogLevel { get; set; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void AddLogger(ILogger logger)
        {
            _loggers.Add(logger);
        }

        public void Log(LogLevel level, string message, params object[] args)
        {
            if (UseAggregateMinimumLogLevel && !BaseLogger.ShouldLog(level, MinimumLogLevel))
            {
                return;
            }

            foreach (ILogger logger in _loggers)
            {
                logger.Log(level, message);
            }
        }
    }
}
