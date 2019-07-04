﻿// ---------------------------------------------------------------------------------------------------------------------
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
        [Test]
        public void TryDeserialize_should_throw_on_null_params()
        {
            Action action = () => ServiceCommand.TryDeserialize(null, out _, out _);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("valueSet");
        }

        [Test]
        public void TryDeserialize_should_correctly_deserialize_a_RegistryReadIntValueCommand()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue.ToString(),
                [ParamName.RegistryHive.ToString()] = RegistryHive.CurrentUser.ToString(),
                [ParamName.RegistryKey.ToString()] = "Key",
                [ParamName.RegistryValueName.ToString()] = "ValueName",
                [ParamName.RegistryDefaultValue.ToString()] = 123
            };

            ServiceCommand.TryDeserialize(
                    valueSet,
                    out ServiceCommand command,
                    out ServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            command.Should().BeOfType<RegistryReadIntValueCommand>();
            errorResponse.Should().BeNull();

            var typedCommand = (RegistryReadIntValueCommand)command;
            typedCommand.Hive.Should().Be(RegistryHive.CurrentUser);
            typedCommand.Key.Should().Be("Key");
            typedCommand.ValueName.Should().Be("ValueName");
            typedCommand.DefaultValue.Should().Be(123);
        }

        [Test]
        public void TryDeserialize_should_return_an_error_if_missing_the_command_name()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.RegistryHive.ToString()] = RegistryHive.CurrentUser.ToString(),
                [ParamName.RegistryKey.ToString()] = "Key",
                [ParamName.RegistryValueName.ToString()] = "ValueName",
                [ParamName.RegistryDefaultValue.ToString()] = 123
            };

            ServiceCommand.TryDeserialize(
                    valueSet,
                    out ServiceCommand command,
                    out ServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            command.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.CommandName.Should().Be(ServiceCommandName.Unknown);
        }

        [Test]
        public void TryDeserialize_should_return_an_error_if_missing_a_required_parameter()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue.ToString(),
                [ParamName.RegistryKey.ToString()] = "Key",
                [ParamName.RegistryValueName.ToString()] = "ValueName",
                [ParamName.RegistryDefaultValue.ToString()] = 123
            };

            ServiceCommand.TryDeserialize(
                    valueSet,
                    out ServiceCommand command,
                    out ServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            command.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            errorResponse.ErrorMessage.Should().Contain(ParamName.RegistryHive.ToString());
        }
    }
}