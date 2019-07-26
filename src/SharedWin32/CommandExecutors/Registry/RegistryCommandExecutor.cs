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
    using Shared.Extensions;
    using Shared.Logging;

    /// <summary>
    /// Executes registry commands.
    /// </summary>
    internal sealed class RegistryCommandExecutor : ICommandExecutor
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

        public bool CanExecute(IServiceCommand command)
        {
            return command.CommandName.IsOneOf(
                ServiceCommandName.RegistryReadIntValue,
                ServiceCommandName.RegistryReadStringValue,
                ServiceCommandName.RegistryWriteIntValue,
                ServiceCommandName.RegistryWriteStringValue);
        }

        public IServiceCommandResponse Execute(IServiceCommand command)
        {
            Param.VerifyNotNull(command, nameof(command));

            switch (command)
            {
                case IRegistryReadIntValueCommand cmd:
                    return ExecuteRead(
                        command.CommandName,
                        RegistryPath.CreateValuePath(cmd.BaseKey, cmd.Key, cmd.ValueName, null),
                        cmd.DefaultValue);

                case IRegistryReadStringValueCommand cmd:
                    return ExecuteRead(
                        command.CommandName,
                        RegistryPath.CreateValuePath(cmd.BaseKey, cmd.Key, cmd.ValueName, null),
                        cmd.DefaultValue);

                case IRegistryWriteIntValueCommand cmd:
                    return ExecuteWrite(
                        command.CommandName,
                        RegistryPath.CreateValuePath(cmd.BaseKey, cmd.Key, cmd.ValueName, cmd.Value));

                case IRegistryWriteStringValueCommand cmd:
                    return ExecuteWrite(
                        command.CommandName,
                        RegistryPath.CreateValuePath(cmd.BaseKey, cmd.Key, cmd.ValueName, cmd.Value));

                default:
                    throw new ArgumentException($"Unsupported command '{command.CommandName}'.");
            }
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
