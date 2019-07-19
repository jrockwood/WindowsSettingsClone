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
    using Shared.Logging;
    using SharedWin32.CommandExecutors.Registry;

    public class RegistryCommandExecutorTests
    {
        [Test]
        public void BaseKeyToHive_should_correctly_map_keys_to_Win32_hives()
        {
            RegistryCommandExecutor.BaseKeyToHive(RegistryBaseKey.ClassesRoot)
                .Should()
                .Be(RegistryHive.ClassesRoot);
            RegistryCommandExecutor.BaseKeyToHive(RegistryBaseKey.CurrentUser)
                .Should()
                .Be(RegistryHive.CurrentUser);
            RegistryCommandExecutor.BaseKeyToHive(RegistryBaseKey.LocalMachine)
                .Should()
                .Be(RegistryHive.LocalMachine);
            RegistryCommandExecutor.BaseKeyToHive(RegistryBaseKey.Users)
                .Should()
                .Be(RegistryHive.Users);
            RegistryCommandExecutor.BaseKeyToHive(RegistryBaseKey.PerformanceData)
                .Should()
                .Be(RegistryHive.PerformanceData);
            RegistryCommandExecutor.BaseKeyToHive(RegistryBaseKey.CurrentConfig)
                .Should()
                .Be(RegistryHive.CurrentConfig);
            RegistryCommandExecutor.BaseKeyToHive(RegistryBaseKey.DynData)
                .Should()
                .Be(RegistryHive.DynData);
        }

        [Test]
        public void HiveToWin32Name_should_correctly_map_hives_to_long_names()
        {
            RegistryCommandExecutor.HiveToWin32Name(RegistryHive.ClassesRoot).Should().Be("HKEY_CLASSES_ROOT");
            RegistryCommandExecutor.HiveToWin32Name(RegistryHive.CurrentUser).Should().Be("HKEY_CURRENT_USER");
            RegistryCommandExecutor.HiveToWin32Name(RegistryHive.LocalMachine).Should().Be("HKEY_LOCAL_MACHINE");
            RegistryCommandExecutor.HiveToWin32Name(RegistryHive.Users).Should().Be("HKEY_USERS");
            RegistryCommandExecutor.HiveToWin32Name(RegistryHive.CurrentConfig).Should().Be("HKEY_CURRENT_CONFIG");
            RegistryCommandExecutor.HiveToWin32Name(RegistryHive.PerformanceData).Should().Be("HKEY_PERFORMANCE_DATA");
            RegistryCommandExecutor.HiveToWin32Name(RegistryHive.DynData).Should().Be("HKEY_DYN_DATA");
        }

        [Test]
        public void Win32NameToHive_should_correctly_map_names_to_hives()
        {
            RegistryCommandExecutor.Win32NameToHive("HKEY_CLASSES_ROOT").Should().Be(RegistryHive.ClassesRoot);
            RegistryCommandExecutor.Win32NameToHive("HKCR").Should().Be(RegistryHive.ClassesRoot);
            RegistryCommandExecutor.Win32NameToHive("HKEY_CURRENT_USER").Should().Be(RegistryHive.CurrentUser);
            RegistryCommandExecutor.Win32NameToHive("HKCU").Should().Be(RegistryHive.CurrentUser);
            RegistryCommandExecutor.Win32NameToHive("HKEY_LOCAL_MACHINE").Should().Be(RegistryHive.LocalMachine);
            RegistryCommandExecutor.Win32NameToHive("HKLM").Should().Be(RegistryHive.LocalMachine);
            RegistryCommandExecutor.Win32NameToHive("HKEY_USERS").Should().Be(RegistryHive.Users);
            RegistryCommandExecutor.Win32NameToHive("HKU").Should().Be(RegistryHive.Users);
            RegistryCommandExecutor.Win32NameToHive("HKEY_CURRENT_CONFIG").Should().Be(RegistryHive.CurrentConfig);
            RegistryCommandExecutor.Win32NameToHive("HKEY_PERFORMANCE_DATA").Should().Be(RegistryHive.PerformanceData);
            RegistryCommandExecutor.Win32NameToHive("HKEY_DYN_DATA").Should().Be(RegistryHive.DynData);
        }

        //// ===========================================================================================================
        //// ExecuteRead
        //// ===========================================================================================================

        [Test]
        public void ExecuteRead_should_throw_on_null_params()
        {
            var executor = new RegistryCommandExecutor(new FakeRegistry());
            Action action = () => executor.ExecuteRead<int>(null, new NullLogger());
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("command");

            action = () => executor.ExecuteRead(
                new RegistryReadIntValueCommand(RegistryBaseKey.CurrentUser, "Key", "ValueName", 123),
                null);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("logger");
        }

        [Test]
        public void ExecuteRead_should_return_the_read_value()
        {
            var registry = new FakeRegistry(@"HKCU\SubKey\IntValue=123");
            var executor = new RegistryCommandExecutor(registry);
            IServiceCommandResponse response = executor.ExecuteRead(
                new RegistryReadIntValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "IntValue", 0),
                new NullLogger());

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
            IServiceCommandResponse response = executor.ExecuteRead(
                new RegistryReadIntValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "IntValue", 0),
                new NullLogger());

            response.IsError.Should().BeTrue();
            response.ErrorCode.Should().Be(ServiceCommandErrorCode.RegistryReadError);
            response.ErrorMessage.Should()
                .Be(
                    @"Error in reading registry value 'HKEY_CURRENT_USER\SubKey\IntValue': System.UnauthorizedAccessException: Test.");
        }

        //// ===========================================================================================================
        //// ExecuteWrite
        //// ===========================================================================================================

        [Test]
        public void ExecuteWrite_should_throw_on_null_params()
        {
            var executor = new RegistryCommandExecutor(new FakeRegistry());
            Action action = () => executor.ExecuteWrite<int>(null, new NullLogger());
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("command");

            action = () => executor.ExecuteWrite(
                new RegistryWriteIntValueCommand(RegistryBaseKey.CurrentUser, "Key", "ValueName", 123),
                null);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("logger");
        }

        [Test]
        public void ExecuteWrite_should_return_the_value()
        {
            var registry = new FakeRegistry(@"HKLM\SubKey");
            var executor = new RegistryCommandExecutor(registry);
            IServiceCommandResponse response = executor.ExecuteWrite(
                new RegistryWriteIntValueCommand(RegistryBaseKey.LocalMachine, "SubKey", "IntValue", 123),
                new NullLogger());

            response.IsSuccess.Should().BeTrue();
            response.Result.Should().Be(123);
        }

        [Test]
        public void ExecuteWrite_should_write_the_value()
        {
            var registry = new FakeRegistry(@"HKLM\SubKey");
            var executor = new RegistryCommandExecutor(registry);
            executor.ExecuteWrite(
                new RegistryWriteIntValueCommand(RegistryBaseKey.LocalMachine, "SubKey", "IntValue", 123),
                new NullLogger());

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
            executor.ExecuteWrite(
                new RegistryWriteIntValueCommand(RegistryBaseKey.LocalMachine, "SubKey", "IntValue", 123),
                new NullLogger());

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
            IServiceCommandResponse response = executor.ExecuteWrite(
                new RegistryWriteIntValueCommand(RegistryBaseKey.CurrentUser, "SubKey", "IntValue", 0),
                new NullLogger());

            response.IsError.Should().BeTrue();
            response.ErrorCode.Should().Be(ServiceCommandErrorCode.RegistryWriteError);
            response.ErrorMessage.Should()
                .Be(
                    @"Error in writing registry value 'HKEY_CURRENT_USER\SubKey\IntValue': System.UnauthorizedAccessException: Test.");
        }
    }
}
