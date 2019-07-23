// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IShutdownServerCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Commands
{
    using CommandBridge;

    /// <summary>
    /// Command that requests the server to shutdown and terminate.
    /// </summary>
    public interface IShutdownServerCommand : IServiceCommand
    {
    }
}
