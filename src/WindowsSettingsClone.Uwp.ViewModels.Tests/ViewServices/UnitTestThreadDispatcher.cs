﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestThreadDispatcher.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.Tests.ViewServices
{
    using System;
    using System.Collections.Immutable;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.ViewServices;

    /// <summary>
    /// Dispatcher suitable for unit tests where calls are tracked and everything is synchronous.
    /// </summary>
    internal class UnitTestThreadDispatcher : ThreadDispatcher
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private ImmutableArray<DispatchRunKind> _runs = ImmutableArray<DispatchRunKind>.Empty;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public UnitTestThreadDispatcher(
            ThreadInvokerFunc uiThreadInvoker = null,
            ThreadInvokerFunc backgroundThreadInvoker = null,
            DelayInvokerFunc delayInvoker = null)
            : base(uiThreadInvoker ?? RunAction, backgroundThreadInvoker ?? RunAction, delayInvoker ?? DontDelay)
        {
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ImmutableArray<DispatchRunKind> Runs => _runs;

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override Task RunOnUIThreadAsync(Action action)
        {
            AddRunKind(DispatchRunKind.UIThread);
            return base.RunOnUIThreadAsync(action);
        }

        public override Task RunOnUIThreadDelayedAsync(
            Action action,
            int millisecondsDelay,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            AddRunKind(DispatchRunKind.UIThreadDelayed);
            return base.RunOnUIThreadDelayedAsync(action, millisecondsDelay, cancellationToken);
        }

        public override Task RunOnBackgroundThreadAsync(Action action)
        {
            AddRunKind(DispatchRunKind.BackgroundThread);
            return base.RunOnBackgroundThreadAsync(action);
        }

        private static Task RunAction(Action action)
        {
            action();
            return Task.CompletedTask;
        }

        private static Task DontDelay(int millisecondsDelay, CancellationToken cancellationToken) => Task.CompletedTask;

        private void AddRunKind(DispatchRunKind runKind)
        {
            ImmutableArray<DispatchRunKind> originalRuns, newRuns;

            do
            {
                originalRuns = _runs;
                newRuns = _runs.Add(runKind);
            } while (originalRuns != ImmutableInterlocked.InterlockedCompareExchange(ref _runs, newRuns, originalRuns));
        }
    }

    internal enum DispatchRunKind
    {
        UIThread,
        UIThreadDelayed,
        BackgroundThread,
    }
}
