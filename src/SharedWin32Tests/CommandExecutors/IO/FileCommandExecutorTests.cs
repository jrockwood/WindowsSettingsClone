// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FileCommandExecutorTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests.CommandExecutors.IO
{
    using FluentAssertions;
    using Moq;
    using NUnit.Framework;
    using ServiceContracts.CommandBridge;
    using Shared.CommandBridge;
    using Shared.Commands;
    using SharedWin32.CommandExecutors.IO;

    public class FileCommandExecutorTests
    {
        [Test]
        public void Execute_copy_should_return_true_if_successful()
        {
            var fileSystem = new Mock<IWin32FileSystem>();
            var executor = new FileCommandExecutor(fileSystem.Object);
            executor.Execute(new FileCopyCommand("source", "dest", true))
                .Should()
                .BeEquivalentTo(ServiceCommandResponse.Create(ServiceCommandName.FileCopy, true));

            fileSystem.Verify(x => x.CopyFile("source", "dest", true), Times.Once);
        }
    }
}
