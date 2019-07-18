// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegistryReadService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.FullTrust
{
    using System.Threading.Tasks;
    using Commands;

    /// <summary>
    /// Service interface for reading settings from the Windows registry.
    /// </summary>
    public interface IRegistryReadService
    {
        Task<int> ReadValueAsync(RegistryBaseKey baseKey, string key, string valueName, int defaultValue);

        Task<bool> ReadValueAsync(RegistryBaseKey baseKey, string key, string valueName, bool defaultValue);

        Task<string> ReadValueAsync(RegistryBaseKey baseKey, string key, string valueName, string defaultValue);
    }
}
