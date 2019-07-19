// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.CommandBridge
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Commands;
    using ServiceContracts.CommandBridge;

    /// <summary>
    /// Represents a command that is sent to the full-trust host application from the UWP application to perform a system
    /// operation that requires full trust.
    /// </summary>
    public abstract class ServiceCommand : IServiceCommand
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

        public static bool TryDeserializeFromValueSet(
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
                    command = new RegistryReadIntValueCommand(deserializer);
                    break;

                case ServiceCommandName.RegistryReadStringValue:
                    command = new RegistryReadStringValueCommand(deserializer);
                    break;

                case ServiceCommandName.RegistryWriteIntValue:
                    command = new RegistryWriteIntValueCommand(deserializer);
                    break;

                case ServiceCommandName.RegistryWriteStringValue:
                    command = new RegistryWriteStringValueCommand(deserializer);
                    break;

                case ServiceCommandName.Unknown:
                default:
                    throw new InvalidOperationException($"Unknown command name: {deserializer.CommandName}");
            }

            if (deserializer.HadError)
            {
                command = null;
            }
            errorResponse = deserializer.LastError;
            return !deserializer.HadError;
        }

        public void SerializeToValueSet(IDictionary<string, object> valueSet)
        {
            valueSet.Add(ParamName.CommandName.ToString(), CommandName.ToString());
            SerializeParams(valueSet);
        }

        public virtual string ToDebugString()
        {
            var valueSet = new Dictionary<string, object>();
            SerializeParams(valueSet);

            var builder = new StringBuilder($"{CommandName}: ");
            foreach (KeyValuePair<string, object> pair in valueSet)
            {
                builder.Append($"{pair.Key}={pair.Value}, ");
            }

            builder.Remove(builder.Length - 2, 2);
            return builder.ToString();
        }

        protected abstract void SerializeParams(IDictionary<string, object> valueSet);
    }
}
