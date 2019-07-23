// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FullTrustServiceBase.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.FullTrustServices
{
    using ServiceContracts.CommandBridge;
    using Shared.Diagnostics;

    /// <summary>
    /// Abstract base class for all full-trust service implementations that require sending a command across the bridge.
    /// </summary>
    internal abstract class FullTrustServiceBase
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        protected FullTrustServiceBase(ICommandBridgeClientService commandBridge)
        {
            CommandBridge = Param.VerifyNotNull(commandBridge, nameof(commandBridge));
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        protected ICommandBridgeClientService CommandBridge { get; }
    }
}
