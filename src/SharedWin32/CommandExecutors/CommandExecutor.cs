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
    using SystemParametersInfo;

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
        private readonly SystemParametersInfoCommandExecutor _systemParametersInfoCommandExecutor;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public CommandExecutor(ILogger logger, IWin32Registry registry = null, IWin32SystemParametersInfo systemParametersInfo = null)
        {
            _logger = Param.VerifyNotNull(logger, nameof(logger));
            _registryExecutor = new RegistryCommandExecutor(registry, logger);
            _systemParametersInfoCommandExecutor =
                new SystemParametersInfoCommandExecutor(systemParametersInfo, logger);
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
                    response = _registryExecutor.ExecuteRead(registryCommand);
                    break;

                case RegistryReadStringValueCommand registryCommand:
                    response = _registryExecutor.ExecuteRead(registryCommand);
                    break;

                case RegistryWriteIntValueCommand registryCommand:
                    response = _registryExecutor.ExecuteWrite(registryCommand);
                    break;

                case SystemParametersInfoGetValueCommand systemParametersInfoCommand:
                    response = _systemParametersInfoCommandExecutor.ExecuteGet(systemParametersInfoCommand);
                    break;

                case SystemParametersInfoSetValueCommand systemParametersInfoCommand:
                    response = _systemParametersInfoCommandExecutor.ExecuteSet(systemParametersInfoCommand);
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
