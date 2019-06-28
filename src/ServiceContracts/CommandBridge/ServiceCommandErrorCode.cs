// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCommandErrorCode.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.CommandBridge
{
    /// <summary>
    /// Enumerates the different kinds of errors a <see cref="ServiceCommand"/> can encounter.
    /// </summary>
    public enum ServiceCommandErrorCode
    {
        InternalError = -1,
        MissingRequiredMessageValue = -2,
        WrongMessageValueType = -3,
        Success = 0,
        RegistryValueNameNotFound,
    }
}
