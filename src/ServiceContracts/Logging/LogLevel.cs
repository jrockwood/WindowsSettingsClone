// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="LogLevel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Logging
{
    /// <summary>
    /// Enumerates the different levels of logging.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Debug logging can aid in debugging issues, but should normally be off since it can be very verbose.
        /// </summary>
        Debug,

        /// <summary>
        /// Informational messages can aid in debugging and are meant to be less verbose than debug messages.
        /// </summary>
        Informational,

        /// <summary>
        /// Success messages are a special kind of informational message, but are less frequent and can typically be
        /// shown in green in a console, for example.
        /// </summary>
        Success,

        /// <summary>
        /// Warning messages are typically recoverable or known errors, but may indicate an impending failure.
        /// </summary>
        Warning,

        /// <summary>
        /// Errors are problems in the system that may or may not be recoverable.
        /// </summary>
        Error,
    }
}
