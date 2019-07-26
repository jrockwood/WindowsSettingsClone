// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoryLoggerTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Tests.Logging
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;
    using ServiceContracts.Logging;
    using Shared.Logging;

    public class MemoryLoggerTests
    {
        private static MemoryLogger CreateLoggerWithOneOfEachLevel()
        {
            var logger = new MemoryLogger();
            foreach (LogLevel logLevel in Enum.GetValues(typeof(LogLevel)))
            {
                logger.Log(logLevel, ((int)logLevel).ToString());
            }

            return logger;
        }

        [Test]
        public void Log_should_keep_a_history_of_each_logged_message()
        {
            MemoryLogger logger = CreateLoggerWithOneOfEachLevel();
            logger.All.Should().Equal("debug: 0", "info: 1", "success: 2", "warn: 3", "error: 4");
        }

        [Test]
        public void Log_should_not_keep_track_of_discarded_messages()
        {
            var logger = new MemoryLogger(LogLevel.Error);
            logger.Log(LogLevel.Warning, "discarded");
            logger.All.Should().BeEmpty();
        }

        [Test]
        public void Errors_should_return_just_the_errors()
        {
            MemoryLogger logger = CreateLoggerWithOneOfEachLevel();
            logger.Errors.Should().Equal($"error: {(int)LogLevel.Error}");
        }

        [Test]
        public void Warnings_should_return_just_the_warnings()
        {
            MemoryLogger logger = CreateLoggerWithOneOfEachLevel();
            logger.Warnings.Should().Equal($"warn: {(int)LogLevel.Warning}");
        }
    }
}
