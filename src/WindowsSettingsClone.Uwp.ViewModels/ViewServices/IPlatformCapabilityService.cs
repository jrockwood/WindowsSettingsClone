// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlatformCapabilityService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.ViewServices
{
    /// <summary>
    /// Interface for querying the underlying platform capabilities.
    /// </summary>
    public interface IPlatformCapabilityService
    {
        bool IsRevealBrushSupported { get; }
    }
}
