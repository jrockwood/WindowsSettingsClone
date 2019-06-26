// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadDispatcherTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

// ReSharper disable InvokeAsExtensionMethod
namespace WindowsSettingsClone.ServiceContracts.Tests.ViewServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;
    using ServiceContracts.ViewServices;

    public class ThreadDispatcherTests
    {
        [Test]
        public void Ctor_should_throw_on_null_arguments()
        {
            Action action = () => new ThreadDispatcher(null);
            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("uiThreadInvoker");
        }

        [Test]
        public async Task RunOnUIThreadAsync_should_run_on_the_UI_thread()
        {
            bool ranOnUIThread = false;
            bool ranOnBackgroundThread = false;
            bool ranAction = false;

            var dispatcher = new ThreadDispatcher(
                uiThreadInvoker: action =>
                {
                    ranOnUIThread = true;
                    action();
                    return Task.CompletedTask;
                },
                backgroundThreadInvoker: action =>
                {
                    ranOnBackgroundThread = true;
                    action();
                    return Task.CompletedTask;
                });

            await dispatcher.RunOnUIThreadAsync(() => ranAction = true);

            ranOnUIThread.Should().BeTrue();
            ranOnBackgroundThread.Should().BeFalse();
            ranAction.Should().BeTrue();
        }

        [Test]
        public async Task RunOnBackgroundThreadAsync_should_run_on_the_background_thread()
        {
            bool ranOnUIThread = false;
            bool ranOnBackgroundThread = false;
            bool ranAction = false;

            var dispatcher = new ThreadDispatcher(
                uiThreadInvoker: action =>
                {
                    ranOnUIThread = true;
                    action();
                    return Task.CompletedTask;
                },
                backgroundThreadInvoker: action =>
                {
                    ranOnBackgroundThread = true;
                    action();
                    return Task.CompletedTask;
                });

            await dispatcher.RunOnBackgroundThreadAsync(() => ranAction = true);

            ranOnUIThread.Should().BeFalse();
            ranOnBackgroundThread.Should().BeTrue();
            ranAction.Should().BeTrue();
        }

        [Test]
        public void RunOnUIThreadDelayedAsync_should_throw_on_null_arguments()
        {
            var dispatcher = new ThreadDispatcher(_ => Task.CompletedTask);
            Func<Task> func = async () => await dispatcher.RunOnUIThreadDelayedAsync(null, 0);
            func.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("action");
        }

        [Test]
        public async Task RunOnUIThreadDelayedAsync_should_run_after_a_delay()
        {
            bool ranOnUIThread = false;
            bool ranDelayed = false;
            bool ranAction = false;

            var dispatcher = new ThreadDispatcher(
                uiThreadInvoker: action =>
                {
                    ranOnUIThread = true;
                    action();
                    return Task.CompletedTask;
                },
                delayInvoker: (ms, token) =>
                {
                    ranDelayed = true;
                    return Task.CompletedTask;
                });

            await dispatcher.RunOnUIThreadDelayedAsync(() => ranAction = true, 0);

            ranOnUIThread.Should().BeTrue();
            ranDelayed.Should().BeTrue();
            ranAction.Should().BeTrue();
        }

        [Test]
        public async Task RunOnUIThreadDelayedAsync_should_not_run_if_canceled_before_the_delay_timeout()
        {
            var dispatcher = new ThreadDispatcher(_ => Task.CompletedTask);
            var cancellationSource = new CancellationTokenSource();

            bool actionRan = false;
            Task task = dispatcher.RunOnUIThreadDelayedAsync(
                () => actionRan = true,
                -1,
                cancellationSource.Token);

            cancellationSource.Cancel();
            await task;

            actionRan.Should().BeFalse();
        }
    }
}
