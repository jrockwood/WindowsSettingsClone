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
    using ServiceContracts.Commands;
    using SharedWin32.CommandExecutors.Registry;

    public class RegistryPathTests
    {
        private static EquivalencyAssertionOptions<RegistryPath> GetEquivalenceOptions(
            EquivalencyAssertionOptions<RegistryPath> options)
        {
            return options.Excluding(path => path.StringValue).Excluding(path => path.IntValue);
        }

        [Test]
        public void BaseKeyToHive_should_correctly_map_keys_to_Win32_hives()
        {
            RegistryPath.BaseKeyToHive(RegistryBaseKey.ClassesRoot)
                .Should()
                .Be(RegistryHive.ClassesRoot);
            RegistryPath.BaseKeyToHive(RegistryBaseKey.CurrentUser)
                .Should()
                .Be(RegistryHive.CurrentUser);
            RegistryPath.BaseKeyToHive(RegistryBaseKey.LocalMachine)
                .Should()
                .Be(RegistryHive.LocalMachine);
            RegistryPath.BaseKeyToHive(RegistryBaseKey.Users)
                .Should()
                .Be(RegistryHive.Users);
            RegistryPath.BaseKeyToHive(RegistryBaseKey.PerformanceData)
                .Should()
                .Be(RegistryHive.PerformanceData);
            RegistryPath.BaseKeyToHive(RegistryBaseKey.CurrentConfig)
                .Should()
                .Be(RegistryHive.CurrentConfig);
            RegistryPath.BaseKeyToHive(RegistryBaseKey.DynData)
                .Should()
                .Be(RegistryHive.DynData);
        }

        [Test]
        public void HiveToWin32Name_should_correctly_map_hives_to_long_names()
        {
            RegistryPath.HiveToWin32Name(RegistryHive.ClassesRoot).Should().Be("HKEY_CLASSES_ROOT");
            RegistryPath.HiveToWin32Name(RegistryHive.CurrentUser).Should().Be("HKEY_CURRENT_USER");
            RegistryPath.HiveToWin32Name(RegistryHive.LocalMachine).Should().Be("HKEY_LOCAL_MACHINE");
            RegistryPath.HiveToWin32Name(RegistryHive.Users).Should().Be("HKEY_USERS");
            RegistryPath.HiveToWin32Name(RegistryHive.CurrentConfig).Should().Be("HKEY_CURRENT_CONFIG");
            RegistryPath.HiveToWin32Name(RegistryHive.PerformanceData).Should().Be("HKEY_PERFORMANCE_DATA");
            RegistryPath.HiveToWin32Name(RegistryHive.DynData).Should().Be("HKEY_DYN_DATA");
        }

        [Test]
        public void Win32NameToHive_should_correctly_map_names_to_hives()
        {
            RegistryPath.Win32NameToHive("HKEY_CLASSES_ROOT").Should().Be(RegistryHive.ClassesRoot);
            RegistryPath.Win32NameToHive("HKCR").Should().Be(RegistryHive.ClassesRoot);
            RegistryPath.Win32NameToHive("HKEY_CURRENT_USER").Should().Be(RegistryHive.CurrentUser);
            RegistryPath.Win32NameToHive("HKCU").Should().Be(RegistryHive.CurrentUser);
            RegistryPath.Win32NameToHive("HKEY_LOCAL_MACHINE").Should().Be(RegistryHive.LocalMachine);
            RegistryPath.Win32NameToHive("HKLM").Should().Be(RegistryHive.LocalMachine);
            RegistryPath.Win32NameToHive("HKEY_USERS").Should().Be(RegistryHive.Users);
            RegistryPath.Win32NameToHive("HKU").Should().Be(RegistryHive.Users);
            RegistryPath.Win32NameToHive("HKEY_CURRENT_CONFIG").Should().Be(RegistryHive.CurrentConfig);
            RegistryPath.Win32NameToHive("HKEY_PERFORMANCE_DATA").Should().Be(RegistryHive.PerformanceData);
            RegistryPath.Win32NameToHive("HKEY_DYN_DATA").Should().Be(RegistryHive.DynData);
        }

        //// ===========================================================================================================
        //// Parse Tests
        //// ===========================================================================================================

        [Test]
        public void Parse_should_recognize_just_a_hive()
        {
            RegistryPath.Parse("HKCU")
                .Should()
                .BeEquivalentTo(RegistryPath.CreatePath(RegistryHive.CurrentUser), GetEquivalenceOptions);
            RegistryPath.Parse("HKEY_CURRENT_USER")
                .Should()
                .BeEquivalentTo(RegistryPath.CreatePath(RegistryHive.CurrentUser), GetEquivalenceOptions);
        }

        [Test]
        public void Parse_should_recognize_a_single_sub_level_in_the_path()
        {
            RegistryPath.Parse(@"HKLM\SubKey")
                .Should()
                .BeEquivalentTo(RegistryPath.CreatePath(RegistryHive.LocalMachine, "SubKey"), GetEquivalenceOptions);
        }

        [Test]
        public void Parse_should_recognize_a_nested_path()
        {
            RegistryPath.Parse(@"HKU\Level1\Level2\Level3")
                .Should()
                .BeEquivalentTo(RegistryPath.CreatePath(RegistryHive.Users, @"Level1\Level2\Level3"), GetEquivalenceOptions);
        }

        [Test]
        public void Parse_should_recognize_an_integer_value()
        {
            RegistryPath.Parse(@"HKEY_DYN_DATA\SubKey\IntValue=123")
                .Should()
                .BeEquivalentTo(
                    RegistryPath.CreateValuePath(RegistryHive.DynData, "SubKey", "IntValue", 123),
                    GetEquivalenceOptions);
        }

        [Test]
        public void Parse_should_recognize_a_string_value()
        {
            RegistryPath.Parse(@"HKCR\SubKey\StringValue=""s""")
                .Should()
                .BeEquivalentTo(
                    RegistryPath.CreateValuePath(RegistryHive.ClassesRoot, "SubKey", "StringValue", "s"),
                    GetEquivalenceOptions);

            RegistryPath.Parse(@"HKEY_PERFORMANCE_DATA\SubKey\StringValue='s'")
                .Should()
                .BeEquivalentTo(
                    RegistryPath.CreateValuePath(RegistryHive.PerformanceData, "SubKey", "StringValue", "s"),
                    GetEquivalenceOptions);
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
            RegistryPath.CreateValuePath(RegistryHive.Users, "Key", "Value", 123).HasValue.Should().BeTrue();
        }

        [Test]
        public void HasValue_should_be_false_if_there_is_no_value()
        {
            RegistryPath.CreatePath(RegistryHive.Users, "Key").HasValue.Should().BeFalse();
        }

        //// ===========================================================================================================
        //// IntValue Tests
        //// ===========================================================================================================

        [Test]
        public void IntValue_should_return_an_integer_if_originally_specified()
        {
            RegistryPath.CreateValuePath(RegistryHive.Users, "Key", "Value", 123).IntValue.Should().Be(123);
        }

        [Test]
        public void StringValue_should_return_an_integer_if_originally_specified()
        {
            RegistryPath.CreateValuePath(RegistryHive.Users, "Key", "Value", "s").StringValue.Should().Be("s");
        }

        [Test]
        public void IntValue_should_throw_an_InvalidCastExpression_if_the_value_is_not_an_integer()
        {
            var path = RegistryPath.CreateValuePath(RegistryHive.Users, "Key", "Value", "s");
            Action action = () => { int unused = path.IntValue; };
            action.Should().ThrowExactly<InvalidCastException>();
        }

        [Test]
        public void StringValue_should_throw_an_InvalidCastExpression_if_the_value_is_not_a_string()
        {
            var path = RegistryPath.CreateValuePath(RegistryHive.Users, "Key", "Value", 123);
            Action action = () => { string unused = path.StringValue; };
            action.Should().ThrowExactly<InvalidCastException>();
        }
    }
}
