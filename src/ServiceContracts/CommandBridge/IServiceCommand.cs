// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.CommandBridge
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a command that is sent to the full-trust host application from the UWP application to perform a system
    /// operation that requires full trust.
    /// </summary>
    public interface IServiceCommand
    {
        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        ServiceCommandName CommandName { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        void SerializeTo(IDictionary<string, object> valueSet);
    }
}
