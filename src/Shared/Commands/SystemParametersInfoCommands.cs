// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemParametersInfoCommand.cs" company="Justin Rockwood">
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
    /// Command that reads a system parameter using the Win32 API.
    /// </summary>
    public class SystemParametersInfoGetValueCommand : ServiceCommand, ISystemParametersInfoGetValueCommand
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public SystemParametersInfoGetValueCommand(SystemParameterInfoKind systemParameter)
            : base(ServiceCommandName.SystemParametersInfoGetValue)
        {
            SystemParameter = systemParameter;
        }

        internal SystemParametersInfoGetValueCommand(BridgeMessageDeserializer deserializer)
            : base(ServiceCommandName.SystemParametersInfoGetValue)
        {
            SystemParameter = deserializer.GetEnumValue<SystemParameterInfoKind>(ParamName.SystemParameter);
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public SystemParameterInfoKind SystemParameter { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        internal override void SerializeParams(IDictionary<ParamName, object> valueSet)
        {
            valueSet.Add(ParamName.SystemParameter, SystemParameter.ToString());
        }
    }

    /// <summary>
    /// Command that reads a system parameter using the Win32 API.
    /// </summary>
    public class SystemParametersInfoSetValueCommand : ServiceCommand, ISystemParametersInfoSetValueCommand
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public SystemParametersInfoSetValueCommand(SystemParameterInfoKind systemParameter, SystemParameterInfoUpdateKind updateKind)
            : base(ServiceCommandName.SystemParametersInfoGetValue)
        {
            SystemParameter = systemParameter;
            UpdateKind = updateKind;
        }

        internal SystemParametersInfoSetValueCommand(BridgeMessageDeserializer deserializer)
            : base(ServiceCommandName.SystemParametersInfoGetValue)
        {
            SystemParameter = deserializer.GetEnumValue<SystemParameterInfoKind>(ParamName.SystemParameter);
            UpdateKind = deserializer.GetEnumValue<SystemParameterInfoUpdateKind>(ParamName.UpdateKind);
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public SystemParameterInfoKind SystemParameter { get; }
        public SystemParameterInfoUpdateKind UpdateKind { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        internal override void SerializeParams(IDictionary<ParamName, object> valueSet)
        {
            valueSet.Add(ParamName.SystemParameter, SystemParameter.ToString());
            valueSet.Add(ParamName.UpdateKind, UpdateKind.ToString());
        }
    }
}
