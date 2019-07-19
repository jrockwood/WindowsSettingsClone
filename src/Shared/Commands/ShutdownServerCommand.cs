// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ShutdownServerCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Commands
{
    using System.Collections.Generic;
    using CommandBridge;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;

    /// <summary>
    /// Command that requests the server to shutdown and terminate.
    /// </summary>
    public sealed class ShutdownServerCommand : ServiceCommand, IShutdownServerCommand
    {
        public ShutdownServerCommand()
            : base(ServiceCommandName.ShutdownServer)
        {
        }

        internal override void SerializeParams(IDictionary<ParamName, object> valueSet)
        {
            // No parameters to serialize.
        }
    }
}
