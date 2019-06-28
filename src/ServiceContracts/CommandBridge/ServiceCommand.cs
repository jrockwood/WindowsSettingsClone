// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.CommandBridge
{
    using System;
    using System.Collections.Generic;
    using Commands;

    /// <summary>
    /// Represents a command that is sent to the full-trust host application from the UWP application to perform a system
    /// operation that requires full trust.
    /// </summary>
    public abstract class ServiceCommand
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        protected ServiceCommand(ServiceCommandName commandName)
        {
            CommandName = commandName;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ServiceCommandName CommandName { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static bool TryDeserialize(
            IDictionary<string, object> valueSet,
            out ServiceCommand command,
            out ServiceCommandResponse errorResponse)
        {
            if (!BridgeMessageDeserializer.TryCreate(
                valueSet,
                out BridgeMessageDeserializer deserializer,
                out errorResponse))
            {
                command = null;
                return false;
            }

            switch (deserializer.CommandName)
            {
                case ServiceCommandName.RegistryReadIntValue:
                    RegistryHive hive = deserializer.GetEnumValue<RegistryHive>(ParamName.RegistryHive);
                    string key = deserializer.GetStringValue(ParamName.RegistryKey);
                    string valueName = deserializer.GetStringValue(ParamName.RegistryValueName);
                    int defaultValue = deserializer.GetIntValue(ParamName.RegistryDefaultValue);
                    command = deserializer.HadError
                        ? null
                        : new RegistryReadIntValueCommand(hive, key, valueName, defaultValue);
                    errorResponse = deserializer.LastError;
                    return !deserializer.HadError;

                default:
                    throw new InvalidOperationException("Should not be hit");
            }
        }

        public void SerializeTo(IDictionary<string, object> valueSet)
        {
            valueSet.Add(ParamName.CommandName.ToString(), CommandName.ToString());
            SerializeParams(valueSet);
        }

        protected abstract void SerializeParams(IDictionary<string, object> valueSet);
    }
}
