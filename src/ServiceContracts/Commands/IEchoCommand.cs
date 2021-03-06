﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IEchoCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Commands
{
    using CommandBridge;

    /// <summary>
    /// A command that echoes whatever it receives as the response. Useful for testing.
    /// </summary>
    public interface IEchoCommand : IServiceCommand
    {
        string EchoMessage { get; }
    }
}
