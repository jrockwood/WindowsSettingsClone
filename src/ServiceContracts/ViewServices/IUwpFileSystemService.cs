// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IUwpFileSystemService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.ViewServices
{
    using System.Threading.Tasks;

    /// <summary>
    /// Service contract for file operations that need to be performed within the UWP application sandbox, but without
    /// access to the UWP assemblies.
    /// </summary>
    public interface IUwpFileSystemService
    {
        /// <summary>
        /// Creates a file in the temporary directory with the specified name.
        /// </summary>
        /// <param name="desiredFileName">The desired file name.</param>
        /// <param name="overwrite">Indicates whether to overwrite the file if it already exists.</param>
        /// <returns>The full path to the file.</returns>
        Task<string> CreateTemporaryFileAsync(string desiredFileName, bool overwrite);
    }
}
