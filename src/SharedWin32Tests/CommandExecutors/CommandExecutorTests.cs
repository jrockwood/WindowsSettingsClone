// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandExecutorTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests.CommandExecutors
{
    using Fakes;
    using FluentAssertions;
    using NUnit.Framework;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using Shared.CommandBridge;
    using Shared.Commands;
    using Shared.Logging;
    using SharedWin32.CommandExecutors;

    public class CommandExecutorTests
    {
        [Test]
        public void Execute_should_run_a_ShutdownServerCommand()
        {
            var executor = new CommandExecutor(new NullLogger(), new FakeRegistry());
            executor.Execute(new ShutdownServerCommand())
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.ShutdownServer, true));
        }

        [Test]
        public void Execute_should_run_an_EchoCommand()
        {
            var executor = new CommandExecutor(new NullLogger(), new FakeRegistry());
            executor.Execute(new EchoCommand("Test"))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.Echo, "Test"));
        }

        [Test]
        public void Execute_should_run_a_RegistryReadIntValueCommand()
        {
            var registry = new FakeRegistry(@"HKCU\SubKey\IntValue=123");
            var executor = new CommandExecutor(new NullLogger(), registry);
            executor.Execute(new RegistryReadIntValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "IntValue", -1))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.RegistryReadIntValue, 123));
        }

        [Test]
        public void Execute_should_run_a_RegistryReadStringValueCommand()
        {
            var registry = new FakeRegistry(@"HKCU\SubKey\StringValue=""Here""");
            var executor = new CommandExecutor(new NullLogger(), registry);
            executor.Execute(
                    new RegistryReadStringValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "StringValue", null))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.RegistryReadStringValue, "Here"));
        }

        [Test]
        public void Execute_should_run_a_RegistryWriteIntValueCommand()
        {
            var registry = new FakeRegistry();
            var executor = new CommandExecutor(new NullLogger(), registry);
            executor.Execute(new RegistryWriteIntValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "IntValue", 123))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.RegistryWriteIntValue, 123));
        }

        [Test]
        public void Execute_should_run_a_RegistryWriteStringValueCommand()
        {
            var registry = new FakeRegistry(@"HKCU\SubKey\StringValue=""Here""");
            var executor = new CommandExecutor(new NullLogger(), registry);
            executor.Execute(
                    new RegistryReadStringValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "StringValue", null))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.RegistryReadStringValue, "Here"));
        }
    }
}
