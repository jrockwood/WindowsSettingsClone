// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCommandErrorCode.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.CommandBridge
{
    /// <summary>
    /// Enumerates the different kinds of errors a <see cref="IServiceCommand"/> can encounter.
    /// </summary>
    public enum ServiceCommandErrorCode
    {
        // Negative errors indicate an internal error that is a little more specific

        InternalError = -1,
        MissingRequiredMessageValue = -2,
        WrongMessageValueType = -3,

        // Make sure success is always zero
        Success = 0,

        // "Standard" errors are positive

        RegistryReadError,
        RegistryWriteError,
    }
}
