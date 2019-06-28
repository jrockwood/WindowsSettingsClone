// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BridgeMessageDeserializerTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Tests.CommandBridge
{
    using System;
    using System.Collections.Generic;
    using Commands;
    using FluentAssertions;
    using NUnit.Framework;
    using WindowsSettingsClone.ServiceContracts.CommandBridge;

    public class BridgeMessageDeserializerTests
    {
        [Test]
        public void TryCreate_should_throw_on_null_params()
        {
            Action action = () => BridgeMessageDeserializer.TryCreate(null, out _, out _);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("valueSet");
        }

        [Test]
        public void TryCreate_should_correctly_deserialize()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            deserializer.Should().NotBeNull();
            errorResponse.Should().BeNull();

            deserializer.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
        }

        [Test]
        public void TryCreate_should_return_an_error_if_missing_a_command_name()
        {
            var valueSet = new Dictionary<string, object>();
            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            deserializer.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.CommandName.Should().Be(ServiceCommandName.Unknown);
            errorResponse.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            errorResponse.ErrorMessage.Should().Contain(ParamName.CommandName.ToString());
        }

        [Test]
        public void TryCreate_should_return_an_error_if_command_name_is_not_the_right_type()
        {
            var valueSet = new Dictionary<string, object> { [ParamName.CommandName.ToString()] = "NotRight" };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            deserializer.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.CommandName.Should().Be(ServiceCommandName.Unknown);
            errorResponse.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            errorResponse.ErrorMessage.Should().Contain(ParamName.CommandName.ToString());
        }

        [Test]
        public void LastError_should_be_null_on_a_new_instance()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.LastError.Should().BeNull();
        }

        [Test]
        public void HadError_should_be_false_initially()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.HadError.Should().BeFalse();
        }

        [Test]
        public void TryGetOptionalEnumValue_should_return_true_when_correctly_deserializing()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryHive.ToString()] = RegistryHive.CurrentUser
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.TryGetOptionalEnumValue(ParamName.RegistryHive, out RegistryHive hive).Should().BeTrue();

            hive.Should().Be(RegistryHive.CurrentUser);
            deserializer.LastError.Should().BeNull();
        }

        [Test]
        public void TryGetOptionalEnumValue_should_return_false_if_the_value_is_not_present()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.TryGetOptionalEnumValue(ParamName.RegistryHive, out RegistryHive hive).Should().BeFalse();

            hive.Should().Be(default(RegistryHive));
            deserializer.LastError.Should().BeNull();
        }

        [Test]
        public void TryGetOptionalEnumValue_should_return_false_if_the_value_is_not_valid()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryHive.ToString()] = "NotValid"
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.TryGetOptionalEnumValue(ParamName.RegistryHive, out RegistryHive hive).Should().BeFalse();

            hive.Should().Be(default(RegistryHive));

            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.RegistryHive.ToString());
        }

        [Test]
        public void GetEnumValue_should_return_the_deserialized_value()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryHive.ToString()] = RegistryHive.CurrentUser
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetEnumValue<RegistryHive>(ParamName.RegistryHive).Should().Be(RegistryHive.CurrentUser);
            deserializer.LastError.Should().BeNull();
        }

        [Test]
        public void GetEnumValue_should_set_LastError_if_the_parameter_is_not_present()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetEnumValue<RegistryHive>(ParamName.RegistryHive).Should().Be(default(RegistryHive));
            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.RegistryHive.ToString());
        }

        [Test]
        public void GetEnumValue_should_set_LastError_if_the_parameter_is_not_the_right_type()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryHive.ToString()] = "NotValid"
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetEnumValue<RegistryHive>(ParamName.RegistryHive).Should().Be(default(RegistryHive));

            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.RegistryHive.ToString());
        }

        [Test]
        public void GetIntValue_should_return_the_deserialized_value()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.ErrorCode.ToString()] = 12345
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetIntValue(ParamName.ErrorCode).Should().Be(12345);
            deserializer.LastError.Should().BeNull();
        }

        [Test]
        public void GetIntValue_should_set_LastError_if_the_parameter_is_not_present()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetIntValue(ParamName.ErrorCode).Should().Be(default(int));
            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.ErrorCode.ToString());
        }

        [Test]
        public void GetIntValue_should_set_LastError_if_the_parameter_is_not_the_right_type()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.ErrorCode.ToString()] = "NotValid"
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetIntValue(ParamName.ErrorCode).Should().Be(default(int));

            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.ErrorCode.ToString());
        }

        [Test]
        public void GetStringValue_should_return_the_deserialized_value()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryKey.ToString()] = "Key"
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetStringValue(ParamName.RegistryKey).Should().Be("Key");
            deserializer.LastError.Should().BeNull();
        }

        [Test]
        public void GetStringValue_should_set_LastError_if_the_parameter_is_not_present()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetStringValue(ParamName.RegistryKey).Should().BeNull();
            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.RegistryKey.ToString());
        }

        [Test]
        public void GetStringValue_should_set_LastError_if_the_parameter_is_not_the_right_type()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryKey.ToString()] = false
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetStringValue(ParamName.RegistryKey).Should().BeNull();

            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.RegistryKey.ToString());
        }

        [Test]
        public void GetValue_should_return_the_deserialized_value()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryKey.ToString()] = "Key"
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetValue(ParamName.RegistryKey).Should().Be("Key");
            deserializer.LastError.Should().BeNull();
        }

        [Test]
        public void GetValue_should_set_LastError_if_the_parameter_is_not_present()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
            };

            BridgeMessageDeserializer.TryCreate(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out ServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetValue(ParamName.RegistryKey).Should().BeNull();
            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.RegistryKey.ToString());
        }
    }
}
