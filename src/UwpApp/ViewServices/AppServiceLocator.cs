// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="AppServiceLocator.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.ViewServices
{
    using System;
    using ServiceContracts.ViewServices;
    using ServiceContracts.Win32Services;
    using Views;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Finds or stores all of the singleton application-level services.
    /// </summary>
    internal class AppServiceLocator : IAppServiceLocator
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private IRegistryReadService _registryReadService;
        private IRegistryWriteService _registryWriteService;

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public INavigationViewService NavigationViewService =>
            ((Window.Current.Content as Frame)?.Content as RootPage)?.NavigationService;

        public IPlatformCapabilityService PlatformCapabilityService { get; } = new PlatformCapabilityService();

        public IThreadDispatcher ThreadDispatcher { get; } = new ViewThreadDispatcher();

        public IRegistryReadService RegistryReadService
        {
            get => _registryReadService;
            set
            {
                if (_registryReadService != null)
                {
                    throw new InvalidOperationException("Cannot set the service more than once");
                }

                _registryReadService = value;
            }
        }

        public IRegistryWriteService RegistryWriteService
        {
            get => _registryWriteService;
            set
            {
                if (_registryWriteService != null)
                {
                    throw new InvalidOperationException("Cannot set the service more than once");
                }

                _registryWriteService = value;
            }
        }
    }
}
