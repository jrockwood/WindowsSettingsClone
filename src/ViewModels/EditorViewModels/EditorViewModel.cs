// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.EditorViewModels
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ServiceContracts.FullTrust;
    using ServiceContracts.Logging;
    using ServiceContracts.ViewServices;
    using Shared.Diagnostics;
    using Shared.Logging;
    using Shared.Threading;

    /// <summary>
    /// Abstract base class describing all settings editors.
    /// </summary>
    public abstract class EditorViewModel : BaseViewModel
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        protected const int DefaultPropertyUpdateTimeoutMs = 1000;

        private bool _isContentReady;
        private bool _isIndeterminateProgressBarVisible;
        private string _updateErrorMessage;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        protected EditorViewModel(
            ILogger logger,
            IThreadDispatcher threadDispatcher,
            IRegistryWriteService registryWriteService,
            BonusBarViewModel bonusBar)
        {
            Logger = Param.VerifyNotNull(logger, nameof(logger));
            ThreadDispatcher = Param.VerifyNotNull(threadDispatcher, nameof(threadDispatcher));
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

        public string UpdateErrorMessage
        {
            get => _updateErrorMessage;
            set => SetProperty(ref _updateErrorMessage, value);
        }

        public bool HasUpdateErrorMessage => !string.IsNullOrEmpty(UpdateErrorMessage);

        protected bool IsLoading { get; private set; }

        protected ILogger Logger { get; }
        protected IRegistryWriteService RegistryWriteService { get; }
        protected IThreadDispatcher ThreadDispatcher { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public async Task LoadAsync(
            IRegistryReadService registryReadService,
            int showProgressBarDelayMilliseconds = 500,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            IsLoading = true;

            // Start a timer to trigger the progress bar visibility if the content still hasn't loaded within the
            // specified delay.
            var progressBarCancellationSource = new CancellationTokenSource();
            CancellationToken progressBarCancellationToken = progressBarCancellationSource.Token;
            Task progressTask = ThreadDispatcher.RunOnUIThreadDelayedAsync(
                () => IsIndeterminateProgressBarVisible = true,
                showProgressBarDelayMilliseconds,
                progressBarCancellationToken);

            // Start loading on a background thread.
            await ThreadDispatcher.RunOnBackgroundThreadAsync(
                () => LoadInternalAsync(registryReadService, cancellationToken));

            // Cancel the progress bar timer if it hasn't been set yet.
            progressBarCancellationSource.Cancel();

            // Set the progress bar and content ready flags.
            await ThreadDispatcher.RunOnUIThreadAsync(
                () =>
                {
                    IsIndeterminateProgressBarVisible = false;
                    IsContentReady = true;
                    IsLoading = false;
                });
        }

        protected abstract Task LoadInternalAsync(
            IRegistryReadService registryReadService,
            CancellationToken cancellationToken);

        /// <summary>
        /// Sets the specified property value. If the property value changed, the <see
        /// cref="INotifyPropertyChanged.PropertyChanged"/> event is raised an an async update is kicked off. If there
        /// was an error during the async update, the <see cref="UpdateErrorMessage"/> is set with the error.
        /// </summary>
        /// <typeparam name="T">The type of the field to change.</typeparam>
        /// <param name="field">A reference to the field to change.</param>
        /// <param name="value">The value to change to.</param>
        /// <param name="asyncUpdateFunc">The function that is run to update the model.</param>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait for the update before it times out.</param>
        /// <param name="propertyName">
        /// The name of the property that is changing and that will be specified in the <see
        /// cref="INotifyPropertyChanged.PropertyChanged"/> event.
        /// </param>
        /// <returns>True if the property was changed; otherwise, false.</returns>
        protected bool SetPropertyAndPerformAsyncUpdate<T>(
            ref T field,
            T value,
            Func<Task> asyncUpdateFunc,
            int millisecondsTimeout = DefaultPropertyUpdateTimeoutMs,
            [CallerMemberName] string propertyName = null)
        {
            if (!SetProperty(ref field, value, propertyName))
            {
                return false;
            }

            // Don't invoke the update function if we're still loading.
            if (IsLoading)
            {
                return true;
            }

            PerformUpdateForPropertyAsync(asyncUpdateFunc, propertyName, millisecondsTimeout);

            return true;
        }

        protected async void PerformUpdateForPropertyAsync(
            Func<Task> updateTask,
            string propertyName,
            int millisecondsTimeout = DefaultPropertyUpdateTimeoutMs)
        {
            bool wasError = false;

            try
            {
                await updateTask().TimeoutAfter(millisecondsTimeout);
            }
            catch (TimeoutException)
            {
                Logger.LogWarning("Timeout occurred while updating {0}", propertyName);
                wasError = true;
            }
            catch (AggregateException e)
            {
                StringBuilder builder = new StringBuilder("An AggregateException occurred with the following message: ")
                    .AppendLine(e.Message)
                    .AppendLine("  InnerExceptions:");

                foreach (Exception innerException in e.InnerExceptions)
                {
                    builder.Append("    ").AppendLine(innerException.ToString());
                }

                Logger.LogWarning(builder.ToString());
                wasError = true;
            }

            if (wasError)
            {
                await ThreadDispatcher.RunOnUIThreadAsync(
                    () => UpdateErrorMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        Strings.EditorUpdateErrorMessage,
                        propertyName));
            }
        }
    }
}
