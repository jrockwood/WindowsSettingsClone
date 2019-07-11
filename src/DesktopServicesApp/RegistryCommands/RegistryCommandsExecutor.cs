// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryCommandsExecutor.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.DesktopServicesApp.RegistryCommands
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Win32;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Logging;
    using Shared.CommandBridge;
    using Shared.Commands;
    using Shared.Logging;

    internal static class RegistryCommandsExecutor
    {
        public static ServiceCommandResponse ExecuteRead<T>(RegistryReadValueCommand<T> command, ILogger logger)
        {
            ServiceCommandResponse response;

            try
            {
                var registryHive = (RegistryHive)Enum.Parse(typeof(RegistryHive), command.Hive.ToString());

                using (var baseKey = RegistryKey.OpenBaseKey(registryHive, RegistryView.Registry64))
                using (RegistryKey subKey = baseKey.OpenSubKey(command.Key, writable: false))
                {
                    int? value = (int?)subKey?.GetValue(command.ValueName, command.DefaultValue);

                    response = value.HasValue
                       ? ServiceCommandResponse.Create(command.CommandName, value)
                       : ServiceCommandResponse.CreateError(
                           command.CommandName,
                           ServiceErrorInfo.RegistryValueNameNotFound(command.Hive, command.Key, command.ValueName));
                }
            }
            catch (Exception e)
            {
                response = ServiceCommandResponse.CreateError(command.CommandName, e);
                logger.LogError(response.ErrorMessage);
            }

            return response;
        }

        public static async Task<IServiceCommandResponse> ExecuteWriteAsync<T>(
            RegistryWriteValueCommand<T> command,
            ILogger logger)
        {
            IServiceCommandResponse response;

            try
            {
                var elevatedBridge = new ElevatedAppCommunicationBridge();
                response = await elevatedBridge.SendCommandAsync(command, logger);

                //var registryHive = (RegistryHive)Enum.Parse(typeof(RegistryHive), command.Hive.ToString());

                //using (var baseKey = RegistryKey.OpenBaseKey(registryHive, RegistryView.Registry64))
                //using (RegistryKey subKey = baseKey.CreateSubKey(command.Key, writable: true))
                //{
                //    subKey.SetValue(command.ValueName, command.Value, TypeToRegistryValueKind(typeof(T)));
                //    response = ServiceCommandResponse.Create(command.CommandName, command.Value);
                //}
            }
            catch (Exception e)
            {
                response = ServiceCommandResponse.CreateError(command.CommandName, e);
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
