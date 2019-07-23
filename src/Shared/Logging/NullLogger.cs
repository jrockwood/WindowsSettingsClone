// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NullLogger.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Logging
{
    using ServiceContracts.Logging;

    /// <summary>
    /// An <see cref="ILogger"/> that does nothing.
    /// </summary>
    public sealed class NullLogger : BaseLogger
    {
        protected override void LogInternal(LogLevel level, string message)
        {
            // Do nothing
        }
    }
}
