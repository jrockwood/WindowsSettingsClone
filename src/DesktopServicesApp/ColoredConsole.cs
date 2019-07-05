// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ColoredConsole.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.DesktopServicesApp
{
    using System;

    internal static class ColoredConsole
    {
        public static void WriteLine(string message, ConsoleColor foreground)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = foreground;
            Console.WriteLine(message);
            Console.ForegroundColor = currentColor;
        }

        public static void WriteSuccess(string message)
        {
            WriteLine(message, ConsoleColor.Green);
        }

        public static void WriteWarning(string message)
        {
            WriteLine(message, ConsoleColor.Yellow);
        }

        public static void WriteError(string message)
        {
            WriteLine(message, ConsoleColor.Red);
        }
    }
}
