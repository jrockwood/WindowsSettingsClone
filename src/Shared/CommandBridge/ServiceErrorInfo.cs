// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceErrorInfo.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.CommandBridge
{
    using System;
    using System.Globalization;
    using ServiceContracts.CommandBridge;

    /// <summary>
    /// Contains error information about a specific <see cref="ServiceCommandErrorCode"/>.
    /// </summary>
    public sealed class ServiceErrorInfo
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private ServiceErrorInfo(ServiceCommandErrorCode errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ServiceCommandErrorCode ErrorCode { get; }
        public string ErrorMessage { get; }

        //// ===========================================================================================================
        //// Factory Methods
        //// ===========================================================================================================

        public static ServiceErrorInfo InternalError(string message)
        {
            message = string.Format(CultureInfo.CurrentCulture, Strings.InternalError, message);
            return new ServiceErrorInfo(ServiceCommandErrorCode.InternalError, message);
        }

        public static ServiceErrorInfo RegistryReadError(string hive, string key, string valueName, Exception exception)
        {
            string message = string.Format(
                CultureInfo.CurrentCulture,
                Strings.RegistryReadError,
                $"{hive}\\{key}",
                valueName,
                $"{exception.GetType()}: {exception.Message}");

            return new ServiceErrorInfo(ServiceCommandErrorCode.RegistryReadError, message);
        }

        public static ServiceErrorInfo RegistryWriteError(string hive, string key, string valueName, Exception exception)
        {
            string message = string.Format(
                CultureInfo.CurrentCulture,
                Strings.RegistryWriteError,
                $"{hive}\\{key}",
                valueName,
                $"{exception.GetType()}: {exception.Message}");

            return new ServiceErrorInfo(ServiceCommandErrorCode.RegistryWriteError, message);
        }

        internal static ServiceErrorInfo MissingRequiredMessageValue(ParamName paramName)
        {
            string message = string.Format(CultureInfo.CurrentCulture, Strings.MissingRequiredMessageValue, paramName);
            return new ServiceErrorInfo(ServiceCommandErrorCode.MissingRequiredMessageValue, message);
        }

        internal static ServiceErrorInfo WrongMessageValueType(ParamName paramName, Type actualType, Type expectedType)
        {
            string message = string.Format(
                CultureInfo.CurrentCulture,
                Strings.WrongMessageValueType,
                paramName,
                actualType,
                expectedType);
            return new ServiceErrorInfo(ServiceCommandErrorCode.WrongMessageValueType, message);
        }
    }
}
