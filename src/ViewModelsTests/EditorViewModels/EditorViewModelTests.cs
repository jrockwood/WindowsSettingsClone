// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorViewModelTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.Tests.EditorViewModels
{
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;
    using ServiceContracts.ViewServices;
    using ServiceContracts.Win32;
    using Shared.Logging;
    using Shared.Tests.FakeServices;
    using Shared.Tests.ViewServices;
    using ViewModels.EditorViewModels;

    public class EditorViewModelTests
    {
        private static readonly BonusBarViewModel s_bonusBar = new BonusBarViewModel(null);

        [Test]
        public void Ctor_should_throw_on_null_arguments()
        {
            Action action = () => new TestEditorViewModel(null);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("bonusBar");
        }

        [Test]
        public void IsContentReady_should_be_false_initially()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            vm.IsContentReady.Should().BeFalse();
        }

        [Test]
        public void IsIndeterminateProgressBarVisible_should_be_false_initially()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            vm.IsIndeterminateProgressBarVisible.Should().BeFalse();
        }

        //// ===========================================================================================================
        //// LoadAsync Tests
        //// ===========================================================================================================

        [Test]
        public async Task LoadAsync_should_invoke_LoadInternalAsync_using_the_correct_parameters()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            CancellationToken cancellationToken = new CancellationTokenSource().Token;
            await vm.LoadAsync(new FakeRegistryService(), cancellationToken: cancellationToken);
            vm.LoadInternalAsyncCalled.Should().BeTrue();
            vm.LoadInternalAsyncCancellationToken.Should().Be(cancellationToken);
        }

        [Test]
        public async Task LoadAsync_should_start_a_timer_for_the_progress_bar_then_run_the_action()
        {
            var dispatcher = new UnitTestThreadDispatcher();
            var vm = new TestEditorViewModel(s_bonusBar, dispatcher);

            await vm.LoadAsync(new FakeRegistryService());
            dispatcher.Runs.Should().ContainInOrder(DispatchRunKind.UIThreadDelayed, DispatchRunKind.BackgroundThread);
        }

        [Test]
        public async Task LoadAsync_should_pass_in_the_delay_to_the_delayInvoker()
        {
            const int delay = 1;
            var cancellationToken = new CancellationToken();

            var dispatcher = new UnitTestThreadDispatcher(
                delayInvoker: (ms, token) =>
                {
                    ms.Should().Be(delay);
                    token.Should()
                        .NotBe(cancellationToken, "because the progress bar cancellation should be different");
                    return Task.CompletedTask;
                });
            var vm = new TestEditorViewModel(s_bonusBar, dispatcher);

            await vm.LoadAsync(new FakeRegistryService(), delay, cancellationToken);
        }

        [Test]
        public async Task
            IsIndeterminateProgressBarVisible_should_be_true_after_a_delay_and_the_content_is_not_yet_ready()
        {
            var cancellationSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationSource.Token;

            var dispatcher =
                new UnitTestThreadDispatcher(backgroundAsyncThreadInvoker: action => Task.Delay(-1, cancellationToken));
            var vm = new TestEditorViewModel(s_bonusBar, dispatcher);

            Task task = vm.LoadAsync(new FakeRegistryService(), 0, cancellationToken);
            vm.IsIndeterminateProgressBarVisible.Should().BeTrue();
            cancellationSource.Cancel();

            Func<Task> func = async () => await task;
            await func.Should().ThrowExactlyAsync<TaskCanceledException>();
        }

        [Test]
        public async Task IsIndeterminateProgressBarVisible_should_be_false_after_the_load_returns()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            var dispatcher = new UnitTestThreadDispatcher();

            await vm.LoadAsync(new FakeRegistryService());
            vm.IsIndeterminateProgressBarVisible.Should().BeFalse();
        }

        [Test]
        public async Task IsContentReady_should_be_true_after_LoadAsync_returns()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            var dispatcher = new UnitTestThreadDispatcher();

            await vm.LoadAsync(new FakeRegistryService());
            vm.IsContentReady.Should().BeTrue();
        }

        //// ===========================================================================================================
        //// SetModelPropertyAsync Tests
        //// ===========================================================================================================

        [Test]
        public async Task SetModelPropertyAsync_should_invoke_the_task()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            bool wasInvoked = false;
            await vm.SetModelPropertyAsync(() =>
            {
                wasInvoked = true;
                return Task.CompletedTask;
            }, "PropertyName");

            wasInvoked.Should().BeTrue();
        }

        [Test]
        public async Task SetModelPropertyAsync_should_set_UpdateErrorMessage_if_there_was_a_timeout()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            await vm.SetModelPropertyAsync(() => Task.Delay(TimeSpan.FromMinutes(1)), "LongProperty", 1);
            vm.UpdateErrorMessage.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public async Task SetModelPropertyAsync_should_set_UpdateErrorMessage_if_there_was_an_error()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            await vm.SetModelPropertyAsync(() => Task.FromException(new InvalidOperationException()), "Error");
            vm.UpdateErrorMessage.Should().NotBeNullOrWhiteSpace();
        }

        //// ===========================================================================================================
        //// SetPropertyAndPerformAsyncUpdate Tests
        //// ===========================================================================================================

        [Test]
        public void SetPropertyAndPerformAsyncUpdate_should_set_the_property()
        {
            string fakeProperty = "Nothing";

            var vm = new TestEditorViewModel(s_bonusBar);
            vm.SetPropertyAndPerformAsyncUpdate(
                    ref fakeProperty,
                    "Changed",
                    () => Task.CompletedTask,
                    TimeSpan.FromMinutes(1).Milliseconds,
                    // ReSharper disable once ExplicitCallerInfoArgument
                    "TestProperty")
                .Should()
                .BeTrue();

            fakeProperty.Should().Be("Changed");
        }

        [Test]
        public void SetPropertyAndPerformAsyncUpdate_should_not_do_anything_if_the_property_has_not_chnaged()
        {
            string fakeProperty = "Nothing";

            var vm = new TestEditorViewModel(s_bonusBar);
            vm.SetPropertyAndPerformAsyncUpdate(
                    ref fakeProperty,
                    "Nothing",
                    () => Task.CompletedTask,
                    TimeSpan.FromMinutes(1).Milliseconds,
                    // ReSharper disable once ExplicitCallerInfoArgument
                    "TestProperty")
                .Should()
                .BeFalse();

            fakeProperty.Should().Be("Nothing");
        }

        [Test]
        public void SetPropertyAndPerformAsyncUpdate_should_not_invoke_the_update_func_if_loading()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            typeof(EditorViewModel)
                .InvokeMember(
                    "IsLoading",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty,
                    Type.DefaultBinder,
                    vm,
                    new object[] { true });

            int value = 0;
            bool invokedUpdateAction = false;
            vm.SetPropertyAndPerformAsyncUpdate(
                    ref value,
                    10,
                    () =>
                    {
                        invokedUpdateAction = true;
                        return Task.CompletedTask;
                    },
                    TimeSpan.FromMinutes(1).Milliseconds,
                    // ReSharper disable once ExplicitCallerInfoArgument
                    "TestProperty")
                .Should()
                .BeTrue();

            invokedUpdateAction.Should().BeFalse();
        }

        private class TestEditorViewModel : EditorViewModel
        {
            public TestEditorViewModel(BonusBarViewModel bonusBar, IThreadDispatcher threadDispatcher = null)
                : base(
                    new NullLogger(),
                    threadDispatcher ?? new UnitTestThreadDispatcher(),
                    new DoNothingRegistryWriteService(),
                    bonusBar)
            {
            }

            public bool LoadInternalAsyncCalled { get; private set; }
            public CancellationToken LoadInternalAsyncCancellationToken { get; private set; }

            public override EditorKind EditorKind => EditorKind.AccountsYourInfo;
            public override string DisplayName => "Test Setting";

            public Task LoadingTask { get; set; } = Task.CompletedTask;

            protected override Task LoadInternalAsync(
                IRegistryReadService registryReadService,
                CancellationToken cancellationToken)
            {
                LoadInternalAsyncCalled = true;
                LoadInternalAsyncCancellationToken = cancellationToken;
                return LoadingTask;
            }
        }
    }
}
