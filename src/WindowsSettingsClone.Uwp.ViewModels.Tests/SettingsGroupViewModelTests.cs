// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsGroupViewModelTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.Tests
{
    using NUnit.Framework;

    public class SettingsGroupViewModelTests
    {
        [Test]
        public void SettingsGroupViewModel_ctor_should_throw_ArgumentNullExceptions()
        {
            TestDelegate action = () =>
                _ = new SettingsGroupViewModel(
                    name: null,
                    description: "Description",
                    glyph: GlyphKind.Accounts,
                    pages: new SettingPageViewModel[0]);
            Assert.That(action, Throws.ArgumentNullException.With.Property("ParamName").EqualTo("name"));

            action = () => _ = new SettingsGroupViewModel(
                "Name",
                description: null,
                glyph: GlyphKind.Accounts,
                pages: new SettingPageViewModel[0]);
            Assert.That(action, Throws.ArgumentNullException.With.Property("ParamName").EqualTo("description"));
        }

        [Test]
        public void SettingsGroupViewModel_ctor_should_throw_ArgumentExceptions_for_empty_strings()
        {
            TestDelegate action = () =>
                _ = new SettingsGroupViewModel(
                    name: "",
                    description: "Description",
                    glyph: GlyphKind.Accounts,
                    pages: new SettingPageViewModel[0]);
            Assert.That(action, Throws.ArgumentException.With.Property("ParamName").EqualTo("name"));

            action = () => _ = new SettingsGroupViewModel(
                "Name",
                description: "",
                glyph: GlyphKind.Accounts,
                pages: new SettingPageViewModel[0]);
            Assert.That(action, Throws.ArgumentException.With.Property("ParamName").EqualTo("description"));
        }

        [Test]
        public void SettingsGroupViewModel_ctor_should_store_the_parameters_in_properties()
        {
            var vm = new SettingsGroupViewModel("Name", "Description", GlyphKind.Phone, new SettingPageViewModel[0]);
            Assert.That(vm.Name, Is.EqualTo("Name"));
            Assert.That(vm.Description, Is.EqualTo("Description"));
            Assert.That(vm.Glyph, Is.EqualTo(GlyphKind.Phone));
        }
    }
}
