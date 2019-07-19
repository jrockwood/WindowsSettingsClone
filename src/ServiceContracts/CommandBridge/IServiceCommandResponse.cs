// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceCommandResponse.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.CommandBridge
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the response from a <see cref="IServiceCommand"/>.
    /// </summary>
    public interface IServiceCommandResponse
    {
        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        ServiceCommandName CommandName { get; }
        object Result { get; }

        bool IsSuccess { get; }
        bool IsError { get; }

        ServiceCommandErrorCode ErrorCode { get; }
        string ErrorMessage { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        void SerializeToValueSet(IDictionary<string, object> valueSet);

        /// <summary>
        /// Returns a string used for logging or debugging that represents the important parts of this command.
        /// </summary>
        string ToDebugString();

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if this response represents an error. Does nothing if this
        /// response is a success.
        /// </summary>
        void ThrowIfError();
    }
}
