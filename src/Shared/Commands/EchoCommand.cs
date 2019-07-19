// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EchoCommand.cs" company="Justin Rockwood">
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
    /// A command that echoes whatever it receives as the response. Useful for testing.
    /// </summary>
    public sealed class EchoCommand : ServiceCommand, IEchoCommand
    {
        public EchoCommand(string echoMessage)
            : base(ServiceCommandName.Echo)
        {
            EchoMessage = echoMessage;
        }

        internal EchoCommand(BridgeMessageDeserializer deserializer)
            : base(ServiceCommandName.Echo)
        {
            EchoMessage = deserializer.GetStringValue(ParamName.EchoMessage);
        }

        internal override void SerializeParams(IDictionary<ParamName, object> valueSet)
        {
            valueSet.Add(ParamName.EchoMessage, EchoMessage);
        }

        public string EchoMessage { get; }
    }
}
