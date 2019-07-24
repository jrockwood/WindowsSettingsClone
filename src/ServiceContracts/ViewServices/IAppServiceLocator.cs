// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IAppServiceLocator.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ServiceContracts.ViewServices
{
    using Win32;
    using Win32Services;

    /// <summary>
    /// Service contract for finding other services that are meant to be used across the entire application. For example,
    /// there should only every be one instance of a <see cref="INavigationViewService"/>.
    /// </summary>
    public interface IAppServiceLocator
    {
        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        INavigationViewService NavigationViewService { get; }
        IPlatformCapabilityService PlatformCapabilityService { get; }
        IThreadDispatcher ThreadDispatcher { get; }
        IRegistryReadService RegistryReadService { get; }
        IRegistryWriteService RegistryWriteService { get; }
        IWin32ApiService Win32ApiService { get; }
    }
}
