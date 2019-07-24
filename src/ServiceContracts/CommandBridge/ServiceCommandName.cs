// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCommandName.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.CommandBridge
{
    /// <summary>
    /// Enumerates all of the service commands.
    /// </summary>
    // PLEASE ALPHABETIZE THESE COMMANDS (except for the first Unknown value).
    public enum ServiceCommandName
    {
        // Keep this first
        Unknown,

        // Alphabetize the rest

        Echo,
        RegistryReadIntValue,
        RegistryReadStringValue,
        RegistryWriteIntValue,
        RegistryWriteStringValue,
        ShutdownServer,
        SystemParametersInfoGetValue,
        SystemParametersInfoSetValue,
    }
}
