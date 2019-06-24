// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.DesktopServices
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Hello World";
            Console.WriteLine("This process has access to the entire public desktop API surface");
            Console.WriteLine("Press any key to exit ...");
            Console.ReadLine();
        }
    }
}
