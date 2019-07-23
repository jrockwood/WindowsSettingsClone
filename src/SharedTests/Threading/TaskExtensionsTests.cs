// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskExtensionsTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Tests.Threading
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;
    using Shared.Threading;

    public class TaskExtensionsTests
    {
        [Test]
        public async Task TimeoutAfter_returns_the_original_task_if_it_ran_to_completion_within_the_timeout()
        {
            Func<Task> action = () => Task.CompletedTask.TimeoutAfter(TimeSpan.FromMinutes(1));
            await action.Should().CompleteWithinAsync(TimeSpan.FromMinutes(1));
        }

        [Test]
        public async Task TimeoutAfter_should_timeout_if_the_task_has_not_completed_within_the_timeout_period()
        {
            Func<Task> action = () => Task.Delay(TimeSpan.FromMinutes(1)).TimeoutAfter(TimeSpan.FromMilliseconds(1));
            await action.Should().ThrowExactlyAsync<TimeoutException>();
        }
    }
}
