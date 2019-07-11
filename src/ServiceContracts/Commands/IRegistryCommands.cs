// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegistryCommands.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Commands
{
    using CommandBridge;

    /// <summary>
    /// Command that reads an integer value (REG_DWORD) from the Windows Registry.
    /// </summary>
    public interface IRegistryReadIntValueCommand : IServiceCommand
    {
        RegistryHive Hive { get; }
        string Key { get; }
        string ValueName { get; }
        int DefaultValue { get; }
    }

    /// <summary>
    /// Command that reads a string value (REG_SZ) from the Windows Registry.
    /// </summary>
    public interface IRegistryReadStringCommand : IServiceCommand
    {
        RegistryHive Hive { get; }
        string Key { get; }
        string ValueName { get; }
        string DefaultValue { get; }
    }

    /// <summary>
    /// Command that writes an integer value (REG_DWORD) to the Windows Registry.
    /// </summary>
    public interface IRegistryWriteIntValueCommand : IServiceCommand
    {
        RegistryHive Hive { get; }
        string Key { get; }
        string ValueName { get; }
        int Value { get; }
    }

    /// <summary>
    /// Command that writes a string value (REG_SZ) to the Windows Registry.
    /// </summary>
    public interface IRegistryWriteStringCommand : IServiceCommand
    {
        RegistryHive Hive { get; }
        string Key { get; }
        string ValueName { get; }
        string Value { get; }
    }
}
