// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryCommandExecutor.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.Registry
{
    using System;
    using Microsoft.Win32;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Logging;
    using Shared.CommandBridge;
    using Shared.Commands;
    using Shared.Diagnostics;
    using Shared.Logging;

    /// <summary>
    /// Executes registry commands.
    /// </summary>
    public class RegistryCommandExecutor
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly ILogger _logger;
        private readonly IWin32Registry _registry;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryCommandExecutor"/> class using the specified registry.
        /// </summary>
        /// <param name="registry">A registry to use (the real Windows registry is used if null).</param>
        /// <param name="logger">The logger to use.</param>
        public RegistryCommandExecutor(IWin32Registry registry = null, ILogger logger = null)
        {
            _registry = registry ?? new Win32Registry();
            _logger = logger ?? new NullLogger();
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Reads a value from the Windows registryKey.
        /// </summary>
        /// <typeparam name="T">The type of value that is read (<see cref="RegistryReadValueCommand{T}"/>).</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="logger">The logger to use.</param>
        /// <returns>A response indicating success and the read value, or failure with error details.</returns>
        public IServiceCommandResponse ExecuteRead<T>(RegistryReadValueCommand<T> command, ILogger logger)
        {
            Param.VerifyNotNull(command, nameof(command));
            Param.VerifyNotNull(logger, nameof(logger));

            ServiceCommandResponse response;

            try
            {
                var registryHive = (RegistryHive)Enum.Parse(typeof(RegistryHive), command.BaseKey.ToString());

                using (IWin32RegistryKey baseKey = _registry.OpenBaseKey(registryHive, RegistryView.Registry64))
                using (IWin32RegistryKey subKey = baseKey.OpenSubKey(command.Key, writable: false))
                {
                    object value = subKey?.GetValue(command.ValueName, command.DefaultValue) ?? command.DefaultValue;
                    response = ServiceCommandResponse.Create(command.CommandName, value);
                }
            }
            catch (Exception e)
            {
                response = ServiceCommandResponse.CreateError(
                    command.CommandName,
                    ServiceErrorInfo.RegistryReadError(
                        RegistryPath.HiveToWin32Name(RegistryPath.BaseKeyToHive(command.BaseKey)),
                        command.Key,
                        command.ValueName,
                        e));
                logger.LogError(response.ErrorMessage);
            }

            return response;
        }

        /// <summary>
        /// Writes a value to the Windows registryKey.
        /// </summary>
        /// <typeparam name="T">The type of value that is written (<see cref="RegistryReadValueCommand{T}"/>).</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="logger">The logger to use.</param>
        /// <returns>A response indicating success and the written value, or failure with error details.</returns>
        public IServiceCommandResponse ExecuteWrite<T>(RegistryWriteValueCommand<T> command, ILogger logger)
        {
            Param.VerifyNotNull(command, nameof(command));
            Param.VerifyNotNull(logger, nameof(logger));

            IServiceCommandResponse response;

            try
            {
                var registryHive = (RegistryHive)Enum.Parse(typeof(RegistryHive), command.BaseKey.ToString());

                using (IWin32RegistryKey baseKey = _registry.OpenBaseKey(registryHive, RegistryView.Registry64))
                using (IWin32RegistryKey subKey = baseKey.CreateSubKey(command.Key, writable: true))
                {
                    subKey.SetValue(command.ValueName, command.Value, TypeToRegistryValueKind(typeof(T)));
                    response = ServiceCommandResponse.Create(command.CommandName, command.Value);
                }
            }
            catch (Exception e)
            {
                response = ServiceCommandResponse.CreateError(
                    command.CommandName,
                    ServiceErrorInfo.RegistryWriteError(
                        RegistryPath.HiveToWin32Name(RegistryPath.BaseKeyToHive(command.BaseKey)),
                        command.Key,
                        command.ValueName,
                        e));
                logger.LogError(response.ErrorMessage);
            }

            return response;
        }

        private static RegistryValueKind TypeToRegistryValueKind(Type type)
        {
            if (type == typeof(string))
            {
                return RegistryValueKind.String;
            }

            if (type == typeof(int) || type == typeof(bool))
            {
                return RegistryValueKind.DWord;
            }

            if (type == typeof(long))
            {
                return RegistryValueKind.QWord;
            }

            return RegistryValueKind.Unknown;
        }
    }
}
