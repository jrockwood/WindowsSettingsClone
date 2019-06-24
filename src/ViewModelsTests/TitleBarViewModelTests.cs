// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TitleBarViewModelTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.Tests
{
    using System;
    using FluentAssertions;
    using FluentAssertions.Events;
    using Moq;
    using NUnit.Framework;
    using ViewModels.ViewServices;

    public class TitleBarViewModelTests
    {
        [Test]
        public void Ctor_should_throw_on_null_arguments()
        {
            Action action = () => _ = new TitleBarViewModel(null);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("navigationViewService");
        }

        [Test]
        public void CanGoBack_should_be_false_when_there_is_no_back_history()
        {
            var mock = new Mock<INavigationViewService>();
            mock.Setup(service => service.CanGoBack).Returns(false);

            var vm = new TitleBarViewModel(mock.Object);
            vm.CanGoBack.Should().BeFalse();
        }

        [Test]
        public void CanGoBack_should_be_true_when_there_is_back_history()
        {
            var mock = new Mock<INavigationViewService>();
            mock.Setup(service => service.CanGoBack).Returns(true);

            var vm = new TitleBarViewModel(mock.Object);
            vm.CanGoBack.Should().BeTrue();
        }

        [Test]
        public void CanGoBack_should_raise_PropertyChanged_when_the_navigation_service_adds_a_back_history()
        {
            var mock = new Mock<INavigationViewService>();
            var vm = new TitleBarViewModel(mock.Object);
            using (IMonitor<TitleBarViewModel> monitoredVm = vm.Monitor())
            {
                mock.Setup(service => service.BackStackDepth).Returns(1);
                mock.Raise(x => x.BackStackDepthChange += null, EventArgs.Empty);
                monitoredVm.Should().RaisePropertyChangeFor(x => x.CanGoBack);
            }
        }

        [Test]
        public void BackCommand_should_tell_the_navigation_service_to_go_back_if_there_are_back_entries()
        {
            var mock = new Mock<INavigationViewService>();
            mock.Setup(service => service.CanGoBack).Returns(true);
            var vm = new TitleBarViewModel(mock.Object);
            vm.BackCommand.Execute(null);
            mock.Verify(service => service.GoBack(), Times.Once);
        }

        [Test]
        public void BackCommand_should_do_nothing_if_the_navigation_service_does_not_have_back_entries()
        {
            var mock = new Mock<INavigationViewService>();
            var vm = new TitleBarViewModel(mock.Object);
            vm.BackCommand.Execute(null);
            mock.Verify(service => service.GoBack(), Times.Never);
        }
    }
}
