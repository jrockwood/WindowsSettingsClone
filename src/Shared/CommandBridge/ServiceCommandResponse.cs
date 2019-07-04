// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCommandResponse.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.CommandBridge
{
    using System;
    using System.Collections.Generic;
    using ServiceContracts.CommandBridge;

    /// <summary>
    /// Represents the response from a <see cref="ServiceCommand"/>.
    /// </summary>
    public sealed class ServiceCommandResponse : IServiceCommandResponse
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private ServiceCommandResponse(
            ServiceCommandName commandName,
            object result,
            ServiceCommandErrorCode errorCode,
            string errorMessage)
        {
            CommandName = commandName;
            Result = result;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ServiceCommandName CommandName { get; }
        public object Result { get; }

        public bool IsSuccess => ErrorCode == ServiceCommandErrorCode.Success;
        public bool IsError => ErrorCode != ServiceCommandErrorCode.Success;

        public ServiceCommandErrorCode ErrorCode { get; }
        public string ErrorMessage { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static ServiceCommandResponse Create(ServiceCommandName commandName, object result)
        {
            return new ServiceCommandResponse(commandName, result, ServiceCommandErrorCode.Success, null);
        }

        public static ServiceCommandResponse CreateError(
            ServiceCommandName commandName,
            ServiceErrorInfo errorInfo)
        {
            return new ServiceCommandResponse(commandName, null, errorInfo.ErrorCode, errorInfo.ErrorMessage);
        }

        public static ServiceCommandResponse CreateError(ServiceCommandName commandName, Exception exception)
        {
            return CreateError(commandName, ServiceErrorInfo.InternalError(exception.Message));
        }

        public static bool TryDeserialize(
            IDictionary<string, object> valueSet,
            out ServiceCommandResponse response,
            out ServiceCommandResponse errorResponse)
        {
            if (!BridgeMessageDeserializer.TryCreate(
                valueSet,
                out BridgeMessageDeserializer deserializer,
                out errorResponse))
            {
                response = null;
                return false;
            }

            // Check for a non-success ErrorCode, which indicates we're deserializing an error
            if (deserializer.TryGetOptionalEnumValue(ParamName.ErrorCode, out ServiceCommandErrorCode errorCode) &&
                errorCode != ServiceCommandErrorCode.Success)
            {
                string errorMessage = deserializer.GetStringValue(ParamName.ErrorMessage);
                response = deserializer.LastError == null
                    ? new ServiceCommandResponse(deserializer.CommandName, null, errorCode, errorMessage)
                    : null;
            }
            // Deserialize a successful result
            else
            {
                object result = deserializer.GetValue(ParamName.CommandResult);
                response = deserializer.LastError == null
                    ? new ServiceCommandResponse(deserializer.CommandName, result, ServiceCommandErrorCode.Success, null)
                    : null;
            }

            errorResponse = deserializer.LastError;
            return errorResponse == null;
        }

        public void SerializeTo(IDictionary<string, object> valueSet)
        {
            valueSet.Add(ParamName.CommandName.ToString(), CommandName.ToString());
            valueSet.Add(ParamName.CommandResult.ToString(), Result);
            valueSet.Add(ParamName.ErrorCode.ToString(), ErrorCode.ToString());

            if (ErrorCode != ServiceCommandErrorCode.Success)
            {
                valueSet.Add(ParamName.ErrorMessage.ToString(), ErrorMessage);
            }
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if this response represents an error. Does nothing if this
        /// response is a success.
        /// </summary>
        public void ThrowIfError()
        {
            if (IsError)
            {
                throw new InvalidOperationException(ErrorMessage);
            }
        }
    }
}
