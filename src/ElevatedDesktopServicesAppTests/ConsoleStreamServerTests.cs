// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleStreamServerTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ElevatedDesktopServicesAppTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ElevatedDesktopServicesApp;
    using FluentAssertions;
    using NUnit.Framework;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using Shared.Commands;
    using Shared.Logging;
    using SharedWin32Tests.Fakes;

    public class ConsoleStreamServerTests
    {
        [Test]
        public void Ctor_should_throw_on_null_params()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => new ConsoleStreamServer(null);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("logger");
        }

        //// ===========================================================================================================
        //// Start Tests
        //// ===========================================================================================================

        private sealed class FakeClient
        {
            private readonly Queue<IServiceCommand> _commands;
            private readonly List<string> _responses = new List<string>();

            public FakeClient(params IServiceCommand[] commands)
            {
                _commands = new Queue<IServiceCommand>(commands);
            }

            public string LastResponse => _responses.Last();
            public IReadOnlyList<string> Responses => _responses.AsReadOnly();

            public string SendCommand()
            {
                IServiceCommand command = _commands.Count > 0 ? _commands.Dequeue() : new ShutdownServerCommand();
                string serializedCommand = command.SerializeToJsonString();
                return serializedCommand;
            }

            public void ReceiveResponse(string response)
            {
                _responses.Add(response);
            }
        }

        [Test]
        public void Start_should_throw_on_null_params()
        {
            var server = new ConsoleStreamServer(new NullLogger());
            Action action = () => server.Start(null, s => { }, new FakeRegistry());
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("readLineFunc");

            action = () => server.Start(() => "", null, new FakeRegistry());
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("writeLineAction");

            action = () => server.Start(() => "", s => { }, null);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("registry");
        }

        [Test]
        public void Start_should_accept_a_shutdown_request()
        {
            var client = new FakeClient();
            var server = new ConsoleStreamServer(new NullLogger());
            server.Start(client.SendCommand, client.ReceiveResponse, new FakeRegistry());

            client.LastResponse.Should()
                .Be("{\"CommandName\":\"ShutdownServer\",\"CommandResult\":true,\"ErrorCode\":\"Success\"}");
        }

        [Test]
        public void Start_should_accept_echo_request()
        {
            var client = new FakeClient(new EchoCommand("Hello!"));
            var server = new ConsoleStreamServer(new NullLogger());
            server.Start(client.SendCommand, client.ReceiveResponse, new FakeRegistry());

            client.Responses.First().Should()
                .Be("{\"CommandName\":\"Echo\",\"CommandResult\":\"Hello!\",\"ErrorCode\":\"Success\"}");
        }

        [Test]
        public void Start_should_accept_a_registry_read_request()
        {
            var client = new FakeClient(
                new RegistryReadIntValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "IntValue", -1));
            var server = new ConsoleStreamServer(new NullLogger());
            server.Start(client.SendCommand, client.ReceiveResponse, new FakeRegistry(@"HKCU\SubKey\IntValue=123"));

            client.Responses.First().Should()
                .Be("{\"CommandName\":\"RegistryReadIntValue\",\"CommandResult\":123,\"ErrorCode\":\"Success\"}");
        }
    }
}
