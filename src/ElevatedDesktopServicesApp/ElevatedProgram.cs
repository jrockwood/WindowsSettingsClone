﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ElevatedProgram.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ElevatedDesktopServicesApp
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.Win32;
    using ServiceContracts.Logging;
    using Shared.Logging;

    internal sealed class ElevatedProgram
    {
        public static void Main(string[] args)
        {
            ILogger logger = CreateLogger();

            AppDomain.CurrentDomain.UnhandledException +=
                (_, e) => logger.LogError($"Terminating: {e.ExceptionObject}");

            logger.LogDebug("Setting the registry key");

            using (var hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            using (RegistryKey valueKey = hkcu.OpenSubKey(
                @"Control Panel\Personalization\Desktop Slideshow",
                writable: true))
            {
                valueKey.SetValue("Shuffle", 1, RegistryValueKind.DWord);
            }

            logger.LogSuccess("Set the registry key");
        }

        private static ILogger CreateLogger()
        {
            string filePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException(),
                "ElevatedProgram.log");

            return FileLogger.TryCreate(filePath, LogLevel.Debug, out FileLogger fileLogger)
                ? (ILogger)fileLogger
                : new NullLogger();
        }
    }
}
