// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IWin32FileSystem.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.IO
{
    /// <summary>
    /// Service contract for performing file system operations. Extracted to an an interface for easy unit testing.
    /// </summary>
    public interface IWin32FileSystem
    {
        void CopyFile(string sourceFileName, string destinationFileName, bool overwrite);
    }
}
