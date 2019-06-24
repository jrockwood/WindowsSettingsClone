// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NamedValue.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
{
    public class NamedValue<T>
    {
        public NamedValue(string displayName, T value)
        {
            DisplayName = displayName;
            Value = value;
        }

        public string DisplayName { get; }
        public T Value { get; }
    }
}
