// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandExecutor.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors
{
    using System.Collections.Generic;
    using System.Linq;
    using IO;
    using Registry;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Logging;
    using Shared.CommandBridge;
    using Shared.Commands;
    using Shared.Diagnostics;
    using Shared.Extensions;
    using Shared.Logging;
    using SystemParametersInfo;

    /// <summary>
    /// Executes all known commands. Used in both the full-trust and elevated apps.
    /// </summary>
    public sealed class CommandExecutor : ICommandExecutor
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly ILogger _logger;
        private readonly IReadOnlyList<ICommandExecutor> _executors;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public CommandExecutor(
            ILogger logger,
            IWin32Registry registry = null,
            IWin32SystemParametersInfo systemParametersInfo = null,
            IWin32FileSystem fileSystem = null)
        {
            _logger = Param.VerifyNotNull(logger, nameof(logger));

            _executors = new ICommandExecutor[]
            {
                new RegistryCommandExecutor(registry, logger),
                new SystemParametersInfoCommandExecutor(systemParametersInfo, logger),
                new FileCommandExecutor(fileSystem, logger)
            };
        }

        public bool CanExecute(IServiceCommand command)
        {
            return command.CommandName.IsOneOf(ServiceCommandName.ShutdownServer, ServiceCommandName.Echo) ||
                   _executors.Any(x => x.CanExecute(command));
        }

        public IServiceCommandResponse Execute(IServiceCommand command)
        {
            IServiceCommandResponse response;

            _logger.LogDebug("Executing command: ", command.ToDebugString());
            switch (command)
            {
                case ShutdownServerCommand _:
                    response = ServiceCommandResponse.Create(ServiceCommandName.ShutdownServer, true);
                    break;

                case EchoCommand echoCommand:
                    response = ServiceCommandResponse.Create(ServiceCommandName.Echo, echoCommand.EchoMessage);
                    break;

                default:
                    ICommandExecutor executor = _executors.FirstOrDefault(x => x.CanExecute(command));
                    if (executor != null)
                    {
                        response = executor.Execute(command);
                    }
                    else
                    {
                        _logger.LogWarning("Unsupported command: ", command.CommandName);
                        response = ServiceCommandResponse.CreateError(
                            command.CommandName,
                            ServiceErrorInfo.InternalError(
                                $"Command '{command.CommandName}' is not supported for execution."));
                    }

                    break;
            }

            return response;
        }
    }
}
