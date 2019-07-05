// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerExtensions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Logging
{
    using ServiceContracts.Logging;

    /// <summary>
    /// Contains extension methods for a <see cref="ILogger"/>.
    /// </summary>
    public static class LoggerExtensions
    {
        public static void LogDebug(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, message, args);
        }

        public static void LogInfo(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Informational, message, args);
        }

        public static void LogSuccess(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Success, message, args);
        }

        public static void LogWarning(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, message, args);
        }

        public static void LogError(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, message, args);
        }
    }
}
