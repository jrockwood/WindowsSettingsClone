// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeRegistryTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests.Fakes
{
    using System;
    using FluentAssertions;
    using Microsoft.Win32;
    using NUnit.Framework;
    using SharedWin32.CommandExecutors.Registry;

    public class FakeRegistryTests
    {
        [Test]
        public void OpenBaseKey_should_throw_an_exception_for_anything_other_than_x64()
        {
            var registry = new FakeRegistry();
            Action action = () => registry.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
            action.Should().ThrowExactly<ArgumentException>().And.ParamName.Should().Be("view");
        }

        [Test]
        public void OpenSubKey_should_return_null_if_the_key_does_not_exist()
        {
            var registry = new FakeRegistry();
            using (IWin32RegistryKey hkcu = registry.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            {
                hkcu.OpenSubKey("NotThere").Should().BeNull();
            }
        }

        [Test]
        public void CreateSubKey_should_create_a_key_if_it_does_not_exist()
        {
            var registry = new FakeRegistry();
            using (IWin32RegistryKey hkcu = registry.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            using (hkcu.CreateSubKey("SubKey"))
            {
                hkcu.OpenSubKey("SubKey").Should().NotBeNull();
            }
        }

        [Test]
        public void OpenSubKey_and_CreateSubKey_should_accept_nested_paths()
        {
            var registry = new FakeRegistry();
            using (IWin32RegistryKey hkcu = registry.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            using (IWin32RegistryKey subKey = hkcu.CreateSubKey(@"a\b\c"))
            {
                hkcu.OpenSubKey(@"a\b\c").Should().NotBeNull();
                hkcu.OpenSubKey(@"a\b").Should().NotBeNull();
                hkcu.OpenSubKey(@"a").Should().NotBeNull();
            }
        }

        [Test]
        public void GetValue_should_return_null_if_the_value_name_is_not_present()
        {
            var registry = new FakeRegistry();
            using (IWin32RegistryKey hkcu = registry.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            using (IWin32RegistryKey subKey = hkcu.CreateSubKey("SubKey"))
            {
                subKey.GetValue("NoValue").Should().BeNull();
            }
        }

        [Test]
        public void GetValue_should_return_the_default_value_if_not_present()
        {
            var registry = new FakeRegistry();
            using (IWin32RegistryKey hkcu = registry.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            using (IWin32RegistryKey subKey = hkcu.CreateSubKey("SubKey"))
            {
                subKey.GetValue("NoValue", 100).Should().Be(100);
            }
        }

        [Test]
        public void SetValue_should_create_the_value_if_not_present()
        {
            var registry = new FakeRegistry();
            using (IWin32RegistryKey hkcu = registry.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            using (IWin32RegistryKey subKey = hkcu.CreateSubKey("SubKey", writable: true))
            {
                subKey.SetValue("ValueName", 123, RegistryValueKind.DWord);
                subKey.GetValue("ValueName").Should().Be(123);
            }
        }

        [Test]
        public void SetValue_should_throw_an_UnauthorizedAccessException_if_it_is_not_writable()
        {
            var registry = new FakeRegistry();
            using (IWin32RegistryKey hkcu = registry.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            using (IWin32RegistryKey subKey = hkcu.CreateSubKey("SubKey", writable: false))
            {
                Action action = () => subKey.SetValue("ValueName", 123, RegistryValueKind.DWord);
                action.Should().ThrowExactly<UnauthorizedAccessException>();
            }
        }

        [Test]
        public void OpenSubKey_should_throw_ObjectDisposedException_if_closed()
        {
            var registry = new FakeRegistry();
            using (IWin32RegistryKey hkcu = registry.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            {
                IWin32RegistryKey subKey = hkcu.CreateSubKey("SubKey", writable: false);
                subKey.Dispose();

                Action action = () => subKey.OpenSubKey("ValueName");
                action.Should().ThrowExactly<ObjectDisposedException>();
            }
        }
    }
}
