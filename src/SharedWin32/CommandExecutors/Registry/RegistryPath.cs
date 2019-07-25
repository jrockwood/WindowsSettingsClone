// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryPath.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.SharedWin32.CommandExecutors.Registry
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Microsoft.Win32;
    using ServiceContracts.Commands;
    using Shared.Diagnostics;

    /// <summary>
    /// Represents a full path to a registry key or value.
    /// </summary>
    public class RegistryPath
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private RegistryPath(RegistryHive hive, string key, string valueName, object value)
        {
            Hive = hive;
            Key = key ?? string.Empty;
            ValueName = valueName ?? string.Empty;
            Value = value;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public RegistryHive Hive { get; }
        public string HiveAsWin32Name => HiveToWin32Name(Hive);

        public string Key { get; }
        public string ValueName { get; }
        public object Value { get; }

        public bool HasValue => !string.IsNullOrWhiteSpace(ValueName);
        public int IntValue => (int)Value;
        public string StringValue => (string)Value;

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static RegistryPath CreatePath(RegistryHive hive, string key = null)
        {
            return new RegistryPath(hive, key, null, null);
        }

        public static RegistryPath CreateValuePath(RegistryHive hive, string key, string valueName, object value)
        {
            return new RegistryPath(
                hive,
                Param.VerifyString(key, nameof(key)),
                Param.VerifyString(valueName, nameof(valueName)),
                value);
        }

        public static RegistryPath CreateValuePath(RegistryBaseKey baseKey, string key, string valueName, object value)
        {
            return new RegistryPath(
                BaseKeyToHive(baseKey),
                Param.VerifyString(key, nameof(key)),
                Param.VerifyString(valueName, nameof(valueName)),
                value);
        }

        public static RegistryPath Parse(string s)
        {
            string[] parts = s.Split('\\');

            RegistryHive hive = Win32NameToHive(parts[0]);
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

        public static RegistryHive BaseKeyToHive(RegistryBaseKey baseKey)
        {
            switch (baseKey)
            {
                case RegistryBaseKey.ClassesRoot:
                    return RegistryHive.ClassesRoot;

                case RegistryBaseKey.CurrentUser:
                    return RegistryHive.CurrentUser;

                case RegistryBaseKey.LocalMachine:
                    return RegistryHive.LocalMachine;

                case RegistryBaseKey.Users:
                    return RegistryHive.Users;

                case RegistryBaseKey.PerformanceData:
                    return RegistryHive.PerformanceData;

                case RegistryBaseKey.CurrentConfig:
                    return RegistryHive.CurrentConfig;

                case RegistryBaseKey.DynData:
                    return RegistryHive.DynData;

                default:
                    throw new ArgumentOutOfRangeException(nameof(baseKey), baseKey, null);
            }
        }

        public static string HiveToWin32Name(RegistryHive hive)
        {
            switch (hive)
            {
                case RegistryHive.ClassesRoot:
                    return "HKEY_CLASSES_ROOT";

                case RegistryHive.CurrentUser:
                    return "HKEY_CURRENT_USER";

                case RegistryHive.LocalMachine:
                    return "HKEY_LOCAL_MACHINE";

                case RegistryHive.Users:
                    return "HKEY_USERS";

                case RegistryHive.PerformanceData:
                    return "HKEY_PERFORMANCE_DATA";

                case RegistryHive.CurrentConfig:
                    return "HKEY_CURRENT_CONFIG";

                case RegistryHive.DynData:
                    return "HKEY_DYN_DATA";

                default:
                    throw new ArgumentOutOfRangeException(nameof(hive), hive, null);
            }
        }

        public static RegistryHive Win32NameToHive(string win32HiveName)
        {
            switch (win32HiveName.ToUpperInvariant())
            {
                case "HKCR":
                case "HKEY_CLASSES_ROOT":
                    return RegistryHive.ClassesRoot;

                case "HKCU":
                case "HKEY_CURRENT_USER":
                    return RegistryHive.CurrentUser;

                case "HKLM":
                case "HKEY_LOCAL_MACHINE":
                    return RegistryHive.LocalMachine;

                case "HKU":
                case "HKEY_USERS":
                    return RegistryHive.Users;

                case "HKEY_CURRENT_CONFIG":
                    return RegistryHive.CurrentConfig;

                case "HKEY_PERFORMANCE_DATA":
                    return RegistryHive.PerformanceData;

                case "HKEY_DYN_DATA":
                    return RegistryHive.DynData;

                default:
                    throw new InvalidOperationException($"Unknown registry hive: {win32HiveName}");
            }
        }
    }
}
