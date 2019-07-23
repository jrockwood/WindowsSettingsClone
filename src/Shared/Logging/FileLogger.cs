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
    using ServiceContracts.Logging;

    /// <summary>
    /// Implements a file-based logger.
    /// </summary>
    public class FileLogger : StreamLogger, IDisposable
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private FileLogger(Stream stream, string filePath, LogLevel minimumLogLevel)
            : base(stream, minimumLogLevel, leaveOpen: false)
        {
            FilePath = filePath;
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
                fileLogger = new FileLogger(fileStream, filePath, minimumLogLevel);
                return true;
            }
            catch (Exception)
            {
                fileLogger = null;
                return false;
            }
        }
    }
}
