// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionEquivalenceOptions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32Tests
{
    using System;
    using FluentAssertions.Equivalency;

    internal static class ExceptionEquivalenceOptions
    {
        /// <summary>
        /// Returns options to exclude certain fields and properties from an <see cref="Exception"/> when doing
        /// equivalence checks using Fluent Assertions.
        /// </summary>
        /// <remarks>
        /// Use in this way:
        /// <code><![CDATA[
        /// action.Should().Throw<Exception>().And.Should().BeEquivalentTo(expectedException, ExceptionEquivalenceOptions.GetOptions);
        /// ]]></code>
        /// </remarks>
        public static EquivalencyAssertionOptions<Exception> GetOptions(EquivalencyAssertionOptions<Exception> options)
        {
            return options.Excluding(e => e.TargetSite)
                .Excluding(e => e.StackTrace)
                .Excluding(e => e.Source)
                .Excluding(info => info.SelectedMemberInfo.Name == "IPForWatsonBuckets");
        }
    }
}
