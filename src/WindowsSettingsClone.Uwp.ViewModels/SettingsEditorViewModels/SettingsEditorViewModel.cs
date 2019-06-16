// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsEditorViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.SettingsEditorViewModels
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewServices;

    /// <summary>
    /// Abstract base class describing all settings editors.
    /// </summary>
    public abstract class SettingsEditorViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private bool _isContentReady;
        private bool _isIndeterminateProgressBarVisible;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        protected SettingsEditorViewModel(BonusBarViewModel bonusBar) =>
            BonusBar = bonusBar ?? throw new ArgumentNullException(nameof(bonusBar));

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public BonusBarViewModel BonusBar { get; }

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

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public async Task LoadAsync(
            IThreadDispatcher threadDispatcher,
            int showProgressBarDelayMilliseconds = 500,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (threadDispatcher == null)
            {
                throw new ArgumentNullException(nameof(threadDispatcher));
            }

            // Start a timer to trigger the progress bar visibility if the content still hasn't loaded within the
            // specified delay.
            var progressBarCancellationSource = new CancellationTokenSource();
            CancellationToken progressBarCancellationToken = progressBarCancellationSource.Token;
            Task progressTask = threadDispatcher.RunOnUIThreadDelayedAsync(
                () => IsIndeterminateProgressBarVisible = true,
                showProgressBarDelayMilliseconds,
                progressBarCancellationToken);

            // Start loading on a background thread.
            await threadDispatcher.RunOnBackgroundThreadAsync(() => LoadInternalAsync(cancellationToken));

            // Cancel the progress bar timer if it hasn't been set yet.
            progressBarCancellationSource.Cancel();

            // Set the progress bar and content ready flags.
            await threadDispatcher.RunOnUIThreadAsync(
                () =>
                {
                    IsIndeterminateProgressBarVisible = false;
                    IsContentReady = true;
                });
        }

        protected abstract Task LoadInternalAsync(CancellationToken cancellationToken);
    }
}
