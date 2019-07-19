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
    using Shared.Commands;
    using Shared.Diagnostics;
    using Shared.Logging;

    /// <summary>
    /// Executes registryKey commands.
    /// </summary>
    public class RegistryCommandExecutor
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly IWin32Registry _registry;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryCommandExecutor"/> class using the specified registry.
        /// </summary>
        /// <param name="registry">A registry to use (the real Windows registry is used if null).</param>
        public RegistryCommandExecutor(IWin32Registry registry = null)
        {
            _registry = registry ?? new Win32Registry();
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static RegistryHive BaseKeyToHive(RegistryBaseKey baseKey)
        {
            switch (baseKey)
            {
                case RegistryBaseKey.ClassesRoot:
                    return RegistryHive.ClassesRoot;

                case RegistryBaseKey.CurrentUser:
                    return RegistryHive.CurrentUser;

                case RegistryBaseKey.LocalMachine:
                    return RegistryHive.LocalMachine;

                case RegistryBaseKey.Users:
                    return RegistryHive.Users;

                case RegistryBaseKey.PerformanceData:
                    return RegistryHive.PerformanceData;

                case RegistryBaseKey.CurrentConfig:
                    return RegistryHive.CurrentConfig;

                case RegistryBaseKey.DynData:
                    return RegistryHive.DynData;

                default:
                    throw new ArgumentOutOfRangeException(nameof(baseKey), baseKey, null);
            }
        }

        public static string HiveToWin32Name(RegistryHive hive)
        {
            switch (hive)
            {
                case RegistryHive.ClassesRoot:
                    return "HKEY_CLASSES_ROOT";

                case RegistryHive.CurrentUser:
                    return "HKEY_CURRENT_USER";

                case RegistryHive.LocalMachine:
                    return "HKEY_LOCAL_MACHINE";

                case RegistryHive.Users:
                    return "HKEY_USERS";

                case RegistryHive.PerformanceData:
                    return "HKEY_PERFORMANCE_DATA";

                case RegistryHive.CurrentConfig:
                    return "HKEY_CURRENT_CONFIG";

                case RegistryHive.DynData:
                    return "HKEY_DYN_DATA";

                default:
                    throw new ArgumentOutOfRangeException(nameof(hive), hive, null);
            }
        }

        public static RegistryHive Win32NameToHive(string win32HiveName)
        {
            switch (win32HiveName.ToUpperInvariant())
            {
                case "HKCR":
                case "HKEY_CLASSES_ROOT":
                    return RegistryHive.ClassesRoot;

                case "HKCU":
                case "HKEY_CURRENT_USER":
                    return RegistryHive.CurrentUser;

                case "HKLM":
                case "HKEY_LOCAL_MACHINE":
                    return RegistryHive.LocalMachine;

                case "HKU":
                case "HKEY_USERS":
                    return RegistryHive.Users;

                case "HKEY_CURRENT_CONFIG":
                    return RegistryHive.CurrentConfig;

                case "HKEY_PERFORMANCE_DATA":
                    return RegistryHive.PerformanceData;

                case "HKEY_DYN_DATA":
                    return RegistryHive.DynData;

                default:
                    throw new InvalidOperationException($"Unknown registry hive: {win32HiveName}");
            }
        }

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
                        HiveToWin32Name(BaseKeyToHive(command.BaseKey)),
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
                        HiveToWin32Name(BaseKeyToHive(command.BaseKey)),
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
