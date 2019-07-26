// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeFileSystemTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests.Fakes
{
    using FluentAssertions;
    using NUnit.Framework;

    public class FakeFileSystemTests
    {
        [Test]
        public void CopyFile_should_copy_the_file()
        {
            var fs = new FakeFileSystem(@"C:\a.txt=Contents");
            fs.CopyFile(@"C:\a.txt", @"C:\b.txt", overwrite: false);
            fs.FileExists(@"C:\b.txt").Should().BeTrue();
        }
    }
}
