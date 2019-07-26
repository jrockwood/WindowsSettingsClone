// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NotYetImplementedEditorViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.EditorViewModels
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ServiceContracts.Commands;
    using ServiceContracts.ViewServices;
    using ServiceContracts.Win32Services;
    using Shared.Logging;

    public class NotYetImplementedEditorViewModel : EditorViewModel
    {
        public NotYetImplementedEditorViewModel(EditorKind editorKind, string displayName)
            : base(
                new NullLogger(),
                new DoNothingAppServiceLocator(),
                new BonusBarViewModel(null))
        {
            EditorKind = editorKind;
            DisplayName = displayName;
        }

        public override EditorKind EditorKind { get; }
        public override string DisplayName { get; }

        protected override Task LoadInternalAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        //// ===========================================================================================================
        //// Classes
        //// ===========================================================================================================

        private sealed class DoNothingAppServiceLocator : IAppServiceLocator
        {
            public INavigationViewService NavigationViewService { get; } = new DoNothingNavigationViewService();

            public IPlatformCapabilityService PlatformCapabilityService { get; } =
                new DoNothingPlatformCapabilityService();

            public IThreadDispatcher ThreadDispatcher { get; } = new DoNothingThreadDispatcher();
            public IRegistryReadService RegistryReadService { get; } = new DoNothingRegistryReadService();
            public IRegistryWriteService RegistryWriteService { get; } = new DoNothingRegistryWriteService();
            public IWin32ApiService Win32ApiService { get; } = new DoNothingWin32ApiService();
        }

        private sealed class DoNothingPlatformCapabilityService : IPlatformCapabilityService
        {
            public bool IsRevealBrushSupported => false;
        }

        private sealed class DoNothingThreadDispatcher : IThreadDispatcher
        {
            public Task RunOnUIThreadAsync(Action action)
            {
                return Task.CompletedTask;
            }

            public Task RunOnUIThreadDelayedAsync(
                Action action,
                int millisecondsDelay,
                CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.CompletedTask;
            }

            public Task RunOnBackgroundThreadAsync(Action action)
            {
                return Task.CompletedTask;
            }

            public Task RunOnBackgroundThreadAsync(Func<Task> actionAsync)
            {
                return Task.CompletedTask;
            }
        }

        private sealed class DoNothingNavigationViewService : INavigationViewService
        {
            public event EventHandler BackStackDepthChange;

            public bool CanGoBack => false;

            public int BackStackDepth
            {
                get
                {
                    // This is only here to avoid the compiler error that this event is not used.
                    BackStackDepthChange?.Invoke(this, EventArgs.Empty);
                    return 0;
                }
            }

            public void GoBack() { }

            public void NavigateTo(Type pageViewModelType, string pageViewModelState) { }
        }

        private sealed class DoNothingRegistryReadService : IRegistryReadService
        {
            public Task<int> ReadValueAsync(RegistryBaseKey baseKey, string key, string valueName, int defaultValue)
            {
                return Task.FromResult(0);
            }

            public Task<bool> ReadValueAsync(RegistryBaseKey baseKey, string key, string valueName, bool defaultValue)
            {
                return Task.FromResult(false);
            }

            public Task<string> ReadValueAsync(RegistryBaseKey baseKey, string key, string valueName, string defaultValue)
            {
                return Task.FromResult(string.Empty);
            }
        }

        private sealed class DoNothingRegistryWriteService : IRegistryWriteService
        {
            public Task WriteValueAsync(RegistryBaseKey baseKey, string key, string valueName, int value)
            {
                return Task.CompletedTask;
            }

            public Task WriteValueAsync(RegistryBaseKey baseKey, string key, string valueName, bool value)
            {
                return Task.CompletedTask;
            }

            public Task WriteValueAsync(RegistryBaseKey baseKey, string key, string valueName, string value)
            {
                return Task.CompletedTask;
            }
        }

        private sealed class DoNothingWin32ApiService : IWin32ApiService
        {
            public Task<string> GetDesktopWallpaperPathAsync()
            {
                return Task.FromResult(string.Empty);
            }
        }
    }
}
