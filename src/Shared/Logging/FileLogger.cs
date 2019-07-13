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

        private readonly StreamWriter _writer;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private FileLogger(StreamWriter writer, string filePath, LogLevel minimumLogLevel)
            : base(minimumLogLevel)
        {
            FilePath = filePath;
            _writer = writer;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public string FilePath { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static bool TryCreate(string filePath, LogLevel minimumLogLevel, out FileLogger fileLogger)
        {
            try
            {
                filePath = Path.GetFullPath(filePath);
                var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                var writer = new StreamWriter(fileStream, Encoding.UTF8)
                {
                    AutoFlush = true
                };

                fileLogger = new FileLogger(writer, filePath, minimumLogLevel);
                return true;
            }
            catch (Exception)
            {
                fileLogger = null;
                return false;
            }
        }

        public void Dispose()
        {
            _writer?.Dispose();
        }

        protected override void LogInternal(LogLevel level, string message)
        {
            _writer.WriteLine(message);
        }
    }
}
