// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandExecutor.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors
{
    using Registry;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Logging;
    using Shared.CommandBridge;
    using Shared.Commands;
    using Shared.Diagnostics;
    using Shared.Logging;

    /// <summary>
    /// Executes all known commands. Used in both the full-trust and elevated apps.
    /// </summary>
    public class CommandExecutor
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly ILogger _logger;
        private readonly RegistryCommandExecutor _registryExecutor;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public CommandExecutor(ILogger logger, IWin32Registry registry = null)
        {
            _logger = Param.VerifyNotNull(logger, nameof(logger));
            _registryExecutor = new RegistryCommandExecutor(registry);
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

                case RegistryReadIntValueCommand registryCommand:
                    response = _registryExecutor.ExecuteRead(registryCommand, _logger);
                    break;

                case RegistryReadStringValueCommand registryCommand:
                    response = _registryExecutor.ExecuteRead(registryCommand, _logger);
                    break;

                case RegistryWriteIntValueCommand registryCommand:
                    response = _registryExecutor.ExecuteWrite(registryCommand, _logger);
                    break;

                default:
                    _logger.LogWarning("Unsupported command: ", command.CommandName);
                    response = ServiceCommandResponse.CreateError(
                        command.CommandName,
                        ServiceErrorInfo.InternalError(
                            $"Command '{command.CommandName}' is not supported for execution in the elevated bridge application."));
                    break;
            }

            return response;
        }
    }
}
