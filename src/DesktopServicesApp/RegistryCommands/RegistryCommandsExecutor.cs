// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryCommandsExecutor.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.DesktopServicesApp.RegistryCommands
{
    using System;
    using Microsoft.Win32;
    using ServiceContracts.Logging;
    using Shared.CommandBridge;
    using Shared.Commands;
    using Shared.Logging;

    internal static class RegistryCommandsExecutor
    {
        public static ServiceCommandResponse Execute(RegistryReadIntValueCommand command, ILogger logger)
        {
            return ExecuteInternal(command, logger);
        }

        public static ServiceCommandResponse Execute(RegistryReadStringValueCommand command, ILogger logger)
        {
            return ExecuteInternal(command, logger);
        }

        private static ServiceCommandResponse ExecuteInternal<T>(RegistryReadValueCommand<T> command, ILogger logger)
        {
            ServiceCommandResponse response;

            try
            {
                var registryHive = (RegistryHive)Enum.Parse(
                    typeof(RegistryHive),
                    command.Hive.ToString());

                using (var baseKey = RegistryKey.OpenBaseKey(registryHive, RegistryView.Registry64))
                using (RegistryKey subKey = baseKey.OpenSubKey(command.Key, writable: false))
                {
                    int? value = (int?)subKey?.GetValue(
                        command.ValueName,
                        command.DefaultValue);

                    response = value.HasValue
                       ? ServiceCommandResponse.Create(command.CommandName, value)
                       : ServiceCommandResponse.CreateError(
                           command.CommandName,
                           ServiceErrorInfo.RegistryValueNameNotFound(
                               command.Hive,
                               command.Key,
                               command.ValueName));
                }
            }
            catch (Exception e)
            {
                response = ServiceCommandResponse.CreateError(command.CommandName, e);
                logger.LogError(response.ErrorMessage);
            }

            return response;
        }
    }
}
