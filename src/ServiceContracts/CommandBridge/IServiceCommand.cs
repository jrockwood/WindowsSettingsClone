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

        /// <summary>
        /// Serializes the command to a single string. It should be treated as an opaque serialization format to be
        /// deserialized with <c>ServiceCommand.TryDeserializeFromJsonString</c>.
        /// </summary>
        /// <remarks>
        /// This is where I wish C# had the ability to specify a constructor in an interface, which would deserialize the command.
        /// </remarks>
        string SerializeToJsonString();

        /// <summary>
        /// Serializes the command to a dictionary. It should be treated as an opaque serialization format to be
        /// deserialized with <c>ServiceCommand.TryDeserializeFromValueSet</c>.
        /// </summary>
        /// <remarks>
        /// This is where I wish C# had the ability to specify a constructor in an interface, which would deserialize the command.
        /// </remarks>
        void SerializeToValueSet(IDictionary<string, object> valueSet);

        /// <summary>
        /// Returns a string used for logging or debugging that represents the important parts of this command.
        /// </summary>
        string ToDebugString();
    }
}
