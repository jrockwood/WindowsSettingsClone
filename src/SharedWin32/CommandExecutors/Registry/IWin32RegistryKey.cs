// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IWin32RegistryKey.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.Registry
{
    using System;
    using Microsoft.Win32;

    /// <summary>
    /// Contract for working with the raw Windows registry. Extracted to an interface for easy unit testing.
    /// </summary>
    public interface IWin32RegistryKey : IDisposable
    {
        IWin32RegistryKey OpenSubKey(string name, bool writable = false);

        IWin32RegistryKey CreateSubKey(string name, bool writable = false);

        object GetValue(string name);

        object GetValue(string name, object defaultValue);

        void SetValue(string name, object value, RegistryValueKind valueKind);
    }
}
