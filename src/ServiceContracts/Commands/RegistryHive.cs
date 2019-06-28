// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryHive.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Commands
{
    /// <summary>
    /// Represents the possible values for a top-level node in the Windows Registry.
    /// </summary>
    public enum RegistryHive
    {
        /// <summary>
        /// Represents the HKEY_CLASSES_ROOT base key.
        /// </summary>
        ClassesRoot,

        /// <summary>
        /// Represents the HKEY_CURRENT_USER base key.
        /// </summary>
        CurrentUser,

        /// <summary>
        /// Represents the HKEY_LOCAL_MACHINE base key.
        /// </summary>
        LocalMachine,

        /// <summary>
        /// Represents the HKEY_USERS base key.
        /// </summary>
        Users,

        /// <summary>
        /// Represents the HKEY_PERFORMANCE_DATA base key.
        /// </summary>
        PerformanceData,

        /// <summary>
        /// Represents the HKEY_CURRENT_CONFIG base key.
        /// </summary>
        CurrentConfig,

        /// <summary>
        /// Represents the HKEY_DYN_DATA base key.
        /// </summary>
        DynData,
    }
}
