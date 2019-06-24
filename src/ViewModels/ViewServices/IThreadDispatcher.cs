// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IThreadDispatcher.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.ViewServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Abstraction for running methods on a UI or background thread.
    /// </summary>
    public interface IThreadDispatcher
    {
        /// <summary>
        /// Schedules the provided callback on the UI thread from a worker thread, and returns the results asynchronously.
        /// </summary>
        /// <param name="action">The action to run on the UI thread.</param>
        /// <returns>An asynchronous <see cref="Task"/>.</returns>
        Task RunOnUIThreadAsync(Action action);

        /// <summary>
        /// Schedules the provided callback on the UI thread from a worker thread, and returns the results asynchronously.
        /// </summary>
        /// <param name="action">The action to run on the UI thread.</param>
        /// <param name="millisecondsDelay">The number of milliseconds to delay the execution.</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> to use. If the token is canceled before the delay, the action is not run.
        /// </param>
        /// <returns>An asynchronous <see cref="Task"/>.</returns>
        Task RunOnUIThreadDelayedAsync(
            Action action,
            int millisecondsDelay,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Schedules the provided callback on a background thread and returns the results asynchronously.
        /// </summary>
        /// <param name="action">The action to run on a background thread.</param>
        /// <returns>An asynchronous <see cref="Task"/>.</returns>
        Task RunOnBackgroundThreadAsync(Action action);
    }
}
