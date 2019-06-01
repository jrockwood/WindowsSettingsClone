// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewModelTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.Tests
{
    using NUnit.Framework;

    public class BaseViewModelTests
    {
        [Test]
        public void SetProperty_should_set_the_new_value()
        {
            var vm = new TestViewModel { Prop = "new value" };
            Assert.That(vm.Prop, Is.EqualTo("new value"));
        }

        [Test]
        public void SetProperty_should_return_false_if_the_value_is_the_same()
        {
            var vm = new TestViewModel { Prop = TestViewModel.InitialPropValue };
            Assert.That(vm.LastSetPropertyResult, Is.False);
        }

        [Test]
        public void SetProperty_should_return_true_if_the_value_is_different()
        {
            var vm = new TestViewModel { Prop = "new value" };
            Assert.That(vm.LastSetPropertyResult, Is.True);
        }

        [Test]
        public void SetProperty_should_not_raise_PropertyChanged_if_the_value_is_the_same()
        {
            var vm = new TestViewModel();
            bool raised = false;
            vm.PropertyChanged += (sender, args) => raised = true;

            vm.Prop = TestViewModel.InitialPropValue;
            Assert.That(raised, Is.False);
        }

        [Test]
        public void SetProperty_should_raise_PropertyChanged_if_the_value_is_different()
        {
            var vm = new TestViewModel();
            bool raised = false;
            vm.PropertyChanged += (sender, args) => raised = true;

            vm.Prop = "new value";
            Assert.That(raised, Is.True);
        }

        private sealed class TestViewModel : BaseViewModel
        {
            public const string InitialPropValue = "Initial";

            private string _prop = InitialPropValue;

            public bool LastSetPropertyResult { get; private set; }

            public string Prop
            {
                get => _prop;
                set => LastSetPropertyResult = SetProperty(ref _prop, value);
            }
        }
    }
}
