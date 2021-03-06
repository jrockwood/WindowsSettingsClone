﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCommandResponseTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Tests.CommandBridge
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;
    using ServiceContracts.CommandBridge;
    using Shared.CommandBridge;

    public class ServiceCommandResponseTests
    {
        //// ===========================================================================================================
        //// Create Tests
        //// ===========================================================================================================

        [Test]
        public void Create_should_store_the_properties()
        {
            var response = ServiceCommandResponse.Create(ServiceCommandName.RegistryReadIntValue, 10);
            response.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            response.Result.Should().Be(10);
            response.ErrorCode.Should().Be(ServiceCommandErrorCode.Success);
            response.ErrorMessage.Should().BeNull();
        }

        //// ===========================================================================================================
        //// CreateError Tests
        //// ===========================================================================================================

        [Test]
        public void CreateError_should_store_the_properties()
        {
            var response = ServiceCommandResponse.CreateError(
                ServiceCommandName.RegistryReadIntValue,
                ServiceErrorInfo.InternalError("message"));
            response.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            response.Result.Should().BeNull();
            response.ErrorCode.Should().Be(ServiceCommandErrorCode.InternalError);
            response.ErrorMessage.Should().NotBeNull();
        }

        [Test]
        public void CreateError_from_exception_should_store_the_properties()
        {
            var response = ServiceCommandResponse.CreateError(
                ServiceCommandName.RegistryReadIntValue,
                new InvalidOperationException());
            response.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            response.Result.Should().BeNull();
            response.ErrorCode.Should().Be(ServiceCommandErrorCode.InternalError);
            response.ErrorMessage.Should().NotBeNull();
        }

        //// ===========================================================================================================
        //// TryDeserializeFromValueSet Tests
        //// ===========================================================================================================

        [Test]
        public void TryDeserializeFromValueSet_should_deserialize_a_successful_response()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.CommandResult.ToString()] = 123,
                [ParamName.ErrorCode.ToString()] = ServiceCommandErrorCode.Success,
            };

            ServiceCommandResponse.TryDeserializeFromValueSet(
                    valueSet,
                    out IServiceCommandResponse response,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            response.Should().NotBeNull();
            errorResponse.Should().BeNull();

            response.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            response.Result.Should().Be(123);
            response.ErrorCode.Should().Be(ServiceCommandErrorCode.Success);
            response.ErrorMessage.Should().BeNull();

            response.IsError.Should().BeFalse();
            response.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void TryDeserializeFromValueSet_should_deserialize_an_error()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandName.ToString()] = ServiceCommandName.RegistryReadIntValue,
                [ParamName.CommandResult.ToString()] = null,
                [ParamName.ErrorCode.ToString()] = ServiceCommandErrorCode.InternalError,
                [ParamName.ErrorMessage.ToString()] = "Error",
            };

            ServiceCommandResponse.TryDeserializeFromValueSet(
                    valueSet,
                    out IServiceCommandResponse response,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            response.Should().NotBeNull();
            errorResponse.Should().BeNull();

            response.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            response.Result.Should().BeNull();
            response.ErrorCode.Should().Be(ServiceCommandErrorCode.InternalError);
            response.ErrorMessage.Should().Be("Error");

            response.IsError.Should().BeTrue();
            response.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void TryDeserializeFromValueSet_should_fail_if_missing_a_command_name()
        {
            var valueSet = new Dictionary<string, object>
            {
                [ParamName.CommandResult.ToString()] = 123,
                [ParamName.ErrorCode.ToString()] = ServiceCommandErrorCode.Success,
            };

            ServiceCommandResponse.TryDeserializeFromValueSet(
                    valueSet,
                    out IServiceCommandResponse response,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            response.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.CommandName.Should().Be(ServiceCommandName.Unknown);
            errorResponse.Result.Should().BeNull();
            errorResponse.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            errorResponse.ErrorMessage.Should().Contain(ParamName.CommandName.ToString());
        }

        //// ===========================================================================================================
        //// TryDeserializeFromValueSet Tests
        //// ===========================================================================================================

        [Test]
        public void TryDeserializeFromJsonString_should_deserialize_a_successful_response()
        {
            string json = $@"
{{
    {ParamName.CommandName}: ""{ServiceCommandName.RegistryReadIntValue}"",
    {ParamName.CommandResult}: 123,
    {ParamName.ErrorCode}: ""{ServiceCommandErrorCode.Success}"",
}}";

            ServiceCommandResponse.TryDeserializeFromJsonString(
                    json,
                    out IServiceCommandResponse response,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeTrue("because there are no errors in deserialization: {0}", errorResponse?.ToDebugString());

            response.Should().NotBeNull();
            errorResponse.Should().BeNull();

            response.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            response.Result.Should().Be(123);
            response.ErrorCode.Should().Be(ServiceCommandErrorCode.Success);
            response.ErrorMessage.Should().BeNull();

            response.IsError.Should().BeFalse();
            response.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void TryDeserializeFromJsonString_should_deserialize_an_error()
        {
            string json = $@"
{{
    {ParamName.CommandName}: ""{ServiceCommandName.RegistryReadIntValue}"",
    {ParamName.CommandResult}: null,
    {ParamName.ErrorCode}: ""{ServiceCommandErrorCode.InternalError}"",
    {ParamName.ErrorMessage}: ""Error"",
}}";

            ServiceCommandResponse.TryDeserializeFromJsonString(
                    json,
                    out IServiceCommandResponse response,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeTrue();

            response.Should().NotBeNull();
            errorResponse.Should().BeNull();

            response.CommandName.Should().Be(ServiceCommandName.RegistryReadIntValue);
            response.Result.Should().BeNull();
            response.ErrorCode.Should().Be(ServiceCommandErrorCode.InternalError);
            response.ErrorMessage.Should().Be("Error");

            response.IsError.Should().BeTrue();
            response.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void TryDeserializeFromJsonString_should_fail_if_missing_a_command_name()
        {
            string json = $@"
{{
    {ParamName.CommandResult}: 123,
    {ParamName.ErrorCode}: ""{ServiceCommandErrorCode.Success}"",
}}";

            ServiceCommandResponse.TryDeserializeFromJsonString(
                    json,
                    out IServiceCommandResponse response,
                    out IServiceCommandResponse errorResponse)
                .Should()
                .BeFalse();

            response.Should().BeNull();
            errorResponse.Should().NotBeNull();

            errorResponse.CommandName.Should().Be(ServiceCommandName.Unknown);
            errorResponse.Result.Should().BeNull();
            errorResponse.ErrorCode.Should().Be(ServiceCommandErrorCode.MissingRequiredMessageValue);
            errorResponse.ErrorMessage.Should().Contain(ParamName.CommandName.ToString());
        }

        //// ===========================================================================================================
        //// SerializeToValueSet Tests
        //// ===========================================================================================================

        [Test]
        public void SerializeToValueSet_should_add_all_of_the_parameters_except_ErrorMessage_when_successful()
        {
            var response = ServiceCommandResponse.Create(ServiceCommandName.RegistryReadIntValue, 123);
            var valueSet = new Dictionary<string, object>();
            response.SerializeToValueSet(valueSet);

            valueSet.Select(pair => ((ParamName)Enum.Parse(typeof(ParamName), pair.Key), pair.Value))
                .Should()
                .BeEquivalentTo(
                    new (ParamName, object)[]
                    {
                        (ParamName.CommandName, ServiceCommandName.RegistryReadIntValue.ToString()),
                        (ParamName.CommandResult, 123),
                        (ParamName.ErrorCode, ServiceCommandErrorCode.Success.ToString()),
                    });
        }

        [Test]
        public void SerializeToValueSet_should_add_all_of_the_parameters_including_ErrorMessage_when_an_error()
        {
            var response = ServiceCommandResponse.CreateError(
                ServiceCommandName.RegistryReadIntValue,
                new InvalidOperationException("message"));

            var valueSet = new Dictionary<string, object>();
            response.SerializeToValueSet(valueSet);

            valueSet.Select(pair => ((ParamName)Enum.Parse(typeof(ParamName), pair.Key), pair.Value))
                .Should()
                .BeEquivalentTo(
                    new (ParamName, object)[]
                    {
                        (ParamName.CommandName, ServiceCommandName.RegistryReadIntValue.ToString()),
                        (ParamName.CommandResult, null),
                        (ParamName.ErrorCode, ServiceCommandErrorCode.InternalError.ToString()),
                        (ParamName.ErrorMessage, "Internal error: message"),
                    });
        }

        //// ===========================================================================================================
        //// SerializeToJsonString Tests
        //// ===========================================================================================================

        [Test]
        public void SerializeToJsonString_should_serialize_into_compact_JSON()
        {
            var response = ServiceCommandResponse.Create(ServiceCommandName.RegistryReadIntValue, 123);
            string expectedJson = $@"
{{
    ""{ParamName.CommandName}"": ""{ServiceCommandName.RegistryReadIntValue}"",
    ""{ParamName.CommandResult}"": 123,
    ""{ParamName.ErrorCode}"": ""{ServiceCommandErrorCode.Success}""
}}
";
            expectedJson = expectedJson.Replace("\r", "")
                .Replace("\n", "")
                .Replace(" ", "");

            response.SerializeToJsonString().Should().Be(expectedJson);
        }

        //// ===========================================================================================================
        //// ToDebugString Tests
        //// ===========================================================================================================

        [Test]
        public void ToDebugString_should_return_the_correct_format_for_successes()
        {
            var response = ServiceCommandResponse.Create(ServiceCommandName.RegistryReadIntValue, 123);
            response.ToDebugString().Should().Be($"{ServiceCommandName.RegistryReadIntValue}: Result=123");
        }

        [Test]
        public void ToDebugString_should_return_the_correct_format_for_errors()
        {
            var response = ServiceCommandResponse.CreateError(
                ServiceCommandName.RegistryWriteIntValue,
                new InvalidOperationException("message"));
            response.ToDebugString()
                .Should()
                .Be(
                    $"{ServiceCommandName.RegistryWriteIntValue}: " +
                    $"ErrorCode={ServiceCommandErrorCode.InternalError}, " +
                    $"ErrorMessage=Internal error: message");
        }
    }
}
