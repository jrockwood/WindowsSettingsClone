// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BridgeMessageDeserializer.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.CommandBridge
{
    using System;
    using System.Collections.Generic;
    using Diagnostics;
    using Newtonsoft.Json.Linq;
    using ServiceContracts.CommandBridge;

    /// <summary>
    /// Deserializes a low-level message that gets passed across the wire from the client to the server ( <see
    /// cref="ServiceCommand"/>) and the response from the server to the client ( <see cref="ServiceCommandResponse"/>).
    /// </summary>
    internal sealed class BridgeMessageDeserializer
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private BridgeMessageDeserializer(IDictionary<string, object> valueSet, ServiceCommandName commandName)
        {
            ValueSet = valueSet;
            CommandName = commandName;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ServiceCommandName CommandName { get; }
        public bool HadError => LastError != null;
        public IServiceCommandResponse LastError { get; private set; }
        public IDictionary<string, object> ValueSet { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static bool TryCreateFromJsonString(
            string jsonString,
            out BridgeMessageDeserializer deserializer,
            out IServiceCommandResponse errorResponse)
        {
            Param.VerifyString(jsonString, nameof(jsonString));

            var valueSet = new Dictionary<string, object>();

            try
            {
                // Try to parse the JSON.
                var jsonObject = JObject.Parse(jsonString);

                // Populate a value set from the parsed JSON.
                foreach (KeyValuePair<string, JToken> pair in jsonObject)
                {
                    valueSet.Add(pair.Key, ((JValue)pair.Value).Value);
                }
            }
            catch (Exception e)
            {
                deserializer = null;
                errorResponse = ServiceCommandResponse.CreateError(ServiceCommandName.Unknown, e);
                return false;
            }

            return TryCreateFromValueSet(valueSet, out deserializer, out errorResponse);
        }

        public static bool TryCreateFromValueSet(
            IDictionary<string, object> valueSet,
            out BridgeMessageDeserializer deserializer,
            out IServiceCommandResponse errorResponse)
        {
            Param.VerifyNotNull(valueSet, nameof(valueSet));

            if (!TryGetCommandName(valueSet, out ServiceCommandName commandName, out errorResponse))
            {
                deserializer = null;
                return false;
            }

            deserializer = new BridgeMessageDeserializer(valueSet, commandName);
            return true;
        }

        public bool TryGetOptionalEnumValue<T>(ParamName paramName, out T value) where T : struct
        {
            if (ValueSet.TryGetValue(paramName.ToString(), out object rawValue))
            {
                return TryConvertEnumValue(paramName, rawValue, out value);
            }

            value = default(T);
            return false;
        }

        public T GetEnumValue<T>(ParamName paramName) where T : struct
        {
            if (!GetRequiredValue(paramName, out object rawValue))
            {
                return default(T);
            }

            TryConvertEnumValue(paramName, rawValue, out T value);
            return value;
        }

        public int GetIntValue(ParamName paramName)
        {
            if (!GetRequiredValue(paramName, out object rawValue))
            {
                return 0;
            }

            if (rawValue is int value)
            {
                return value;
            }

            LastError = ServiceCommandResponse.CreateError(
                CommandName,
                ServiceErrorInfo.WrongMessageValueType(paramName, rawValue.GetType(), typeof(int)));
            return 0;
        }

        public bool GetBoolValue(ParamName paramName)
        {
            if (!GetRequiredValue(paramName, out object rawValue))
            {
                return false;
            }

            if (rawValue is bool value)
            {
                return value;
            }

            LastError = ServiceCommandResponse.CreateError(
                CommandName,
                ServiceErrorInfo.WrongMessageValueType(paramName, rawValue.GetType(), typeof(bool)));
            return false;
        }

        public string GetStringValue(ParamName paramName)
        {
            if (!GetRequiredValue(paramName, out object rawValue))
            {
                return null;
            }

            if (rawValue is string value)
            {
                return value;
            }

            LastError = ServiceCommandResponse.CreateError(
                CommandName,
                ServiceErrorInfo.WrongMessageValueType(paramName, rawValue.GetType(), typeof(string)));
            return null;
        }

        public object GetValue(ParamName paramName)
        {
            GetRequiredValue(paramName, out object value);
            return value;
        }

        private static bool TryGetCommandName(
            IDictionary<string, object> valueSet,
            out ServiceCommandName commandName,
            out IServiceCommandResponse errorResponse)
        {
            if (!valueSet.TryGetValue(ParamName.CommandName.ToString(), out object rawValue))
            {
                errorResponse = ServiceCommandResponse.CreateError(
                    ServiceCommandName.Unknown,
                    ServiceErrorInfo.MissingRequiredMessageValue(ParamName.CommandName));
                commandName = ServiceCommandName.Unknown;
                return false;
            }

            if (!Enum.TryParse(rawValue.ToString(), out commandName))
            {
                errorResponse = ServiceCommandResponse.CreateError(
                    ServiceCommandName.Unknown,
                    ServiceErrorInfo.WrongMessageValueType(
                        ParamName.CommandName,
                        rawValue.GetType(),
                        typeof(ServiceCommandName)));
                commandName = ServiceCommandName.Unknown;
                return false;
            }

            errorResponse = null;
            return true;
        }

        private bool GetRequiredValue(ParamName paramName, out object value)
        {
            if (ValueSet.TryGetValue(paramName.ToString(), out value))
            {
                return true;
            }

            LastError = ServiceCommandResponse.CreateError(
                CommandName,
                ServiceErrorInfo.MissingRequiredMessageValue(paramName));
            return false;
        }

        private bool TryConvertEnumValue<T>(ParamName paramName, object rawValue, out T value) where T : struct
        {
            try
            {
                value = (T)Enum.Parse(typeof(T), rawValue.ToString());
                return true;
            }
            catch (Exception e) when (e is ArgumentException || e is OverflowException)
            {
                LastError = ServiceCommandResponse.CreateError(
                    CommandName,
                    ServiceErrorInfo.WrongMessageValueType(paramName, rawValue.GetType(), typeof(T)));
                value = default(T);
                return false;
            }
        }
    }
}
