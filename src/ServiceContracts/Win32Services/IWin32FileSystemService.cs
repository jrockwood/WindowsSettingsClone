// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IWin32FileSystemService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Win32Services
{
    using System.Threading.Tasks;

    public interface IWin32FileSystemService
    {
        Task CopyFileAsync(string sourceFileName, string destinationFileName, bool overwrite);
    }
}
