// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamLogger.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Logging
{
    using System;
    using System.IO;
    using System.Text;
    using ServiceContracts.Logging;

    /// <summary>
    /// Logs to a standard <see cref="Stream"/>.
    /// </summary>
    public class StreamLogger : BaseLogger, IDisposable
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private StreamWriter _writer;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public StreamLogger(Stream stream, LogLevel minimumLogLevel, bool leaveOpen = false)
            : base(minimumLogLevel)
        {
            _writer = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen) { AutoFlush = true };
        }

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        protected override void LogInternal(LogLevel level, string message)
        {
            _writer.WriteLine(message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _writer?.Dispose();
                _writer = null;
            }
        }
    }
}
