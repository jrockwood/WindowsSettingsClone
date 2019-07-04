// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseLoggerTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.Tests.Logging
{
    using FluentAssertions;
    using NUnit.Framework;
    using ServiceContracts.Logging;

    public class BaseLoggerTests
    {
        [Test]
        public void MinimumLogLevel_should_default_at_Warning_level()
        {
            var logger = new TestLogger();
            logger.MinimumLogLevel.Should().Be(LogLevel.Warning);
        }

        [Test]
        public void MinimumLogLevel_should_be_set_correctly()
        {
            var logger = new TestLogger
            {
                MinimumLogLevel = LogLevel.Informational
            };
            logger.MinimumLogLevel.Should().Be(LogLevel.Informational);
        }

        [Test]
        public void Log_should_correctly_format_the_message_with_the_level_prefix()
        {
            var logger = new TestLogger { MinimumLogLevel = LogLevel.Debug };
            logger.Log(LogLevel.Debug, "message");
            logger.LastLogMessage.Should().Be("debug: message");

            logger.Log(LogLevel.Informational, "message");
            logger.LastLogMessage.Should().Be("info: message");

            logger.Log(LogLevel.Warning, "message");
            logger.LastLogMessage.Should().Be("warn: message");

            logger.Log(LogLevel.Error, "message");
            logger.LastLogMessage.Should().Be("error: message");
        }

        [Test]
        public void Log_should_format_the_message_with_args()
        {
            var logger = new TestLogger();
            logger.Log(LogLevel.Error, "{0} {1}", "one", "two");
            logger.LastLogMessage.Should().EndWith("one two");
        }

        [Test]
        public void Log_should_skip_messages_below_the_minimum_level()
        {
            var logger = new TestLogger();
            logger.Log(LogLevel.Debug, "skipped");
            logger.LastLogMessage.Should().BeNull();
        }

        private sealed class TestLogger : BaseLogger
        {
            public string LastLogMessage { get; private set; }

            protected override void LogInternal(string message)
            {
                LastLogMessage = message;
            }
        }
    }
}
