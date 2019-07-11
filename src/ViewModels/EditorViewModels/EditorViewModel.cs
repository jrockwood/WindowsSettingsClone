// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.EditorViewModels
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using ServiceContracts.FullTrust;
    using ServiceContracts.ViewServices;
    using Shared.Utility;

    /// <summary>
    /// Abstract base class describing all settings editors.
    /// </summary>
    public abstract class EditorViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private bool _isContentReady;
        private bool _isIndeterminateProgressBarVisible;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        protected EditorViewModel(IRegistryWriteService registryWriteService, BonusBarViewModel bonusBar)
        {
            RegistryWriteService = Param.VerifyNotNull(registryWriteService, nameof(registryWriteService));
            BonusBar = Param.VerifyNotNull(bonusBar, nameof(bonusBar));
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public BonusBarViewModel BonusBar { get; }

        public abstract EditorKind EditorKind { get; }
        public abstract string DisplayName { get; }

        public bool IsContentReady
        {
            get => _isContentReady;
            set => SetProperty(ref _isContentReady, value);
        }

        public bool IsIndeterminateProgressBarVisible
        {
            get => _isIndeterminateProgressBarVisible;
            set => SetProperty(ref _isIndeterminateProgressBarVisible, value);
        }

        protected IRegistryWriteService RegistryWriteService { get; }

        protected bool IsLoading { get; private set; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        protected bool SetPropertyAndWaitForAsyncUpdate<T>(
            ref T field,
            T value,
            Func<Task> asyncUpdateFunc,
            int millisecondsTimeout = 1000,
            [CallerMemberName] string propertyName = null)
        {
            T currentValue = field;

            if (!SetProperty(ref field, value, propertyName))
            {
                return false;
            }

            // Don't invoke the update function if we're still loading.
            if (IsLoading)
            {
                return true;
            }

            bool revert;

            try
            {
                bool completed = asyncUpdateFunc.Invoke().Wait(millisecondsTimeout);
                //revert = !completed;
                revert = false;
            }
            catch
            {
                revert = true;
            }

            if (revert)
            {
                SetProperty(ref field, currentValue, propertyName);
                return false;
            }

            return true;
        }

        public async Task LoadAsync(
            IThreadDispatcher threadDispatcher,
            IRegistryReadService registryReadService,
            int showProgressBarDelayMilliseconds = 500,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (threadDispatcher == null)
            {
                throw new ArgumentNullException(nameof(threadDispatcher));
            }

            IsLoading = true;

            // Start a timer to trigger the progress bar visibility if the content still hasn't loaded within the
            // specified delay.
            var progressBarCancellationSource = new CancellationTokenSource();
            CancellationToken progressBarCancellationToken = progressBarCancellationSource.Token;
            Task progressTask = threadDispatcher.RunOnUIThreadDelayedAsync(
                () => IsIndeterminateProgressBarVisible = true,
                showProgressBarDelayMilliseconds,
                progressBarCancellationToken);

            // Start loading on a background thread.
            await threadDispatcher.RunOnBackgroundThreadAsync(
                () => LoadInternalAsync(registryReadService, cancellationToken));

            // Cancel the progress bar timer if it hasn't been set yet.
            progressBarCancellationSource.Cancel();

            // Set the progress bar and content ready flags.
            await threadDispatcher.RunOnUIThreadAsync(
                () =>
                {
                    IsIndeterminateProgressBarVisible = false;
                    IsContentReady = true;
                });

            IsLoading = false;
        }

        protected abstract Task LoadInternalAsync(
            IRegistryReadService registryReadService,
            CancellationToken cancellationToken);
    }
}
