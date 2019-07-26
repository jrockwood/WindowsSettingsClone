// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandExecutor.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors
{
    using ServiceContracts.CommandBridge;

    public interface ICommandExecutor
    {
        bool CanExecute(IServiceCommand command);

        IServiceCommandResponse Execute(IServiceCommand command);
    }
}
