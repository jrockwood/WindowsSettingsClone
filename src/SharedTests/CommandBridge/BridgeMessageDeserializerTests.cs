// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BridgeMessageDeserializerTests.cs" company="Justin Rockwood">
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

    public class BridgeMessageDeserializerTests
    {
        //// ===========================================================================================================
        //// TryCreateFromValueSet Tests
        //// ===========================================================================================================

        [Test]
        public void TryCreateFromValueSet_should_throw_on_null_params()
        {
            Action action = () => BridgeMessageDeserializer.TryCreateFromValueSet(null, out _, out _);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("valueSet");
        }

        [Test]
        public void TryCreateFromValueSet_should_correctly_deserialize()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            deserializer.Should().NotBeNull();
            errorResponse.Should().BeNull();

            deserializer.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
        }

        [Test]
        public void TryCreateFromValueSet_should_return_an_error_if_missing_a_command_name()
        {
            var valueSet = new Dictionary<string, object>();
            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            deserializer.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.CommandName.Should().Be(ServiceCommandName.Unknown);
            errorResponse.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            errorResponse.ErrorMessage.Should().Contain(ParamName.CommandName.ToString());
        }

        [Test]
        public void TryCreateFromValueSet_should_return_an_error_if_command_name_is_not_the_right_type()
        {
            var valueSet = new Dictionary<string, object> { [ParamName.CommandName.ToString()] = "NotRight" };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            deserializer.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.CommandName.Should().Be(ServiceCommandName.Unknown);
            errorResponse.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            errorResponse.ErrorMessage.Should().Contain(ParamName.CommandName.ToString());
        }

        //// ===========================================================================================================
        //// LastError Tests
        //// ===========================================================================================================

        [Test]
        public void LastError_should_be_null_on_a_new_instance()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.LastError.Should().BeNull();
        }

        //// ===========================================================================================================
        //// HadError Tests
        //// ===========================================================================================================

        [Test]
        public void HadError_should_be_false_initially()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.HadError.Should().BeFalse();
        }

        //// ===========================================================================================================
        //// TryGetOptionalEnumValue Tests
        //// ===========================================================================================================

        [Test]
        public void TryGetOptionalEnumValue_should_return_true_when_correctly_deserializing()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryBaseKey.ToString()] = RegistryBaseKey.CurrentUser
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.TryGetOptionalEnumValue(ParamName.RegistryBaseKey, out RegistryBaseKey hive).Should().BeTrue();

            hive.Should().Be(RegistryBaseKey.CurrentUser);
            deserializer.LastError.Should().BeNull();
        }

        [Test]
        public void TryGetOptionalEnumValue_should_return_false_if_the_value_is_not_present()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.TryGetOptionalEnumValue(ParamName.RegistryBaseKey, out RegistryBaseKey hive).Should().BeFalse();

            hive.Should().Be(default(RegistryBaseKey));
            deserializer.LastError.Should().BeNull();
        }

        [Test]
        public void TryGetOptionalEnumValue_should_return_false_if_the_value_is_not_valid()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryBaseKey.ToString()] = "NotValid"
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.TryGetOptionalEnumValue(ParamName.RegistryBaseKey, out RegistryBaseKey hive).Should().BeFalse();

            hive.Should().Be(default(RegistryBaseKey));

            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.RegistryBaseKey.ToString());
        }

        //// ===========================================================================================================
        //// GetEnumValue Tests
        //// ===========================================================================================================

        [Test]
        public void GetEnumValue_should_return_the_deserialized_value()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryBaseKey.ToString()] = RegistryBaseKey.CurrentUser
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetEnumValue<RegistryBaseKey>(ParamName.RegistryBaseKey).Should().Be(RegistryBaseKey.CurrentUser);
            deserializer.LastError.Should().BeNull();
        }

        [Test]
        public void GetEnumValue_should_set_LastError_if_the_parameter_is_not_present()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetEnumValue<RegistryBaseKey>(ParamName.RegistryBaseKey).Should().Be(default(RegistryBaseKey));
            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.RegistryBaseKey.ToString());
        }

        [Test]
        public void GetEnumValue_should_set_LastError_if_the_parameter_is_not_the_right_type()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryBaseKey.ToString()] = "NotValid"
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetEnumValue<RegistryBaseKey>(ParamName.RegistryBaseKey).Should().Be(default(RegistryBaseKey));

            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.RegistryBaseKey.ToString());
        }

        //// ===========================================================================================================
        //// GetIntValue Tests
        //// ===========================================================================================================

        [Test]
        public void GetIntValue_should_return_the_deserialized_value()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.ErrorCode.ToString()] = 12345
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
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

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
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

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetIntValue(ParamName.ErrorCode).Should().Be(default(int));

            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.ErrorCode.ToString());
        }

        //// ===========================================================================================================
        //// GetStringValue Tests
        //// ===========================================================================================================

        [Test]
        public void GetStringValue_should_return_the_deserialized_value()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryKey.ToString()] = "Key"
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
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

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
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

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetStringValue(ParamName.RegistryKey).Should().BeNull();

            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.RegistryKey.ToString());
        }

        //// ===========================================================================================================
        //// GetValue Tests
        //// ===========================================================================================================

        [Test]
        public void GetValue_should_return_the_deserialized_value()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.RegistryKey.ToString()] = "Key"
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
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

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
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
