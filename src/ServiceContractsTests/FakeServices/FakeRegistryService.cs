// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeRegistryService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Tests.FakeServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Commands;
    using FullTrust;

    /// <summary>
    /// Implementation of a fake registry.
    /// </summary>
    public class FakeRegistryService : IRegistryReadService
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly Dictionary<string, object> _registry =
            new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void SetMockedValue(RegistryHive hive, string key, string valueName, int value)
        {
            string path = ConstructFullPath(hive, key, valueName);
            _registry[path] = value;
        }

        public void SetMockedValue(RegistryHive hive, string key, string valueName, string value)
        {
            string path = ConstructFullPath(hive, key, valueName);
            _registry[path] = value;
        }

        public async Task<int> ReadValueAsync(RegistryHive hive, string key, string valueName, int defaultValue)
        {
            int value = ReadValue(hive, key, valueName, defaultValue);
            return await Task.FromResult(value);
        }

        public async Task<bool> ReadValueAsync(RegistryHive hive, string key, string valueName, bool defaultValue)
        {
            int value = ReadValue(hive, key, valueName, defaultValue ? 1 : 0);
            return await Task.FromResult(value != 0);
        }

        public async Task<string> ReadValueAsync(RegistryHive hive, string key, string valueName, string defaultValue)
        {
            string value = ReadValue(hive, key, valueName, defaultValue);
            return await Task.FromResult(value);
        }

        private T ReadValue<T>(RegistryHive hive, string key, string valueName, T defaultValue)
        {
            string path = ConstructFullPath(hive, key, valueName);
            return _registry.ContainsKey(path) ? (T)_registry[path] : defaultValue;
        }

        private static string ConstructFullPath(RegistryHive hive, string key, string valueName)
        {
            return $@"{hive}\{key}\{valueName}";
        }
    }
}
