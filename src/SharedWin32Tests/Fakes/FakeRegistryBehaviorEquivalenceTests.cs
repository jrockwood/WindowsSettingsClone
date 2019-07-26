// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeRegistryBehaviorEquivalenceTests.cs" company="Justin Rockwood">
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

    /// <summary>
    /// Tests that make sure the fake does the same thing as the real implementation.
    /// </summary>
    public class FakeRegistryBehaviorEquivalenceTests
    {
        private const string TestPath = @"__RockwoodTestTemp__";

        [Test]
        [Category("SkipWhenLiveUnitTesting")]
        public void SetValue_throws_the_same_exception_as_the_real_registry()
        {
            Exception expectedException = null;

            using (var hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            using (RegistryKey testKey = hkcu.CreateSubKey(TestPath, writable: false))
            {
                try
                {
                    testKey.SetValue("TestValue", 123, RegistryValueKind.DWord);
                }
                catch (Exception e)
                {
                    expectedException = e;
                }
                finally
                {
                    hkcu.DeleteSubKey(TestPath, throwOnMissingSubKey: false);
                }

                expectedException.Should().NotBeNull();
            }

            var fakeRegistry = new FakeRegistry();
            using (IWin32RegistryKey hkcu = fakeRegistry.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            using (IWin32RegistryKey testKey = hkcu.CreateSubKey(TestPath, writable: false))
            {
                // ReSharper disable once AccessToDisposedClosure
                Action action = () => testKey.SetValue("TestValue", 123, RegistryValueKind.DWord);
                action.Should()
                    .Throw<Exception>()
                    .And.Should()
                    .BeEquivalentTo(expectedException, ExceptionEquivalenceOptions.GetOptions);
            }
        }

        [Test]
        [Category("SkipWhenLiveUnitTesting")]
        public void Closing_a_base_key_does_not_throw_ObjectDisposedException()
        {
            var realKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            realKey.Dispose();
            Action action = () => realKey.CreateSubKey(TestPath);
            action.Should().NotThrow();

            var fakeRegistry = new FakeRegistry();
            IWin32RegistryKey fakeKey = fakeRegistry.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            fakeKey.Dispose();
            action = () => fakeKey.CreateSubKey(TestPath);
            action.Should().NotThrow();
        }

        [Test]
        [Category("SkipWhenLiveUnitTesting")]
        public void An_operation_on_a_disposed_key_throws_the_same_exception_as_the_real_registry()
        {
            Exception expectedException = null;

            using (var hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            {
                RegistryKey testKey = hkcu.CreateSubKey(TestPath, writable: false);
                testKey.Dispose();

                try
                {
                    testKey.GetValue("TestValue");
                }
                catch (Exception e)
                {
                    expectedException = e;
                }
                finally
                {
                    hkcu.DeleteSubKey(TestPath, throwOnMissingSubKey: false);
                }

                expectedException.Should().NotBeNull();
            }

            var fakeRegistry = new FakeRegistry();
            using (IWin32RegistryKey hkcu = fakeRegistry.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            {
                IWin32RegistryKey testKey = hkcu.CreateSubKey(TestPath, writable: false);
                testKey.Dispose();

                // ReSharper disable once AccessToDisposedClosure
                Action action = () => testKey.SetValue("TestValue", 123, RegistryValueKind.DWord);
                action.Should()
                    .Throw<Exception>()
                    .And.Should()
                    .BeEquivalentTo(
                        expectedException,
                        options => options.Excluding(e => e.TargetSite)
                            .Excluding(e => e.StackTrace)
                            .Excluding(e => e.Source)
                            .Excluding(info => info.SelectedMemberInfo.Name == "IPForWatsonBuckets"));
            }
        }
    }
}
