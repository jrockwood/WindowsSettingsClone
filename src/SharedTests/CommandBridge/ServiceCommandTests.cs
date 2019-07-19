// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCommandTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Tests.CommandBridge
{
    using System;
    using System.Collections.Generic;
    using Commands;
    using FluentAssertions;
    using NUnit.Framework;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using Shared.CommandBridge;

    public class ServiceCommandTests
    {
        //// ===========================================================================================================
        //// TryDeserializeFromValueSet Tests
        //// ===========================================================================================================

        [Test]
        public void TryDeserializeValueSet_should_throw_on_null_params()
        {
            Action action = () => ServiceCommand.TryDeserializeFromValueSet(null, out _, out _);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("valueSet");
        }

        [Test]
        public void TryDeserializeValueSet_should_correctly_deserialize_a_RegistryReadIntValueCommand()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue.ToString(),
                [ParamName.RegistryBaseKey.ToString()] = RegistryBaseKey.CurrentUser.ToString(),
                [ParamName.RegistryKey.ToString()] = "Key",
                [ParamName.RegistryValueName.ToString()] = "ValueName",
                [ParamName.RegistryDefaultValue.ToString()] = 123
            };

            ServiceCommand.TryDeserializeFromValueSet(
                    valueSet,
                    out IServiceCommand command,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            command.Should().BeOfType<RegistryReadIntValueCommand>();
            errorResponse.Should().BeNull();

            var typedCommand = (RegistryReadIntValueCommand)command;
            typedCommand.BaseKey.Should().Be(RegistryBaseKey.CurrentUser);
            typedCommand.Key.Should().Be("Key");
            typedCommand.ValueName.Should().Be("ValueName");
            typedCommand.DefaultValue.Should().Be(123);
        }

        [Test]
        public void TryDeserializeFromValueSet_should_return_an_error_if_missing_the_command_name()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.RegistryBaseKey.ToString()] = RegistryBaseKey.CurrentUser.ToString(),
                [ParamName.RegistryKey.ToString()] = "Key",
                [ParamName.RegistryValueName.ToString()] = "ValueName",
                [ParamName.RegistryDefaultValue.ToString()] = 123
            };

            ServiceCommand.TryDeserializeFromValueSet(
                    valueSet,
                    out IServiceCommand command,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            command.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.CommandName.Should().Be(ServiceCommandName.Unknown);
        }

        [Test]
        public void TryDeserializeValueSet_should_return_an_error_if_missing_a_required_parameter()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue.ToString(),
                [ParamName.RegistryKey.ToString()] = "Key",
                [ParamName.RegistryValueName.ToString()] = "ValueName",
                [ParamName.RegistryDefaultValue.ToString()] = 123
            };

            ServiceCommand.TryDeserializeFromValueSet(
                    valueSet,
                    out IServiceCommand command,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            command.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            errorResponse.ErrorMessage.Should().Contain(ParamName.RegistryBaseKey.ToString());
        }

        [Test]
        public void ToDebugString_should_return_the_correct_format()
        {
            var command = new TestServiceCommand(ServiceCommandName.RegistryReadIntValue);
            command.ToDebugString().Should().Be($"{ServiceCommandName.RegistryReadIntValue}: key=value");
        }

        private sealed class TestServiceCommand : ServiceCommand
        {
            public TestServiceCommand(ServiceCommandName commandName)
                : base(commandName)
            {
            }

            protected override void SerializeParams(IDictionary<string, object> valueSet)
            {
                valueSet.Add("key", "value");
            }
        }
    }
}
