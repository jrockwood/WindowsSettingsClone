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
    using Shared.CommandBridge;

    /// <summary>
    /// Executes <see cref="ISystemParametersInfoGetValueCommand"/> and <see cref="ISystemParametersInfoSetValueCommand"/> commands.
    /// </summary>
    public class SystemParametersInfoCommandExecutor
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly IWin32SystemParametersInfo _win32Invoker;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemParametersInfoCommandExecutor"/> class using the specified
        /// win32 API invoker.
        /// </summary>
        /// <param name="win32Invoker">A Win32 API invoker to use (the real Windows API is used if null).</param>
        public SystemParametersInfoCommandExecutor(IWin32SystemParametersInfo win32Invoker = null)
        {
            _win32Invoker = win32Invoker ?? new Win32SystemParametersInfo();
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public IServiceCommandResponse ExecuteGet(ISystemParametersInfoGetValueCommand getCommand)
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

        public IServiceCommandResponse ExecuteSet(ISystemParametersInfoSetValueCommand setCommand)
        {
            return null;
        }
    }
}
