// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryCommandsTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Tests.CommandBridge
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Commands;
    using FluentAssertions;
    using NUnit.Framework;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using Shared.CommandBridge;

    public class RegistryCommandsTests
    {
        [Test]
        public void RegistryReadValueCommand_ctor_should_throw_on_null_args()
        {
            Action action = () => new RegistryReadIntValueCommand(RegistryHive.CurrentUser, null, "ValueName", 123);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("key");
        }

        [Test]
        public void RegistryReadValueCommand_ctor_should_not_throw_on_null_ValueName()
        {
            Action action = () => new RegistryReadIntValueCommand(RegistryHive.CurrentUser, "Key", null, 123);
            action.Should().NotThrow();
        }

        [Test]
        public void RegistryReadValueCommand_should_serialize_correctly()
        {
            var command = new RegistryReadIntValueCommand(RegistryHive.CurrentUser, "Key", "ValueName", 123);
            var valueSet = new Dictionary<string, object>();
            command.SerializeTo(valueSet);

            valueSet.Select(pair => ((ParamName)Enum.Parse(typeof(ParamName), pair.Key), pair.Value))
                .Should()
                .BeEquivalentTo(
                    new (ParamName, object)[]
                    {
                        (ParamName.CommandName, ServiceCommandName.RegistryReadIntValue.ToString()),
                        (ParamName.RegistryHive, RegistryHive.CurrentUser.ToString()),
                        (ParamName.RegistryKey, "Key"),
                        (ParamName.RegistryValueName, "ValueName"),
                        (ParamName.RegistryDefaultValue, 123)
                    });
        }
    }
}
