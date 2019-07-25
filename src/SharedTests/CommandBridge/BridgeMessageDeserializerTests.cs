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
        //// TryCreateFromJsonString Tests
        //// ===========================================================================================================

        [Test]
        public void TryCreateFromJsonString_should_throw_on_null_params()
        {
            Action action = () => BridgeMessageDeserializer.TryCreateFromJsonString(null, out _, out _);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("jsonString");
        }

        [Test]
        public void TryCreateFromJsonString_should_correctly_deserialize()
        {
            string json = $@"
{{
    {ParamName.CommandName}: ""{ServiceCommandName.Echo}"",
    {ParamName.EchoMessage}: ""Test"",
}}";

            BridgeMessageDeserializer.TryCreateFromJsonString(
                    json,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            deserializer.Should().NotBeNull();
            errorResponse.Should().BeNull();

            deserializer.CommandName.Should().Be(ServiceCommandName.Echo);
            deserializer.GetStringValue(ParamName.EchoMessage).Should().Be("Test");
        }

        [Test]
        public void TryCreateFromJsonString_should_return_an_error_if_missing_a_command_name()
        {
            BridgeMessageDeserializer.TryCreateFromJsonString(
                    $"{{ Something: 10 }}",
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
        public void TryCreateFromJsonString_should_return_an_error_if_command_name_is_not_the_right_type()
        {
            BridgeMessageDeserializer.TryCreateFromJsonString(
                    $"{{ {ParamName.CommandName}: \"NotRight\" }}",
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

        [Test]
        public void TryCreateFromJsonString_should_return_an_error_if_the_json_is_invalid()
        {
            BridgeMessageDeserializer.TryCreateFromJsonString(
                    "invalidJson: 123",
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            deserializer.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.CommandName.Should().Be(ServiceCommandName.Unknown);
            errorResponse.ErrorCode.Should().Be(ServiceCommandErrorCode.InternalError);
            errorResponse.ErrorMessage.Should().StartWith("Internal error: Unexpected character");
        }

        [Test]
        public void TryCreateFromJsonString_should_return_an_error_if_the_json_contains_nested_objects()
        {
            BridgeMessageDeserializer.TryCreateFromJsonString(
                    "{ invalidJson: { nested: 123 } }",
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            deserializer.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.CommandName.Should().Be(ServiceCommandName.Unknown);
            errorResponse.ErrorCode.Should().Be(ServiceCommandErrorCode.InternalError);
            errorResponse.ErrorMessage.Should().Contain("Unable to cast object of type");
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
                [ParamName.BaseKey.ToString()] = RegistryBaseKey.CurrentUser
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.TryGetOptionalEnumValue(ParamName.BaseKey, out RegistryBaseKey hive).Should().BeTrue();

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

            deserializer.TryGetOptionalEnumValue(ParamName.BaseKey, out RegistryBaseKey hive).Should().BeFalse();

            hive.Should().Be(default(RegistryBaseKey));
            deserializer.LastError.Should().BeNull();
        }

        [Test]
        public void TryGetOptionalEnumValue_should_return_false_if_the_value_is_not_valid()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.BaseKey.ToString()] = "NotValid"
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.TryGetOptionalEnumValue(ParamName.BaseKey, out RegistryBaseKey hive).Should().BeFalse();

            hive.Should().Be(default(RegistryBaseKey));

            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.BaseKey.ToString());
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
                [ParamName.BaseKey.ToString()] = RegistryBaseKey.CurrentUser
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetEnumValue<RegistryBaseKey>(ParamName.BaseKey).Should().Be(RegistryBaseKey.CurrentUser);
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

            deserializer.GetEnumValue<RegistryBaseKey>(ParamName.BaseKey).Should().Be(default(RegistryBaseKey));
            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.BaseKey.ToString());
        }

        [Test]
        public void GetEnumValue_should_set_LastError_if_the_parameter_is_not_the_right_type()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.BaseKey.ToString()] = "NotValid"
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetEnumValue<RegistryBaseKey>(ParamName.BaseKey).Should().Be(default(RegistryBaseKey));

            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.BaseKey.ToString());
        }

        //// ===========================================================================================================
        //// GetIntValue Tests
        //// ===========================================================================================================

        [Test]
        public void GetIntValue_should_return_the_deserialized_value()
        {
            var inputExpectedOutputPairs = new (object Input, int ExpectedOutput)[]
            {
                (Input: 'a', ExpectedOutput: 'a'),
                (Input: (sbyte)1, ExpectedOutput: 1),
                (Input: (byte)2, ExpectedOutput: 2),
                (Input: (short)3, ExpectedOutput: 3),
                (Input: (ushort)4, ExpectedOutput: 4),
                (Input: 5, ExpectedOutput: 5),
                (Input: (uint)6, ExpectedOutput: 6),
                (Input: (long)7, ExpectedOutput: 7),
                (Input: (ulong)8, ExpectedOutput: 8),
            };

            foreach ((object input, int expectedOutput) in inputExpectedOutputPairs)
            {
                var valueSet = new Dictionary<string, object>
                {
                    [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                    [ParamName.ErrorCode.ToString()] = input
                };

                BridgeMessageDeserializer.TryCreateFromValueSet(
                        valueSet,
                        out BridgeMessageDeserializer deserializer,
                        out IServiceCommandResponse _)
                    .Should()
                    .BeTrue();

                deserializer.GetIntValue(ParamName.ErrorCode).Should().Be(expectedOutput);
                deserializer.LastError.Should().BeNull();
            }
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
            object[] inputs = { "123", decimal.One, (float)1, (double)2 };

            foreach (object input in inputs)
            {
                var valueSet = new Dictionary<string, object>
                {
                    [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                    [ParamName.ErrorCode.ToString()] = input
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
        }

        [Test]
        public void GetIntValue_should_set_LastError_if_the_parameter_is_out_of_range()
        {
            object[] inputs = { uint.MaxValue, ulong.MaxValue, long.MinValue, long.MaxValue };

            foreach (object input in inputs)
            {
                var valueSet = new Dictionary<string, object>
                {
                    [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                    [ParamName.ErrorCode.ToString()] = input
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
        }

        //// ===========================================================================================================
        //// GetBoolValue Tests
        //// ===========================================================================================================

        [Test]
        public void GetBoolValue_should_return_the_deserialized_value()
        {
            var inputExpectedOutputPairs = new (object Input, bool ExpectedOutput)[]
            {
                (Input: true, ExpectedOutput: true),
                (Input: false, ExpectedOutput: false),
            };

            foreach ((object input, bool expectedOutput) in inputExpectedOutputPairs)
            {
                var valueSet = new Dictionary<string, object>
                {
                    [ParamName.CommandName.ToString()] = ServiceCommandName.FileCopy,
                    [ParamName.Overwrite.ToString()] = input,
                };

                BridgeMessageDeserializer.TryCreateFromValueSet(
                        valueSet,
                        out BridgeMessageDeserializer deserializer,
                        out IServiceCommandResponse _)
                    .Should()
                    .BeTrue();

                deserializer.GetBoolValue(ParamName.Overwrite)
                    .Should()
                    .Be(expectedOutput, $"because {input} should have been converted to a boolean");
                deserializer.LastError.Should().BeNull();
            }
        }

        [Test]
        public void GetBoolValue_should_set_LastError_if_the_parameter_is_not_present()
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

            deserializer.GetBoolValue(ParamName.ErrorCode).Should().Be(default(bool));
            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.ErrorCode.ToString());
        }

        [Test]
        public void GetBoolValue_should_set_LastError_if_the_parameter_is_not_the_right_type()
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
                [ParamName.Key.ToString()] = "Key"
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetStringValue(ParamName.Key).Should().Be("Key");
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

            deserializer.GetStringValue(ParamName.Key).Should().BeNull();
            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.Key.ToString());
        }

        [Test]
        public void GetStringValue_should_set_LastError_if_the_parameter_is_not_the_right_type()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.Key.ToString()] = false
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetStringValue(ParamName.Key).Should().BeNull();

            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.WrongMessageValueType);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.Key.ToString());
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
                [ParamName.Key.ToString()] = "Key"
            };

            BridgeMessageDeserializer.TryCreateFromValueSet(
                    valueSet,
                    out BridgeMessageDeserializer deserializer,
                    out IServiceCommandResponse _)
                .Should()
                .BeTrue();

            deserializer.GetValue(ParamName.Key).Should().Be("Key");
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

            deserializer.GetValue(ParamName.Key).Should().BeNull();
            deserializer.LastError.Should().NotBeNull();
            deserializer.LastError.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            deserializer.LastError.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            deserializer.LastError.ErrorMessage.Should().Contain(ParamName.Key.ToString());
        }
    }
}
