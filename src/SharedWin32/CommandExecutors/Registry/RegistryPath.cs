// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryPath.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.Registry
{
    using System.Globalization;
    using System.Linq;
    using Microsoft.Win32;
    using Shared.Diagnostics;

    /// <summary>
    /// Represents a full path to a registry key or value.
    /// </summary>
    public class RegistryPath
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private RegistryPath(RegistryHive hive, string path, string valueName, object value)
        {
            Hive = hive;
            Path = path ?? string.Empty;
            ValueName = valueName ?? string.Empty;
            Value = value;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public RegistryHive Hive { get; }
        public string Path { get; }
        public string ValueName { get; }
        public object Value { get; }

        public bool HasValue => !string.IsNullOrWhiteSpace(ValueName);
        public int IntValue => (int)Value;
        public string StringValue => (string)Value;

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static RegistryPath CreatePath(RegistryHive hive, string path = null)
        {
            return new RegistryPath(hive, path, null, null);
        }

        public static RegistryPath CreateValuePath(RegistryHive hive, string path, string valueName, object value)
        {
            return new RegistryPath(
                hive,
                Param.VerifyString(path, nameof(path)),
                Param.VerifyString(valueName, nameof(valueName)),
                value);
        }

        public static RegistryPath Parse(string s)
        {
            string[] parts = s.Split('\\');

            RegistryHive hive = RegistryCommandExecutor.Win32NameToHive(parts[0]);
            string path;
            string valueName = null;
            object value = null;

            int equalIndex = parts.Last().IndexOf('=');
            if (equalIndex < 0)
            {
                path = string.Join("\\", parts.Skip(1));
            }
            else
            {
                path = string.Join("\\", parts.Skip(1).Take(parts.Length - 2));
                valueName = parts.Last().Substring(0, equalIndex);
                string rawValue = parts.Last().Substring(equalIndex + 1);

                if ((rawValue.First() == '\'' && rawValue.Last() == '\'') ||
                    (rawValue.First() == '"' && rawValue.Last() == '"'))
                {
                    value = rawValue.Substring(1, rawValue.Length - 2);
                }
                else
                {
                    value = int.Parse(rawValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
                }
            }

            return new RegistryPath(hive, path, valueName, value);
        }
    }
}
