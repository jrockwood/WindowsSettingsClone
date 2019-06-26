// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BackgroundEditorView.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.Editors.Personalization
{
    using System;
    using ViewModels.EditorViewModels.Personalization;
    using Windows.ApplicationModel.AppService;
    using Windows.ApplicationModel.Core;
    using Windows.Foundation.Collections;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Controls;

    public sealed partial class BackgroundEditorView : UserControl
    {
        public BackgroundEditorView(BackgroundEditorViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public BackgroundEditorViewModel ViewModel { get; }

        private async void TestButton_OnClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var command = new ValueSet
            {
                ["CommandName"] = "ReadRegistryKey",
                ["RegistryHive"] = "CurrentUser",
                ["RegistryView"] = "Registry64",
                ["RegistryPath"] = @"Control Panel\Personalization\Desktop Slideshow",
                ["RegistryValueName"] = "Shuffle",
                ["DefaultValue"] = false,
                ["RegistryValueOptions"] = "None",
            };

            AppServiceResponse response = await App.Current.Connection.SendMessageAsync(command);
            if (response.Status != AppServiceResponseStatus.Success)
            {
                return;
            }

            ValueSet responseMessage = response.Message;
            // ReSharper disable once SuggestVarOrType_Elsewhere
            int? result = responseMessage["CommandResult"] as int?;

            await CoreApplication.MainView.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    TestResult.Text = result.HasValue ? result.ToString() : "Unknown result";
                });
        }
    }
}
