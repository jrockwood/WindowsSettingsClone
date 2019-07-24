// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="AppServiceLocator.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.ViewServices
{
    using System;
    using System.Runtime.CompilerServices;
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
            set => SetOnlyOnce(ref _registryReadService, value);
        }

        public IRegistryWriteService RegistryWriteService
        {
            get => _registryWriteService;
            set => SetOnlyOnce(ref _registryWriteService, value);
        }

        private static void SetOnlyOnce<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
            where T : class
        {
            if (field != null)
            {
                throw new InvalidOperationException($"Cannot set {propertyName} more than once");
            }

            field = value;
        }
    }
}
