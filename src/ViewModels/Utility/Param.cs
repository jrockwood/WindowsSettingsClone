// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Param.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.Utility
{
    using System;

    public static class Param
    {
        public static T VerifyNotNull<T>(T value, string parameterName) where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static string VerifyString(string value, string parameterName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("String cannot be empty or whitespace", parameterName);
            }

            return value;
        }
    }
}
