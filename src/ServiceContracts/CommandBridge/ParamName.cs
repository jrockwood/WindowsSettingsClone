// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ParamName.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.CommandBridge
{
    /// <summary>
    /// Contains all of the parameter names (keys) used in a serialized parameter bag (ValueSet).
    /// </summary>
    // PLEASE KEEP THESE ALPHABETIZED
    internal enum ParamName
    {
        CommandName,
        CommandResult,
        ErrorCode,
        ErrorMessage,
        RegistryDefaultValue,
        RegistryHive,
        RegistryKey,
        RegistryValueName,
    }
}
