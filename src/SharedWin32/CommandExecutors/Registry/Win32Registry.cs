// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Win32Registry.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.Registry
{
    using Microsoft.Win32;

    /// <summary>
    /// Implementation of <see cref="IWin32Registry"/> using the real Windows registry.
    /// </summary>
    internal sealed class Win32Registry : IWin32Registry
    {
        public IWin32RegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view)
        {
            var registryKey = RegistryKey.OpenBaseKey(hKey, view);
            return new Win32RegistryKey(registryKey);
        }
    }
}
