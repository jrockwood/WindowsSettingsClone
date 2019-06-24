// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewModelTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.Tests
{
    using FluentAssertions;
    using FluentAssertions.Events;
    using NUnit.Framework;

    public class BaseViewModelTests
    {
        [Test]
        public void SetProperty_should_set_the_new_value()
        {
            var vm = new TestViewModel { Prop = "new value" };
            vm.Prop.Should().Be("new value");
        }

        [Test]
        public void SetProperty_should_return_false_if_the_value_is_the_same()
        {
            var vm = new TestViewModel { Prop = TestViewModel.InitialPropValue };
            vm.LastSetPropertyResult.Should().BeFalse();
        }

        [Test]
        public void SetProperty_should_return_true_if_the_value_is_different()
        {
            var vm = new TestViewModel { Prop = "new value" };
            vm.LastSetPropertyResult.Should().BeTrue();
        }

        [Test]
        public void SetProperty_should_not_raise_PropertyChanged_if_the_value_is_the_same()
        {
            var vm = new TestViewModel();
            using (IMonitor<TestViewModel> monitoredVm = vm.Monitor())
            {
                vm.Prop = TestViewModel.InitialPropValue;
                monitoredVm.Should().NotRaisePropertyChangeFor(x => x.Prop);
            }
        }

        [Test]
        public void SetProperty_should_raise_PropertyChanged_if_the_value_is_different()
        {
            var vm = new TestViewModel();
            using (IMonitor<TestViewModel> monitoredVm = vm.Monitor())
            {
                vm.Prop = "new value";
                monitoredVm.Should().RaisePropertyChangeFor(x => x.Prop);
            }
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
