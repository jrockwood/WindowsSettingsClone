// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemParametersInfoCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Commands
{
    using CommandBridge;

    /// <summary>
    /// Command that invokes the underlying Win32 <c>SystemParametersInfo</c> function that retrieves a value.
    /// </summary>
    public interface ISystemParametersInfoGetValueCommand : IServiceCommand
    {
        SystemParameterInfoKind SystemParameter { get; }
    }

    /// <summary>
    /// Command that invokes the underlying Win32 <c>SystemParametersInfo</c> function that sets a value.
    /// </summary>
    public interface ISystemParametersInfoSetValueCommand : IServiceCommand
    {
        SystemParameterInfoKind SystemParameter { get; }
        SystemParameterInfoUpdateKind UpdateKind { get; }
    }
}
