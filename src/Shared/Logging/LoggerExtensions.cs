// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerExtensions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Logging
{
    using System;
    using ServiceContracts.Logging;

    /// <summary>
    /// Contains extension methods for a <see cref="ILogger"/>.
    /// </summary>
    public static class LoggerExtensions
    {
        public static void LogDebug(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Debug, message);
        }

        public static void LogInfo(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Informational, message);
        }

        public static void LogSuccess(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Success, message);
        }

        public static void LogWarning(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Warning, message);
        }

        public static void LogError(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Error, message);
        }

        public static void LogException(this ILogger logger, string message, Exception e, LogLevel level = LogLevel.Error)
        {
            logger.Log(level, $"{message} {e.GetType()}: {e.Message}");
            logger.Log(level, $"Stack trace: {e.StackTrace}");
        }
    }
}
