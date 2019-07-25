// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryReadStringValueCommand.cs" company="Justin Rockwood">
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
    /// Command that reads a string value (REG_SZ) from the Windows Registry.
    /// </summary>
    public sealed class RegistryReadStringValueCommand : ServiceCommand, IRegistryReadStringValueCommand
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public RegistryReadStringValueCommand(RegistryBaseKey baseKey, string key, string valueName, string defaultValue)
            : base(ServiceCommandName.RegistryReadStringValue)
        {
            BaseKey = baseKey;
            Key = Param.VerifyString(key, nameof(key));
            ValueName = Param.VerifyString(valueName, nameof(valueName));
            DefaultValue = defaultValue;
        }

        internal RegistryReadStringValueCommand(BridgeMessageDeserializer deserializer)
            : base(ServiceCommandName.RegistryReadStringValue)
        {
            BaseKey = deserializer.GetEnumValue<RegistryBaseKey>(ParamName.BaseKey);
            Key = deserializer.GetStringValue(ParamName.Key);
            ValueName = deserializer.GetStringValue(ParamName.ValueName);
            DefaultValue = deserializer.GetStringValue(ParamName.DefaultValue);
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public RegistryBaseKey BaseKey { get; }
        public string Key { get; }
        public string ValueName { get; }
        public string DefaultValue { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        internal override void SerializeParams(IDictionary<ParamName, object> valueSet)
        {
            valueSet.Add(ParamName.BaseKey, BaseKey.ToString());
            valueSet.Add(ParamName.Key, Key);
            valueSet.Add(ParamName.ValueName, ValueName);
            valueSet.Add(ParamName.DefaultValue, DefaultValue);
        }
    }
}
