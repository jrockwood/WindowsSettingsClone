// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Logging
{
    /// <summary>
    /// Interface for any kind of a logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets or sets the minimum logging level. For example, if the level is <see cref="LogLevel.Warning"/>, then
        /// messages of only <see cref="LogLevel.Warning"/> and <see cref="LogLevel.Error"/> are logged.
        /// </summary>
        LogLevel MinimumLogLevel { get; set; }

        void Log(LogLevel level, string message);
    }
}
