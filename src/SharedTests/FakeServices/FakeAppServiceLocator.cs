// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeAppServiceLocator.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Shared.Tests.FakeServices
{
    using ServiceContracts.ViewServices;
    using ServiceContracts.Win32Services;
    using ViewServices;

    public class FakeAppServiceLocator : IAppServiceLocator
    {
        public FakeAppServiceLocator()
        {
            var fakeRegistryService = new FakeRegistryService();
            RegistryReadService = fakeRegistryService;
            RegistryWriteService = fakeRegistryService;
        }

        public INavigationViewService NavigationViewService { get; set; }
        public IPlatformCapabilityService PlatformCapabilityService { get; set; }
        public IThreadDispatcher ThreadDispatcher { get; set; } = new UnitTestThreadDispatcher();
        public IRegistryReadService RegistryReadService { get; set; }
        public IRegistryWriteService RegistryWriteService { get; set; }
        public IWin32ApiService Win32ApiService { get; set; }
    }
}
