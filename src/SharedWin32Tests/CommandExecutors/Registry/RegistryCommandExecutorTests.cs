// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryCommandExecutorTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests.CommandExecutors.Registry
{
    using System;
    using Fakes;
    using FluentAssertions;
    using Microsoft.Win32;
    using Moq;
    using NUnit.Framework;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using Shared.Commands;
    using SharedWin32.CommandExecutors.Registry;

    public class RegistryCommandExecutorTests
    {
        //// ===========================================================================================================
        //// Execute (Read) Tests
        //// ===========================================================================================================

        [Test]
        public void ExecuteRead_should_throw_on_null_params()
        {
            var executor = new RegistryCommandExecutor(new FakeRegistry());
            Action action = () => executor.Execute(null);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("command");
        }

        [Test]
        public void ExecuteRead_should_return_the_read_value()
        {
            var registry = new FakeRegistry(@"HKCU\SubKey\IntValue=123");
            var executor = new RegistryCommandExecutor(registry);
            IServiceCommandResponse response = executor.Execute(
                new RegistryReadIntValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "IntValue", 0));

            response.IsSuccess.Should().BeTrue();
            response.Result.Should().Be(123);
        }

        [Test]
        public void ExecuteRead_should_return_an_error_if_the_sub_key_is_not_present()
        {
            var mockKey = new Mock<IWin32RegistryKey>(MockBehavior.Strict);
            mockKey.Setup(key => key.OpenSubKey("SubKey", false)).Throws(new UnauthorizedAccessException("Test"));
            mockKey.Setup(key => key.Dispose());

            var mockRegistry = new Mock<IWin32Registry>(MockBehavior.Strict);
            mockRegistry.Setup(reg => reg.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                .Returns(mockKey.Object);

            var executor = new RegistryCommandExecutor(mockRegistry.Object);
            IServiceCommandResponse response = executor.Execute(
                new RegistryReadIntValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "IntValue", 0));

            response.IsError.Should().BeTrue();
            response.ErrorCode.Should().Be(ServiceCommandErrorCode.RegistryReadError);
            response.ErrorMessage.Should()
                .Be(
                    @"Error in reading registry value 'HKEY_CURRENT_USER\SubKey\IntValue': System.UnauthorizedAccessException: Test.");
        }

        //// ===========================================================================================================
        //// Execute (Write) Tests
        //// ===========================================================================================================

        [Test]
        public void ExecuteWrite_should_throw_on_null_params()
        {
            var executor = new RegistryCommandExecutor(new FakeRegistry());
            Action action = () => executor.Execute(null);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("command");

            action = () => executor.Execute(null);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("command");
        }

        [Test]
        public void ExecuteWrite_should_return_the_value()
        {
            var registry = new FakeRegistry(@"HKLM\SubKey");
            var executor = new RegistryCommandExecutor(registry);
            IServiceCommandResponse response = executor.Execute(
                new RegistryWriteIntValueCommand(RegistryBaseKey.LocalMachine, "SubKey", "IntValue", 123));

            response.IsSuccess.Should().BeTrue();
            response.Result.Should().Be(123);
        }

        [Test]
        public void ExecuteWrite_should_write_the_value()
        {
            var registry = new FakeRegistry(@"HKLM\SubKey");
            var executor = new RegistryCommandExecutor(registry);
            executor.Execute(
                new RegistryWriteIntValueCommand(RegistryBaseKey.LocalMachine, "SubKey", "IntValue", 123));

            registry.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
                .OpenSubKey("SubKey")
                .GetValue("IntValue")
                .Should()
                .Be(123);
        }

        [Test]
        public void ExecuteWrite_should_create_the_key_if_not_present()
        {
            var registry = new FakeRegistry(@"HKLM");
            var executor = new RegistryCommandExecutor(registry);
            executor.Execute(
                new RegistryWriteIntValueCommand(RegistryBaseKey.LocalMachine, "SubKey", "IntValue", 123));

            registry.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
                .OpenSubKey("SubKey")
                .GetValue("IntValue")
                .Should()
                .Be(123);
        }

        [Test]
        public void ExecuteWrite_should_return_an_error_when_an_exception_is_thrown()
        {
            var mockKey = new Mock<IWin32RegistryKey>(MockBehavior.Strict);
            mockKey.Setup(key => key.CreateSubKey("SubKey", true)).Throws(new UnauthorizedAccessException("Test"));
            mockKey.Setup(key => key.Dispose());

            var mockRegistry = new Mock<IWin32Registry>(MockBehavior.Strict);
            mockRegistry.Setup(reg => reg.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                .Returns(mockKey.Object);

            var executor = new RegistryCommandExecutor(mockRegistry.Object);
            IServiceCommandResponse response = executor.Execute(
                new RegistryWriteIntValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "IntValue", 0));

            response.IsError.Should().BeTrue();
            response.ErrorCode.Should().Be(ServiceCommandErrorCode.RegistryWriteError);
            response.ErrorMessage.Should()
                .Be(
                    @"Error in writing registry value 'HKEY_CURRENT_USER\SubKey\IntValue': System.UnauthorizedAccessException: Test.");
        }
    }
}
