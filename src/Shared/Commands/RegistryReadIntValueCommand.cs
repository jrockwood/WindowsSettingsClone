// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryReadIntValueCommand.cs" company="Justin Rockwood">
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
    /// Command that reads an integer value (REG_DWORD) from the Windows Registry.
    /// </summary>
    public sealed class RegistryReadIntValueCommand : ServiceCommand, IRegistryReadIntValueCommand
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public RegistryReadIntValueCommand(RegistryBaseKey baseKey, string key, string valueName, int defaultValue)
            : base(ServiceCommandName.RegistryReadIntValue)
        {
            BaseKey = baseKey;
            Key = Param.VerifyString(key, nameof(key));
            ValueName = Param.VerifyString(valueName, nameof(valueName));
            DefaultValue = defaultValue;
        }

        internal RegistryReadIntValueCommand(BridgeMessageDeserializer deserializer)
            : base(ServiceCommandName.RegistryReadIntValue)
        {
            BaseKey = deserializer.GetEnumValue<RegistryBaseKey>(ParamName.BaseKey);
            Key = deserializer.GetStringValue(ParamName.Key);
            ValueName = deserializer.GetStringValue(ParamName.ValueName);
            DefaultValue = deserializer.GetIntValue(ParamName.DefaultValue);
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public RegistryBaseKey BaseKey { get; }
        public string Key { get; }
        public string ValueName { get; }
        public int DefaultValue { get; }

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
