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
    using Moq;
    using NUnit.Framework;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using Shared.CommandBridge;
    using Shared.Commands;
    using Shared.Logging;
    using SharedWin32.CommandExecutors;
    using SharedWin32.CommandExecutors.IO;
    using SharedWin32.CommandExecutors.Registry;
    using SharedWin32.CommandExecutors.SystemParametersInfo;

    public class CommandExecutorTests
    {
        private static CommandExecutor CreateExecutorWithFakes(
            IWin32Registry registry = null,
            IWin32SystemParametersInfo systemParametersInfo = null,
            IWin32FileSystem fileSystem = null)
        {
            return new CommandExecutor(
                new NullLogger(),
                registry ?? new FakeRegistry(),
                systemParametersInfo ?? new FakeSystemParametersInfo(),
                fileSystem ?? new FakeFileSystem());
        }

        [Test]
        public void Execute_should_run_a_ShutdownServerCommand()
        {
            CommandExecutor executor = CreateExecutorWithFakes();
            executor.Execute(new ShutdownServerCommand())
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.ShutdownServer, true));
        }

        [Test]
        public void Execute_should_run_an_EchoCommand()
        {
            CommandExecutor executor = CreateExecutorWithFakes();
            executor.Execute(new EchoCommand("Test"))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.Echo, "Test"));
        }

        [Test]
        public void Execute_should_run_a_RegistryReadIntValueCommand()
        {
            var registry = new FakeRegistry(@"HKCU\SubKey\IntValue=123");
            CommandExecutor executor = CreateExecutorWithFakes(registry: registry);
            executor.Execute(new RegistryReadIntValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "IntValue", -1))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.RegistryReadIntValue, 123));
        }

        [Test]
        public void Execute_should_run_a_RegistryReadStringValueCommand()
        {
            var registry = new FakeRegistry(@"HKCU\SubKey\StringValue=""Here""");
            CommandExecutor executor = CreateExecutorWithFakes(registry: registry);
            executor.Execute(
                    new RegistryReadStringValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "StringValue", null))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.RegistryReadStringValue, "Here"));
        }

        [Test]
        public void Execute_should_run_a_RegistryWriteIntValueCommand()
        {
            var registry = new FakeRegistry();
            CommandExecutor executor = CreateExecutorWithFakes(registry: registry);
            executor.Execute(new RegistryWriteIntValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "IntValue", 123))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.RegistryWriteIntValue, 123));
        }

        [Test]
        public void Execute_should_run_a_RegistryWriteStringValueCommand()
        {
            var registry = new FakeRegistry(@"HKCU\SubKey\StringValue=""Here""");
            CommandExecutor executor = CreateExecutorWithFakes(registry: registry);
            executor.Execute(
                    new RegistryReadStringValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "StringValue", null))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.RegistryReadStringValue, "Here"));
        }

        [Test]
        public void Execute_should_run_a_SystemParametersInfoGetValueCommand()
        {
            var fs = new FakeFileSystem(@"C:\source.txt=Contents");
            CommandExecutor executor = CreateExecutorWithFakes(fileSystem: fs);
            executor.Execute(new FileCopyCommand(@"C:\source.txt", @"C:\dest.txt", overwrite: false))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.FileCopy, true));

            fs.ReadAllText(@"C:\dest.txt").Should().Be("Contents");
        }

        [Test]
        public void Execute_should_run_a_FileCopyCommand()
        {
            var fs = new Mock<IWin32FileSystem>();
            CommandExecutor executor = CreateExecutorWithFakes(fileSystem: fs.Object);
            executor.Execute(new FileCopyCommand("source", "dest", false));
            fs.Verify(x => x.CopyFile("source", "dest", false), Times.Once);
        }
    }
}
