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
    using ServiceContracts.Commands;
    using ServiceContracts.Logging;
    using Shared.CommandBridge;
    using Shared.Diagnostics;
    using Shared.Logging;

    /// <summary>
    /// Executes registry commands.
    /// </summary>
    public sealed class RegistryCommandExecutor
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
        /// <param name="command">The command to execute.</param>
        /// <returns>A response indicating success and the read value, or failure with error details.</returns>
        public IServiceCommandResponse ExecuteRead(IRegistryReadIntValueCommand command)
        {
            Param.VerifyNotNull(command, nameof(command));

            return ExecuteRead(
                command.CommandName,
                RegistryPath.CreateValuePath(command.BaseKey, command.Key, command.ValueName, null),
                command.DefaultValue);
        }

        /// <summary>
        /// Reads a value from the Windows registryKey.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A response indicating success and the read value, or failure with error details.</returns>
        public IServiceCommandResponse ExecuteRead(IRegistryReadStringValueCommand command)
        {
            Param.VerifyNotNull(command, nameof(command));

            return ExecuteRead(
                command.CommandName,
                RegistryPath.CreateValuePath(command.BaseKey, command.Key, command.ValueName, null),
                command.DefaultValue);
        }

        /// <summary>
        /// Writes a value to the Windows registryKey.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A response indicating success and the written value, or failure with error details.</returns>
        public IServiceCommandResponse ExecuteWrite(IRegistryWriteIntValueCommand command)
        {
            Param.VerifyNotNull(command, nameof(command));

            return ExecuteWrite(
                command.CommandName,
                RegistryPath.CreateValuePath(command.BaseKey, command.Key, command.ValueName, command.Value));
        }

        /// <summary>
        /// Writes a value to the Windows registryKey.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A response indicating success and the written value, or failure with error details.</returns>
        public IServiceCommandResponse ExecuteWrite(IRegistryWriteStringValueCommand command)
        {
            Param.VerifyNotNull(command, nameof(command));

            return ExecuteWrite(
                command.CommandName,
                RegistryPath.CreateValuePath(command.BaseKey, command.Key, command.ValueName, command.Value));
        }

        private IServiceCommandResponse ExecuteRead(
            ServiceCommandName commandName,
            RegistryPath path,
            object defaultValue)
        {
            ServiceCommandResponse response;

            try
            {
                using (IWin32RegistryKey baseKey = _registry.OpenBaseKey(path.Hive, RegistryView.Registry64))
                using (IWin32RegistryKey subKey = baseKey.OpenSubKey(path.Key, writable: false))
                {
                    object value = subKey?.GetValue(path.ValueName, defaultValue) ?? defaultValue;
                    response = ServiceCommandResponse.Create(commandName, value);
                }
            }
            catch (Exception e)
            {
                response = ServiceCommandResponse.CreateError(
                    commandName,
                    ServiceErrorInfo.RegistryReadError(path.HiveAsWin32Name, path.Key, path.ValueName, e));
                _logger.LogError(response.ErrorMessage);
            }

            return response;
        }

        private IServiceCommandResponse ExecuteWrite(ServiceCommandName commandName, RegistryPath path)
        {
            IServiceCommandResponse response;

            try
            {
                using (IWin32RegistryKey baseKey = _registry.OpenBaseKey(path.Hive, RegistryView.Registry64))
                using (IWin32RegistryKey subKey = baseKey.CreateSubKey(path.Key, writable: true))
                {
                    subKey.SetValue(path.ValueName, path.Value, TypeToRegistryValueKind(path.Value.GetType()));
                    response = ServiceCommandResponse.Create(commandName, path.Value);
                }
            }
            catch (Exception e)
            {
                response = ServiceCommandResponse.CreateError(
                    commandName,
                    ServiceErrorInfo.RegistryWriteError(path.HiveAsWin32Name, path.Key, path.ValueName, e));
                _logger.LogError(response.ErrorMessage);
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
