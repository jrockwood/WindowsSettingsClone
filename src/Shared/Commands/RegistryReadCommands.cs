// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryReadCommands.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Commands
{
    using System.Collections.Generic;
    using CommandBridge;
    using Diagnostics;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;

    /// <summary>
    /// Abstract base class for all of the commands that read a value from the Windows Registry.
    /// </summary>
    /// <typeparam name="T">The type of the value name to read.</typeparam>
    public abstract class RegistryReadValueCommand<T> : ServiceCommand
    {
        protected RegistryReadValueCommand(
            ServiceCommandName commandName,
            RegistryBaseKey baseKey,
            string key,
            string valueName,
            T defaultValue)
            : base(commandName)
        {
            BaseKey = baseKey;
            Key = Param.VerifyString(key, nameof(key));
            ValueName = valueName;
            DefaultValue = defaultValue;
        }

        internal RegistryReadValueCommand(BridgeMessageDeserializer deserializer)
            : base(deserializer.CommandName)
        {
            BaseKey = deserializer.GetEnumValue<RegistryBaseKey>(ParamName.RegistryBaseKey);
            Key = deserializer.GetStringValue(ParamName.RegistryKey);
            ValueName = deserializer.GetStringValue(ParamName.RegistryValueName);
            DefaultValue = (T)deserializer.GetValue(ParamName.RegistryDefaultValue);
        }

        public RegistryBaseKey BaseKey { get; }
        public string Key { get; }
        public string ValueName { get; }
        public T DefaultValue { get; }

        protected override void SerializeParams(IDictionary<string, object> valueSet)
        {
            valueSet.Add(ParamName.RegistryBaseKey.ToString(), BaseKey.ToString());
            valueSet.Add(ParamName.RegistryKey.ToString(), Key);
            valueSet.Add(ParamName.RegistryValueName.ToString(), ValueName);
            valueSet.Add(ParamName.RegistryDefaultValue.ToString(), DefaultValue);
        }
    }

    /// <summary>
    /// Command that reads an integer value (REG_DWORD) from the Windows Registry.
    /// </summary>
    public sealed class RegistryReadIntValueCommand : RegistryReadValueCommand<int>, IRegistryReadIntValueCommand
    {
        public RegistryReadIntValueCommand(RegistryBaseKey baseKey, string key, string valueName, int defaultValue)
            : base(ServiceCommandName.RegistryReadIntValue, baseKey, key, valueName, defaultValue)
        {
        }

        internal RegistryReadIntValueCommand(BridgeMessageDeserializer deserializer)
            : base(deserializer)
        {
        }
    }

    /// <summary>
    /// Command that reads a string value (REG_SZ) from the Windows Registry.
    /// </summary>
    public sealed class RegistryReadStringValueCommand : RegistryReadValueCommand<string>, IRegistryReadStringCommand
    {
        public RegistryReadStringValueCommand(RegistryBaseKey baseKey, string key, string valueName, string defaultValue)
            : base(ServiceCommandName.RegistryReadStringValue, baseKey, key, valueName, defaultValue)
        {
        }

        internal RegistryReadStringValueCommand(BridgeMessageDeserializer deserializer)
            : base(deserializer)
        {
        }
    }
}
