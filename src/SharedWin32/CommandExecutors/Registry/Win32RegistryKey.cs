// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Win32RegistryKey.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.Registry
{
    using Microsoft.Win32;

    /// <summary>
    /// Implementation of <see cref="IWin32RegistryKey"/> using the real Windows registry.
    /// </summary>
    internal sealed class Win32RegistryKey : IWin32RegistryKey
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly RegistryKey _registryKey;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        internal Win32RegistryKey(RegistryKey registryKey)
        {
            _registryKey = registryKey;
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void Dispose()
        {
            _registryKey?.Dispose();
        }

        public IWin32RegistryKey OpenSubKey(string name, bool writable)
        {
            RegistryKey subKey = _registryKey.OpenSubKey(name, writable);
            return new Win32RegistryKey(subKey);
        }

        public IWin32RegistryKey CreateSubKey(string name, bool writable)
        {
            RegistryKey subKey = _registryKey.CreateSubKey(name, writable);
            return new Win32RegistryKey(subKey);
        }

        public object GetValue(string name)
        {
            return _registryKey.GetValue(name);
        }

        public object GetValue(string name, object defaultValue)
        {
            return _registryKey.GetValue(name, defaultValue);
        }

        public void SetValue(string name, object value, RegistryValueKind valueKind)
        {
            _registryKey.SetValue(name, value, valueKind);
        }
    }
}
