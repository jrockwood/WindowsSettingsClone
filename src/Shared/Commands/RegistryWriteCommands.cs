// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryWriteCommands.cs" company="Justin Rockwood">
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
    public abstract class RegistryWriteValueCommand<T> : ServiceCommand
    {
        protected RegistryWriteValueCommand(
            ServiceCommandName commandName,
            RegistryBaseKey baseKey,
            string key,
            string valueName,
            T value)
            : base(commandName)
        {
            BaseKey = baseKey;
            Key = Param.VerifyString(key, nameof(key));
            ValueName = valueName;
            Value = value;
        }

        internal RegistryWriteValueCommand(BridgeMessageDeserializer deserializer)
            : base(deserializer.CommandName)
        {
            BaseKey = deserializer.GetEnumValue<RegistryBaseKey>(ParamName.RegistryBaseKey);
            Key = deserializer.GetStringValue(ParamName.RegistryKey);
            ValueName = deserializer.GetStringValue(ParamName.RegistryValueName);
            Value = (T)deserializer.GetValue(ParamName.RegistryValue);
        }

        public RegistryBaseKey BaseKey { get; }
        public string Key { get; }
        public string ValueName { get; }
        public T Value { get; }

        protected override void SerializeParams(IDictionary<string, object> valueSet)
        {
            valueSet.Add(ParamName.RegistryBaseKey.ToString(), BaseKey.ToString());
            valueSet.Add(ParamName.RegistryKey.ToString(), Key);
            valueSet.Add(ParamName.RegistryValueName.ToString(), ValueName);
            valueSet.Add(ParamName.RegistryValue.ToString(), Value);
        }
    }

    /// <summary>
    /// Command that writes an integer value (REG_DWORD) to the Windows Registry.
    /// </summary>
    public sealed class RegistryWriteIntValueCommand : RegistryWriteValueCommand<int>, IRegistryWriteIntValueCommand
    {
        public RegistryWriteIntValueCommand(RegistryBaseKey baseKey, string key, string valueName, int value)
            : base(ServiceCommandName.RegistryWriteIntValue, baseKey, key, valueName, value)
        {
        }

        internal RegistryWriteIntValueCommand(BridgeMessageDeserializer deserializer)
            : base(deserializer)
        {
        }
    }

    /// <summary>
    /// Command that reads a string value (REG_SZ) from the Windows Registry.
    /// </summary>
    public sealed class RegistryWriteStringValueCommand : RegistryWriteValueCommand<string>, IRegistryWriteStringCommand
    {
        public RegistryWriteStringValueCommand(RegistryBaseKey baseKey, string key, string valueName, string value)
            : base(ServiceCommandName.RegistryWriteStringValue, baseKey, key, valueName, value)
        {
        }

        internal RegistryWriteStringValueCommand(BridgeMessageDeserializer deserializer)
            : base(deserializer)
        {
        }
    }
}
