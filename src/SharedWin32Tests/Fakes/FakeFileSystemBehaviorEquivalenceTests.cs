// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeFileSystemBehaviorEquivalenceTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests.Fakes
{
    using System;
    using System.IO;
    using FluentAssertions;
    using NUnit.Framework;

    /// <summary>
    /// Tests that make sure the fake does the same thing as the real implementation.
    /// </summary>
    public class FakeFileSystemBehaviorEquivalenceTests
    {
        private static void TestExceptions(Action osAction, Action fakeAction)
        {
            Exception expectedException = null;

            try
            {
                osAction();
            }
            catch (Exception e)
            {
                expectedException = e;
            }

            expectedException.Should().NotBeNull("because the OS should have thrown an exception");

            fakeAction.Should()
                .Throw<Exception>()
                .Which.Should()
                .BeOfType(expectedException?.GetType())
                .And.BeEquivalentTo(expectedException, ExceptionEquivalenceOptions.GetOptions);
        }

        //// ===========================================================================================================
        //// CopyFile Tests
        //// ===========================================================================================================

        [Test]
        public void CopyFile_throws_the_same_exception_as_the_OS_when_the_source_file_is_not_found()
        {
            const string source = @"C:\NotThere.txt";
            const string dest = @"C:\dest.txt";
            var fs = new FakeFileSystem();
            TestExceptions(() => File.Copy(source, dest), () => fs.CopyFile(source, dest, overwrite: false));
        }

        [Test]
        public void CopyFile_throws_when_the_destination_exists_and_overwrite_is_set_to_false()
        {
            const string source = @"C:\source.txt";
            const string dest = @"C:\dest.txt";
            var fs = new FakeFileSystem($"{source}=source", $"{dest}=dest");
            TestExceptions(
                () => File.Copy(source, dest, overwrite: false),
                () => fs.CopyFile(source, dest, overwrite: false));
        }
    }
}
