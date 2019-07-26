// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeFileSystem.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using SharedWin32.CommandExecutors.IO;

    /// <summary>
    /// Implementation of a fake file system for unit tests.
    /// </summary>
    internal class FakeFileSystem : IWin32FileSystem
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly Dictionary<string, string> _files =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeFileSystem"/> class with the specified files.
        /// </summary>
        /// <param name="filePathAndContents">The string needs to be in the form "path\to\file.txt=Contents"</param>
        public FakeFileSystem(params string[] filePathAndContents)
        {
            foreach (string filePathAndContent in filePathAndContents)
            {
                if (filePathAndContent.IndexOf('=') < 0)
                {
                    throw new ArgumentException($"Invalid filepath=contents string: {filePathAndContent}");
                }

                string[] parts = filePathAndContent.Split('=');
                string fileName = parts[0];
                string contents = parts.Length > 1 ? parts[1] : string.Empty;

                _files.Add(fileName, contents);
            }
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
        {
            if (!_files.ContainsKey(sourceFileName))
            {
                throw new FileNotFoundException($"Could not find file '{sourceFileName}'.");
            }

            if (_files.ContainsKey(destinationFileName) && !overwrite)
            {
                // Yes, the OS throws this exception message even though the destination file is the problem here.
                throw new FileNotFoundException($"Could not find file '{sourceFileName}'.");
            }

            string sourceContents = _files[sourceFileName];
            _files.Add(destinationFileName, sourceContents);
        }

        public bool FileExists(string fileName)
        {
            return _files.ContainsKey(fileName);
        }

        public string ReadAllText(string fileName)
        {
            if (!_files.ContainsKey(fileName))
            {
                throw new FileNotFoundException($"Could not find file '{fileName}'.");
            }

            return _files[fileName];
        }
    }
}
