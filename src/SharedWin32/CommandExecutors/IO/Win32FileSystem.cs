// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Win32FileSystem.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.IO
{
    using System.IO;

    internal class Win32FileSystem : IWin32FileSystem
    {
        public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
        {
            File.Copy(sourceFileName, destinationFileName, overwrite);
        }
    }
}
