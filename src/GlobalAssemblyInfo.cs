// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalAssemblyInfo.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// Company and Product
[assembly: AssemblyCompany("Justin Rockwood")]
[assembly: AssemblyProduct("Windows Settings Clone")]
[assembly: AssemblyCopyright("Copyright © 2019 Justin Rockwood. All rights reserved.")]
[assembly: AssemblyTrademark("")]

// Localization
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en-US")]

// Version Information
[assembly: AssemblyInformationalVersion("1.0")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Other
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("")]
#endif
[assembly: ComVisible(false)]
