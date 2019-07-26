// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemParametersInfoCommandExecutor.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.SystemParametersInfo
{
    using System;
    using System.Text;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using ServiceContracts.Logging;
    using Shared.CommandBridge;
    using Shared.Extensions;
    using Shared.Logging;

    /// <summary>
    /// Executes <see cref="ISystemParametersInfoGetValueCommand"/> and <see
    /// cref="ISystemParametersInfoSetValueCommand"/> commands.
    /// </summary>
    internal sealed class SystemParametersInfoCommandExecutor : ICommandExecutor
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly ILogger _logger;
        private readonly IWin32SystemParametersInfo _win32Invoker;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemParametersInfoCommandExecutor"/> class using the specified
        /// win32 API invoker.
        /// </summary>
        /// <param name="win32Invoker">A Win32 API invoker to use (the real Windows API is used if null).</param>
        /// <param name="logger">The logger to use.</param>
        public SystemParametersInfoCommandExecutor(IWin32SystemParametersInfo win32Invoker = null, ILogger logger = null)
        {
            _win32Invoker = win32Invoker ?? new Win32SystemParametersInfo();
            _logger = logger ?? new NullLogger();
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public bool CanExecute(IServiceCommand command)
        {
            return command.CommandName.IsOneOf(
                ServiceCommandName.SystemParametersInfoGetValue,
                ServiceCommandName.SystemParametersInfoSetValue);
        }

        public IServiceCommandResponse Execute(IServiceCommand command)
        {
            switch (command)
            {
                case ISystemParametersInfoGetValueCommand cmd:
                    return ExecuteGet(cmd);

                case ISystemParametersInfoSetValueCommand cmd:
                    return ExecuteSet(cmd);

                default:
                    throw new ArgumentException($"Unsupported command '{command.CommandName}'.");
            }
        }

        private IServiceCommandResponse ExecuteGet(ISystemParametersInfoGetValueCommand getCommand)
        {
            var buffer = new StringBuilder(NativeMethods.MaxPath, NativeMethods.MaxPath);

            switch (getCommand.SystemParameter)
            {
                case SystemParameterInfoKind.GetDesktopWallpaper:
                    _win32Invoker.SystemParametersInfo(
                        getCommand.SystemParameter,
                        (uint)buffer.Capacity,
                        buffer,
                        SystemParameterInfoUpdateKind.None);
                    break;

                default:
                    return ServiceCommandResponse.CreateError(
                        getCommand.CommandName,
                        new InvalidOperationException($"Unknown system parameter: {getCommand.SystemParameter}"));
            }

            IServiceCommandResponse response;
            try
            {
                _win32Invoker.ThrowExceptionForLastWin32Error();
                response = ServiceCommandResponse.Create(getCommand.CommandName, buffer.ToString());
            }
            catch (Exception e)
            {
                response = ServiceCommandResponse.CreateError(getCommand.CommandName, e);
            }

            return response;
        }

        private IServiceCommandResponse ExecuteSet(ISystemParametersInfoSetValueCommand setCommand)
        {
            return null;
        }
    }
}
