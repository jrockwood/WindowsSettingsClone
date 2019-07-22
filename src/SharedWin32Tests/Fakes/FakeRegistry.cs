// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeRegistry.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Win32;
    using SharedWin32.CommandExecutors.Registry;

    /// <summary>
    /// Implementation of a fake registry.
    /// </summary>
    public class FakeRegistry : IWin32Registry
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly Dictionary<string, Dictionary<string, object>> _keys =
            new Dictionary<string, Dictionary<string, object>>(StringComparer.OrdinalIgnoreCase);

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeRegistry"/> class with the specified keys and value.
        /// </summary>
        /// <param name="keysAndValues">
        /// The string needs to be in the form "BASE\SubKey[\Value=123]", where BASE is one of <c>HKEY_CLASSES_ROOT</c>
        /// (or <c>HKCR</c>), <c>HKEY_CURRENT_USER</c> (or <c>HKCU</c>), <c>HKEY_LOCAL_MACHINE</c> (or <c>HKLM</c>), or
        /// HKEY_USERS (or <c>HKU</c>). The value can either be an integer or a string with single or double quotes.
        /// </param>
        public FakeRegistry(params string[] keysAndValues)
        {
            foreach (string keyAndValue in keysAndValues)
            {
                var parsedPath = RegistryPath.Parse(keyAndValue);
                using (IWin32RegistryKey key = OpenBaseKey(parsedPath.Hive, RegistryView.Registry64)
                    .CreateSubKey(parsedPath.Path, writable: true))
                {
                    if (!string.IsNullOrEmpty(parsedPath.ValueName))
                    {
                        RegistryValueKind kind = parsedPath.Value is string
                            ? RegistryValueKind.String
                            : RegistryValueKind.DWord;
                        key.SetValue(parsedPath.ValueName, parsedPath.Value, kind);
                    }
                }
            }
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public IWin32RegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view)
        {
            if (view != RegistryView.Registry64)
            {
                throw new ArgumentException("View type not supported", nameof(view));
            }

            return CreateSubKey(RegistryCommandExecutor.HiveToWin32Name(hKey), false);
        }

        private IWin32RegistryKey OpenSubKey(string fullPath, bool writable)
        {
            return _keys.ContainsKey(fullPath) ? new Key(this, fullPath, writable) : null;
        }

        private IWin32RegistryKey CreateSubKey(string fullPath, bool writable)
        {
            IWin32RegistryKey subKey = OpenSubKey(fullPath, writable);
            if (subKey == null)
            {
                subKey = new Key(this, fullPath, writable);
                _keys.Add(fullPath, null);

                // Also add any nested paths
                string[] nestedPathSegments = fullPath.Split('\\');
                while (nestedPathSegments.Length > 1)
                {
                    string path = string.Join("\\", nestedPathSegments.Take(nestedPathSegments.Length - 1));
                    if (_keys.ContainsKey(path))
                    {
                        break;
                    }

                    _keys.Add(path, null);
                    nestedPathSegments = path.Split('\\');
                }
            }

            return subKey;
        }

        //// ===========================================================================================================
        //// Classes
        //// ===========================================================================================================

        private sealed class Key : IWin32RegistryKey
        {
            private readonly FakeRegistry _registry;
            private bool _isDisposed;

            public Key(FakeRegistry registry, string fullPath, bool writable)
            {
                _registry = registry;
                FullPath = fullPath;
                Writable = writable;
            }

            private string FullPath { get; }
            private bool Writable { get; }

            public void Dispose()
            {
                _isDisposed = true;
            }

            public IWin32RegistryKey OpenSubKey(string name, bool writable)
            {
                ThrowIfDisposedOnNonBaseKey();
                return _registry.OpenSubKey($"{FullPath}\\{name}", writable);
            }

            public IWin32RegistryKey CreateSubKey(string name, bool writable)
            {
                ThrowIfDisposedOnNonBaseKey();
                return _registry.CreateSubKey($"{FullPath}\\{name}", writable);
            }

            public object GetValue(string name)
            {
                ThrowIfDisposedOnNonBaseKey();
                return GetValue(name, null);
            }

            public object GetValue(string name, object defaultValue)
            {
                ThrowIfDisposedOnNonBaseKey();

                Dictionary<string, object> values = _registry._keys[FullPath];
                if (values == null || !values.TryGetValue(name, out object value))
                {
                    value = defaultValue;
                }

                return value;
            }

            public void SetValue(string name, object value, RegistryValueKind valueKind)
            {
                ThrowIfDisposedOnNonBaseKey();

                if (!Writable)
                {
                    throw new UnauthorizedAccessException("Cannot write to the registry key.");
                }

                CreateSubKey(name, false);

                _registry._keys.TryGetValue(FullPath, out Dictionary<string, object> values);

                values = values ?? new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                _registry._keys[FullPath] = values;

                values[name] = value;
            }

            /// <summary>
            /// Throws an <see cref="ObjectDisposedException"/> if necessary. Note that operations are still allowed on
            /// root keys even when disposed.
            /// </summary>
            private void ThrowIfDisposedOnNonBaseKey()
            {
                if (_isDisposed && FullPath.Contains("\\"))
                {
                    throw new ObjectDisposedException(FullPath, "Cannot access a closed registry key.");
                }
            }
        }
    }
}
