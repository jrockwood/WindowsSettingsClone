// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryWriteStringValueCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

// DO NOT MODIFY THIS FILE DIRECTLY. It is autogenerated from CommandImplementationTemplate.tt.

namespace WindowsSettingsClone.Shared.Commands
{
    using System.Collections.Generic;
    using CommandBridge;
    using Diagnostics;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;

    /// <summary>
    /// Command that writes a string value (REG_SZ) to the Windows Registry.
    /// </summary>
    public sealed class RegistryWriteStringValueCommand : ServiceCommand, IRegistryWriteStringValueCommand
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public RegistryWriteStringValueCommand(RegistryBaseKey baseKey, string key, string valueName, string value)
            : base(ServiceCommandName.RegistryWriteStringValue)
        {
            BaseKey = baseKey;
            Key = Param.VerifyString(key, nameof(key));
            ValueName = Param.VerifyString(valueName, nameof(valueName));
            Value = value;
        }

        internal RegistryWriteStringValueCommand(BridgeMessageDeserializer deserializer)
            : base(ServiceCommandName.RegistryWriteStringValue)
        {
            BaseKey = deserializer.GetEnumValue<RegistryBaseKey>(ParamName.BaseKey);
            Key = deserializer.GetStringValue(ParamName.Key);
            ValueName = deserializer.GetStringValue(ParamName.ValueName);
            Value = deserializer.GetStringValue(ParamName.Value);
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public RegistryBaseKey BaseKey { get; }
        public string Key { get; }
        public string ValueName { get; }
        public string Value { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        internal override void SerializeParams(IDictionary<ParamName, object> valueSet)
        {
            valueSet.Add(ParamName.BaseKey, BaseKey.ToString());
            valueSet.Add(ParamName.Key, Key);
            valueSet.Add(ParamName.ValueName, ValueName);
            valueSet.Add(ParamName.Value, Value);
        }
    }
}