// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoryLogger.cs" company="Justin Rockwood">
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
    /// Represents an <see cref="ILogger"/> that stores all of the log messages in memory.
    /// </summary>
    public class MemoryLogger : BaseLogger
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly List<LevelMessagePair> _all = new List<LevelMessagePair>();

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public MemoryLogger(LogLevel minimumLogLevel = LogLevel.Debug)
            : base(minimumLogLevel)
        {
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public IEnumerable<string> All => _all.Select(tuple => tuple.Message);

        public IEnumerable<string> Errors =>
            _all.Where(tuple => tuple.Level == LogLevel.Error).Select(tuple => tuple.Message);

        public IEnumerable<string> Warnings =>
            _all.Where(tuple => tuple.Level == LogLevel.Warning).Select(tuple => tuple.Message);

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        protected override void LogInternal(LogLevel level, string message)
        {
            _all.Add(new LevelMessagePair(level, message));
        }

        //// ===========================================================================================================
        //// Classes
        //// ===========================================================================================================

        // This could have been made a tuple instead, but it requires adding the System.ValueTuple assembly, which then
        // causes conflicts with other assemblies.
        private struct LevelMessagePair
        {
            public readonly LogLevel Level;
            public readonly string Message;

            public LevelMessagePair(LogLevel level, string message)
            {
                Level = level;
                Message = message;
            }
        }
    }
}
