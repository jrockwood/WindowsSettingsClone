// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="PlatformCapabilityService.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewServices
{
    using ViewModels.ViewServices;
    using Windows.Foundation.Metadata;

    /// <summary>
    /// Default implementation of the <see cref="IPlatformCapabilityService"/>. Uses a singleton instance to cache
    /// results since it can be expensive to query the platform.
    /// </summary>
    internal class PlatformCapabilityService : IPlatformCapabilityService
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private PlatformCapabilityService()
        {
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public static PlatformCapabilityService Instance { get; } = new PlatformCapabilityService();

        public bool IsRevealBrushSupported { get; } = ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.RevealBrush");
    }
}
