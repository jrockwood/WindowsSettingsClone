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
    using FluentAssertions;
    using NUnit.Framework;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using Shared.CommandBridge;
    using Shared.Commands;

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
                [ParamName.BaseKey.ToString()] = RegistryBaseKey.CurrentUser.ToString(),
                [ParamName.Key.ToString()] = "Key",
                [ParamName.ValueName.ToString()] = "ValueName",
                [ParamName.DefaultValue.ToString()] = 123
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
        public void TryDeserializeValueSet_should_correctly_deserialize_a_RegistryReadStringValueCommand()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadStringValue.ToString(),
                [ParamName.BaseKey.ToString()] = RegistryBaseKey.CurrentUser.ToString(),
                [ParamName.Key.ToString()] = "Key",
                [ParamName.ValueName.ToString()] = "ValueName",
                [ParamName.DefaultValue.ToString()] = "Value"
            };

            ServiceCommand.TryDeserializeFromValueSet(
                    valueSet,
                    out IServiceCommand command,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            command.Should().BeOfType<RegistryReadStringValueCommand>();
            errorResponse.Should().BeNull();

            var typedCommand = (RegistryReadStringValueCommand)command;
            typedCommand.BaseKey.Should().Be(RegistryBaseKey.CurrentUser);
            typedCommand.Key.Should().Be("Key");
            typedCommand.ValueName.Should().Be("ValueName");
            typedCommand.DefaultValue.Should().Be("Value");
        }

        [Test]
        public void TryDeserializeValueSet_should_correctly_deserialize_a_RegistryWriteIntValueCommand()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryWriteIntValue.ToString(),
                [ParamName.BaseKey.ToString()] = RegistryBaseKey.CurrentUser.ToString(),
                [ParamName.Key.ToString()] = "Key",
                [ParamName.ValueName.ToString()] = "ValueName",
                [ParamName.Value.ToString()] = 123
            };

            ServiceCommand.TryDeserializeFromValueSet(
                    valueSet,
                    out IServiceCommand command,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            command.Should().BeOfType<RegistryWriteIntValueCommand>();
            errorResponse.Should().BeNull();

            var typedCommand = (RegistryWriteIntValueCommand)command;
            typedCommand.BaseKey.Should().Be(RegistryBaseKey.CurrentUser);
            typedCommand.Key.Should().Be("Key");
            typedCommand.ValueName.Should().Be("ValueName");
            typedCommand.Value.Should().Be(123);
        }

        [Test]
        public void TryDeserializeValueSet_should_correctly_deserialize_a_RegistryWriteStringValueCommand()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryWriteStringValue.ToString(),
                [ParamName.BaseKey.ToString()] = RegistryBaseKey.CurrentUser.ToString(),
                [ParamName.Key.ToString()] = "Key",
                [ParamName.ValueName.ToString()] = "ValueName",
                [ParamName.Value.ToString()] = "Value"
            };

            ServiceCommand.TryDeserializeFromValueSet(
                    valueSet,
                    out IServiceCommand command,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            command.Should().BeOfType<RegistryWriteStringValueCommand>();
            errorResponse.Should().BeNull();

            var typedCommand = (RegistryWriteStringValueCommand)command;
            typedCommand.BaseKey.Should().Be(RegistryBaseKey.CurrentUser);
            typedCommand.Key.Should().Be("Key");
            typedCommand.ValueName.Should().Be("ValueName");
            typedCommand.Value.Should().Be("Value");
        }

        [Test]
        public void TryDeserializeFromValueSet_should_return_an_error_if_missing_the_command_name()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.BaseKey.ToString()] = RegistryBaseKey.CurrentUser.ToString(),
                [ParamName.Key.ToString()] = "Key",
                [ParamName.ValueName.ToString()] = "ValueName",
                [ParamName.DefaultValue.ToString()] = 123
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
                [ParamName.Key.ToString()] = "Key",
                [ParamName.ValueName.ToString()] = "ValueName",
                [ParamName.DefaultValue.ToString()] = 123
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
            errorResponse.ErrorMessage.Should().Contain(ParamName.BaseKey.ToString());
        }

        //// ===========================================================================================================
        //// TryDeserializeFromJsonString Tests
        //// ===========================================================================================================

        [Test]
        public void TryDeserializeFromJsonString_should_throw_on_null_params()
        {
            Action action = () => ServiceCommand.TryDeserializeFromJsonString(null, out _, out _);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("jsonString");
        }

        [Test]
        public void TryDeserializeFromJsonString_should_return_an_error_on_invalid_JSON()
        {
            ServiceCommand.TryDeserializeFromJsonString(
                    "invalid",
                    out IServiceCommand command,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            command.Should().BeNull();
            errorResponse.Should().NotBeNull();
        }

        [Test]
        public void TryDeserializeFromJsonString_should_correctly_deserialize_a_RegistryReadIntValueCommand()
        {
            string jsonString = $@"
{{
    {ParamName.CommandName}: ""{ServiceCommandName.RegistryReadIntValue}"",
    {ParamName.BaseKey}: ""{RegistryBaseKey.CurrentUser}"",
    {ParamName.Key}: ""Key"",
    {ParamName.ValueName}: ""ValueName"",
    {ParamName.DefaultValue}: 123,
}}";

            ServiceCommand.TryDeserializeFromJsonString(
                    jsonString,
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
        public void TryDeserializeFromJsonString_should_correctly_deserialize_a_RegistryReadStringValueCommand()
        {
            string jsonString = $@"
{{
    {ParamName.CommandName}: ""{ServiceCommandName.RegistryReadStringValue}"",
    {ParamName.BaseKey}: ""{RegistryBaseKey.CurrentUser}"",
    {ParamName.Key}: ""Key"",
    {ParamName.ValueName}: ""ValueName"",
    {ParamName.DefaultValue}: ""Value"",
}}";

            ServiceCommand.TryDeserializeFromJsonString(
                    jsonString,
                    out IServiceCommand command,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            command.Should().BeOfType<RegistryReadStringValueCommand>();
            errorResponse.Should().BeNull();

            var typedCommand = (RegistryReadStringValueCommand)command;
            typedCommand.BaseKey.Should().Be(RegistryBaseKey.CurrentUser);
            typedCommand.Key.Should().Be("Key");
            typedCommand.ValueName.Should().Be("ValueName");
            typedCommand.DefaultValue.Should().Be("Value");
        }

        //// ===========================================================================================================
        //// SerializeToJsonString Tests
        //// ===========================================================================================================

        [Test]
        public void SerializeToJsonString_should_serialize_into_compact_JSON()
        {
            var command = new RegistryReadIntValueCommand(RegistryBaseKey.CurrentUser, "SubPath", "IntValue", 123);
            string expectedJson = $@"
{{
    ""{ParamName.CommandName}"": ""{ServiceCommandName.RegistryReadIntValue}"",
    ""{ParamName.BaseKey}"": ""{RegistryBaseKey.CurrentUser}"",
    ""{ParamName.Key}"": ""SubPath"",
    ""{ParamName.ValueName}"": ""IntValue"",
    ""{ParamName.DefaultValue}"": 123
}}
";
            expectedJson = expectedJson.Replace("\r", "")
                .Replace("\n", "")
                .Replace(" ", "");

            command.SerializeToJsonString().Should().Be(expectedJson);
        }

        //// ===========================================================================================================
        //// ToDebugString Tests
        //// ===========================================================================================================

        [Test]
        public void ToDebugString_should_return_the_correct_format()
        {
            var command = new TestServiceCommand(ServiceCommandName.RegistryReadIntValue);
            command.ToDebugString().Should().Be($"{ServiceCommandName.RegistryReadIntValue}: Key=value");
        }

        private sealed class TestServiceCommand : ServiceCommand
        {
            public TestServiceCommand(ServiceCommandName commandName)
                : base(commandName)
            {
            }

            internal override void SerializeParams(IDictionary<ParamName, object> valueSet)
            {
                valueSet.Add(ParamName.Key, "value");
            }
        }
    }
}
