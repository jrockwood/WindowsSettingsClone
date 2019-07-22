// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryPathTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests.CommandExecutors.Registry
{
    using System;
    using FluentAssertions;
    using FluentAssertions.Equivalency;
    using Microsoft.Win32;
    using NUnit.Framework;
    using SharedWin32.CommandExecutors.Registry;

    public class RegistryPathTests
    {
        private static EquivalencyAssertionOptions<RegistryPath> GetOptions(
            EquivalencyAssertionOptions<RegistryPath> options)
        {
            return options.Excluding(path => path.StringValue).Excluding(path => path.IntValue);
        }

        //// ===========================================================================================================
        //// Parse Tests
        //// ===========================================================================================================

        [Test]
        public void Parse_should_recognize_just_a_hive()
        {
            RegistryPath.Parse("HKCU")
                .Should()
                .BeEquivalentTo(RegistryPath.CreatePath(RegistryHive.CurrentUser), GetOptions);
            RegistryPath.Parse("HKEY_CURRENT_USER")
                .Should()
                .BeEquivalentTo(RegistryPath.CreatePath(RegistryHive.CurrentUser), GetOptions);
        }

        [Test]
        public void Parse_should_recognize_a_single_sub_level_in_the_path()
        {
            RegistryPath.Parse(@"HKLM\SubKey")
                .Should()
                .BeEquivalentTo(RegistryPath.CreatePath(RegistryHive.LocalMachine, "SubKey"), GetOptions);
        }

        [Test]
        public void Parse_should_recognize_a_nested_path()
        {
            RegistryPath.Parse(@"HKU\Level1\Level2\Level3")
                .Should()
                .BeEquivalentTo(RegistryPath.CreatePath(RegistryHive.Users, @"Level1\Level2\Level3"), GetOptions);
        }

        [Test]
        public void Parse_should_recognize_an_integer_value()
        {
            RegistryPath.Parse(@"HKEY_DYN_DATA\SubKey\IntValue=123")
                .Should()
                .BeEquivalentTo(
                    RegistryPath.CreateValuePath(RegistryHive.DynData, "SubKey", "IntValue", 123),
                    GetOptions);
        }

        [Test]
        public void Parse_should_recognize_a_string_value()
        {
            RegistryPath.Parse(@"HKCR\SubKey\StringValue=""s""")
                .Should()
                .BeEquivalentTo(
                    RegistryPath.CreateValuePath(RegistryHive.ClassesRoot, "SubKey", "StringValue", "s"),
                    GetOptions);

            RegistryPath.Parse(@"HKEY_PERFORMANCE_DATA\SubKey\StringValue='s'")
                .Should()
                .BeEquivalentTo(
                    RegistryPath.CreateValuePath(RegistryHive.PerformanceData, "SubKey", "StringValue", "s"),
                    GetOptions);
        }

        [Test]
        public void Parse_should_throw_a_FormatException_when_the_value_is_not_a_string_or_integer()
        {
            Action action = () => RegistryPath.Parse(@"HKEY_CURRENT_CONFIG\SubKey\Value=$123");
            action.Should().ThrowExactly<FormatException>();
        }

        //// ===========================================================================================================
        //// HasValue Tests
        //// ===========================================================================================================

        [Test]
        public void HasValue_should_be_true_if_there_is_a_value()
        {
            RegistryPath.CreateValuePath(RegistryHive.Users, "Path", "Value", 123).HasValue.Should().BeTrue();
        }

        [Test]
        public void HasValue_should_be_false_if_there_is_no_value()
        {
            RegistryPath.CreatePath(RegistryHive.Users, "Path").HasValue.Should().BeFalse();
        }

        //// ===========================================================================================================
        //// IntValue Tests
        //// ===========================================================================================================

        [Test]
        public void IntValue_should_return_an_integer_if_originally_specified()
        {
            RegistryPath.CreateValuePath(RegistryHive.Users, "Path", "Value", 123).IntValue.Should().Be(123);
        }

        [Test]
        public void StringValue_should_return_an_integer_if_originally_specified()
        {
            RegistryPath.CreateValuePath(RegistryHive.Users, "Path", "Value", "s").StringValue.Should().Be("s");
        }

        [Test]
        public void IntValue_should_throw_an_InvalidCastExpression_if_the_value_is_not_an_integer()
        {
            var path = RegistryPath.CreateValuePath(RegistryHive.Users, "Path", "Value", "s");
            Action action = () => { int unused = path.IntValue; };
            action.Should().ThrowExactly<InvalidCastException>();
        }

        [Test]
        public void StringValue_should_throw_an_InvalidCastExpression_if_the_value_is_not_a_string()
        {
            var path = RegistryPath.CreateValuePath(RegistryHive.Users, "Path", "Value", 123);
            Action action = () => { string unused = path.StringValue; };
            action.Should().ThrowExactly<InvalidCastException>();
        }
    }
}
