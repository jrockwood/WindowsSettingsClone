// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleLogger.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.DesktopServicesApp.Logging
{
    using System;
    using ServiceContracts.Logging;

    internal class ConsoleLogger : BaseLogger
    {
        protected override void LogInternal(string message)
        {
            Console.WriteLine(message);
        }
    }
}
