// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskExtensions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Threading
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for working with threads and tasks.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Waits for the task to complete or the timeout has expired. Throws a <see cref="TimeoutException"/> if the
        /// task did not complete within the timeout.
        /// </summary>
        /// <param name="task">The <see cref="Task"/> to await.</param>
        /// <param name="millisecondsTimeout">The interval of time to wait before timing out.</param>
        /// <returns>A <see cref="Task"/> that awaits or times out.</returns>
        public static async Task TimeoutAfter(this Task task, int millisecondsTimeout)
        {
            await TimeoutAfter(task, TimeSpan.FromMilliseconds(millisecondsTimeout));
        }

        /// <summary>
        /// Waits for the task to complete or the timeout has expired. Throws a <see cref="TimeoutException"/> if the
        /// task did not complete within the timeout.
        /// </summary>
        /// <param name="task">The <see cref="Task"/> to await.</param>
        /// <param name="timeout">The interval of time to wait before timing out.</param>
        /// <returns>A <see cref="Task"/> that awaits or times out.</returns>
        /// <remarks>
        /// Taken from a StackOverflow answer here: <see
        /// href="https://stackoverflow.com/questions/4238345/asynchronously-wait-for-taskt-to-complete-with-timeout"/>,
        /// which was adapted from code here: <see href="https://devblogs.microsoft.com/pfxteam/crafting-a-task-timeoutafter-method/"/>.
        /// </remarks>
        public static async Task TimeoutAfter(this Task task, TimeSpan timeout)
        {
            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                Task completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
                if (completedTask == task)
                {
                    // Cancel the delayed task to reclaim system resources.
                    timeoutCancellationTokenSource.Cancel();

                    // Very important to await the task again to propagate exceptions.
                    await task;
                }
                else
                {
                    throw new TimeoutException();
                }
            }
        }
    }
}
