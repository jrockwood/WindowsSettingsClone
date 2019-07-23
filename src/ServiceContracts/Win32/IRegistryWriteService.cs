// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegistryWriteService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Win32
{
    using System.Threading.Tasks;
    using Commands;

    /// <summary>
    /// Service interface for writing settings from the Windows registry.
    /// </summary>
    public interface IRegistryWriteService
    {
        Task WriteValueAsync(RegistryBaseKey baseKey, string key, string valueName, int value);

        Task WriteValueAsync(RegistryBaseKey baseKey, string key, string valueName, bool value);

        Task WriteValueAsync(RegistryBaseKey baseKey, string key, string valueName, string value);
    }
}
