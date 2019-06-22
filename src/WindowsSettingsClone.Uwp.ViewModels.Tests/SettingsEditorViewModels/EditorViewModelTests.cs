// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorViewModelTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.Tests.SettingsEditorViewModels
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EditorViewModels;
    using FluentAssertions;
    using NUnit.Framework;
    using ViewServices;

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

        [Test]
        public async Task LoadAsync_should_invoke_LoadInternalAsync_using_the_correct_parameters()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            CancellationToken cancellationToken = new CancellationTokenSource().Token;
            await vm.LoadAsync(new UnitTestThreadDispatcher(), cancellationToken: cancellationToken);
            vm.LoadInternalAsyncCalled.Should().BeTrue();
            vm.LoadInternalAsyncCancellationToken.Should().Be(cancellationToken);
        }

        [Test]
        public async Task LoadAsync_should_start_a_timer_for_the_progress_bar_then_run_the_action()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            var dispatcher = new UnitTestThreadDispatcher();

            await vm.LoadAsync(dispatcher);
            dispatcher.Runs.Should().ContainInOrder(DispatchRunKind.UIThreadDelayed, DispatchRunKind.BackgroundThread);
        }

        [Test]
        public async Task LoadAsync_should_pass_in_the_delay_to_the_delayInvoker()
        {
            const int delay = 1;
            var cancellationToken = new CancellationToken();

            var vm = new TestEditorViewModel(s_bonusBar);
            var dispatcher = new UnitTestThreadDispatcher(
                delayInvoker: (ms, token) =>
                {
                    ms.Should().Be(delay);
                    token.Should().NotBe(cancellationToken, "because the progress bar cancellation should be different");
                    return Task.CompletedTask;
                });

            await vm.LoadAsync(dispatcher, delay, cancellationToken);
        }

        [Test]
        public async Task IsIndeterminateProgressBarVisible_should_be_true_after_a_delay_and_the_content_is_not_yet_ready()
        {
            var cancellationSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationSource.Token;

            var vm = new TestEditorViewModel(s_bonusBar);
            var dispatcher = new UnitTestThreadDispatcher(backgroundThreadInvoker: action => Task.Delay(-1, cancellationToken));

            Task task = vm.LoadAsync(dispatcher, 0, cancellationToken);
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

            await vm.LoadAsync(dispatcher);
            vm.IsIndeterminateProgressBarVisible.Should().BeFalse();
        }

        [Test]
        public async Task IsContentReady_should_be_true_after_LoadAsync_returns()
        {
            var vm = new TestEditorViewModel(s_bonusBar);
            var dispatcher = new UnitTestThreadDispatcher();

            await vm.LoadAsync(dispatcher);
            vm.IsContentReady.Should().BeTrue();
        }

        private class TestEditorViewModel : EditorViewModel
        {
            public TestEditorViewModel(BonusBarViewModel bonusBar)
                : base(bonusBar)
            {
            }

            public bool LoadInternalAsyncCalled { get; private set; }
            public CancellationToken LoadInternalAsyncCancellationToken { get; private set; }

            public override EditorKind EditorKind => EditorKind.AccountsYourInfo;
            public override string DisplayName => "Test Setting";

            protected override Task LoadInternalAsync(CancellationToken cancellationToken)
            {
                LoadInternalAsyncCalled = true;
                LoadInternalAsyncCancellationToken = cancellationToken;
                return Task.CompletedTask;
            }
        }
    }
}
