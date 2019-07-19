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
    using Newtonsoft.Json;
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

        public static bool TryDeserializeFromJsonString(
            string jsonString,
            out IServiceCommand command,
            out IServiceCommandResponse errorResponse)
        {
            if (!BridgeMessageDeserializer.TryCreateFromJsonString(
                jsonString,
                out BridgeMessageDeserializer deserializer,
                out errorResponse))
            {
                command = null;
                return false;
            }

            return TryDeserialize(deserializer, out command, out errorResponse);
        }

        public static bool TryDeserializeFromValueSet(
            IDictionary<string, object> valueSet,
            out IServiceCommand command,
            out IServiceCommandResponse errorResponse)
        {
            if (!BridgeMessageDeserializer.TryCreateFromValueSet(
                valueSet,
                out BridgeMessageDeserializer deserializer,
                out errorResponse))
            {
                command = null;
                return false;
            }

            return TryDeserialize(deserializer, out command, out errorResponse);
        }

        private static bool TryDeserialize(
            BridgeMessageDeserializer deserializer,
            out IServiceCommand command,
            out IServiceCommandResponse errorResponse)
        {
            switch (deserializer.CommandName)
            {
                case ServiceCommandName.Echo:
                    command = new EchoCommand(deserializer);
                    break;

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
                    throw new InvalidOperationException(
                        "This should be unreachable because the deserializer should have detected an invalid command name");
            }

            if (deserializer.HadError)
            {
                command = null;
            }
            errorResponse = deserializer.LastError;
            return !deserializer.HadError;
        }

        /// <summary>
        /// Serializes the response to a single string. It should be treated as an opaque serialization format to be
        /// deserialized with <see cref="TryDeserializeFromJsonString"/>.
        /// </summary>
        public string SerializeToJsonString()
        {
            var valueSet = new Dictionary<string, object>();
            SerializeToValueSet(valueSet);

            string jsonString = JsonConvert.SerializeObject(valueSet, Formatting.None);
            return jsonString;
        }

        /// <summary>
        /// Serializes the response to a dictionary. It should be treated as an opaque serialization format to be
        /// deserialized with <see cref="TryDeserializeFromValueSet"/>.
        /// </summary>
        public void SerializeToValueSet(IDictionary<string, object> valueSet)
        {
            var allParams = new Dictionary<ParamName, object>
            {
                [ParamName.CommandName] = CommandName.ToString(),
            };

            SerializeParams(allParams);

            foreach (KeyValuePair<ParamName, object> pair in allParams)
            {
                valueSet.Add(pair.Key.ToString(), pair.Value);
            }
        }

        public virtual string ToDebugString()
        {
            var valueSet = new Dictionary<ParamName, object>();
            SerializeParams(valueSet);

            var builder = new StringBuilder($"{CommandName}: ");
            foreach (KeyValuePair<ParamName, object> pair in valueSet)
            {
                builder.Append($"{pair.Key}={pair.Value}, ");
            }

            builder.Remove(builder.Length - 2, 2);
            return builder.ToString();
        }

        internal abstract void SerializeParams(IDictionary<ParamName, object> valueSet);
    }
}
