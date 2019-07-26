// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="UwpFileSystemService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.ViewServices
{
    using System;
    using System.Threading.Tasks;
    using ServiceContracts.ViewServices;
    using Windows.Storage;

    /// <summary>
    /// Service contract for file operations that need to be performed within the UWP application sandbox, but without
    /// access to the UWP assemblies.
    /// </summary>
    internal class UwpFileSystemService : IUwpFileSystemService
    {
        /// <summary>
        /// Creates a file in the temporary directory with the specified name.
        /// </summary>
        /// <param name="desiredFileName">The desired file name.</param>
        /// <param name="overwrite">Indicates whether to overwrite the file if it already exists.</param>
        /// <returns>The full path to the file.</returns>
        public async Task<string> CreateTemporaryFileAsync(string desiredFileName, bool overwrite)
        {
            StorageFile file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(
                desiredFileName,
                overwrite ? CreationCollisionOption.ReplaceExisting : CreationCollisionOption.FailIfExists);

            return file.Path;
        }
    }
}
