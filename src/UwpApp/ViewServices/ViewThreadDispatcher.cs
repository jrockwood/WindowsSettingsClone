// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewThreadDispatcher.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.ViewServices
{
    using System;
    using System.Threading.Tasks;
    using ServiceContracts.ViewServices;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;

    /// <summary>
    /// Default implementation of <see cref="IThreadDispatcher"/> that uses the application's main <see cref="CoreDispatcher"/>.
    /// </summary>
    internal class ViewThreadDispatcher : ThreadDispatcher
    {
        public ViewThreadDispatcher()
            : base(RunOnCoreWindowDispatcher)
        {
        }

        public static Task RunOnCoreWindowDispatcher(Action action)
        {
            return CoreApplication.MainView.CoreWindow.Dispatcher
                .RunAsync(CoreDispatcherPriority.Normal, () => action())
                .AsTask();
        }
    }
}
