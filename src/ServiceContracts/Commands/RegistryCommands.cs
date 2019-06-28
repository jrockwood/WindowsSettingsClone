﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryReadIntValueCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Commands
{
    using System.Collections.Generic;
    using CommandBridge;
    using Shared.Utility;

    /// <summary>
    /// Abstract base class for all of the commands that read a value from the Windows Registry.
    /// </summary>
    /// <typeparam name="T">The type of the value name to read.</typeparam>
    public abstract class RegistryReadValueCommand<T> : ServiceCommand
    {
        protected RegistryReadValueCommand(
            ServiceCommandName commandName,
            RegistryHive hive,
            string key,
            string valueName,
            T defaultValue)
            : base(commandName)
        {
            Hive = hive;
            Key = Param.VerifyString(key, nameof(key));
            ValueName = valueName;
            DefaultValue = defaultValue;
        }

        public RegistryHive Hive { get; }
        public string Key { get; }
        public string ValueName { get; }
        public T DefaultValue { get; }

        protected override void SerializeParams(IDictionary<string, object> valueSet)
        {
            valueSet.Add(ParamName.RegistryHive.ToString(), Hive.ToString());
            valueSet.Add(ParamName.RegistryKey.ToString(), Key);
            valueSet.Add(ParamName.RegistryValueName.ToString(), ValueName);
            valueSet.Add(ParamName.RegistryDefaultValue.ToString(), DefaultValue);
        }
    }

    /// <summary>
    /// Command that reads an integer value (DWORD) from the Windows Registry.
    /// </summary>
    public sealed class RegistryReadIntValueCommand : RegistryReadValueCommand<int>
    {
        public RegistryReadIntValueCommand(RegistryHive hive, string key, string valueName, int defaultValue)
            : base(ServiceCommandName.RegistryReadIntValue, hive, key, valueName, defaultValue)
        {
        }
    }
}
