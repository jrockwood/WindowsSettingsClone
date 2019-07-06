// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FileLogger.cs" company="Justin Rockwood">
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
    /// Implements a file-based logger.
    /// </summary>
    public class FileLogger : BaseLogger, IDisposable
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly FileStream _fileStream;
        private readonly StreamWriter _writer;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public FileLogger(string filePath, LogLevel minimumLogLevel = LogLevel.Debug)
            : base(minimumLogLevel)
        {
            FilePath = Path.GetFullPath(filePath);
            _fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            _writer = new StreamWriter(_fileStream, Encoding.UTF8)
            {
                AutoFlush = true
            };
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public string FilePath { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void Dispose()
        {
            _fileStream?.Dispose();
            _writer?.Dispose();
        }

        protected override void LogInternal(LogLevel level, string message)
        {
            _writer.WriteLine(message);
        }
    }
}
