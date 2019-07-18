// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IWin32Registry.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.Registry
{
    using System;
    using System.Security;
    using Microsoft.Win32;

    /// <summary>
    /// Contract for opening base registry keys for working with the raw Windows registry. Extracted to an an interface
    /// for easy unit testing.
    /// </summary>
    public interface IWin32Registry
    {
        /// <summary>
        /// Opens a new <see cref="IWin32RegistryKey"/> that represents the requested key on the local machine with the
        /// specified view.
        /// </summary>
        /// <param name="hKey">The HKEY to open.</param>
        /// <param name="view">The registry view to use.</param>
        /// <returns>The requested registry key.</returns>
        /// <exception cref="ArgumentException"><paramref name="hKey"/> or <paramref name="view"/> is invalid.</exception>
        /// <exception cref="UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
        /// <exception cref="SecurityException">
        /// The user does not have the permissions required to perform this action.
        /// </exception>
        IWin32RegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view);
    }
}
