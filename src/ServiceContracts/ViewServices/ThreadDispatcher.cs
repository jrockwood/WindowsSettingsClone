// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadDispatcher.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.ViewServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public delegate Task ThreadInvokerFunc(Action action);

    public delegate Task DelayInvokerFunc(int millisecondsDelay, CancellationToken cancellationToken);

    /// <summary>
    /// Implementation for an <see cref="IThreadDispatcher"/> using delegates for invoking operations on a certain thread.
    /// </summary>
    public class ThreadDispatcher : IThreadDispatcher
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly ThreadInvokerFunc _uiThreadInvoker;
        private readonly ThreadInvokerFunc _backgroundThreadInvoker;
        private readonly DelayInvokerFunc _delayInvoker;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public ThreadDispatcher(
            ThreadInvokerFunc uiThreadInvoker,
            ThreadInvokerFunc backgroundThreadInvoker = null,
            DelayInvokerFunc delayInvoker = null)
        {
            _uiThreadInvoker = uiThreadInvoker ?? throw new ArgumentNullException(nameof(uiThreadInvoker));
            _backgroundThreadInvoker = backgroundThreadInvoker ?? Task.Run;
            _delayInvoker = delayInvoker ?? Task.Delay;
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public virtual Task RunOnUIThreadAsync(Action action)
        {
            return _uiThreadInvoker(action);
        }

        public virtual Task RunOnBackgroundThreadAsync(Action action)
        {
            return _backgroundThreadInvoker(action);
        }

        public virtual async Task RunOnUIThreadDelayedAsync(
            Action action,
            int millisecondsDelay,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            try
            {
                await _delayInvoker(millisecondsDelay, cancellationToken);
                await RunOnUIThreadAsync(action);
            }
            catch (TaskCanceledException)
            {
            }
        }
    }
}
