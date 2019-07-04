// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BackgroundEditorView.xaml.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.Editors.Personalization
{
    using System;
    using ServiceContracts.CommandBridge;
    using ServiceContracts.Commands;
    using Shared.Commands;
    using ViewModels.EditorViewModels.Personalization;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class BackgroundEditorView : UserControl
    {
        public BackgroundEditorView(BackgroundEditorViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public BackgroundEditorViewModel ViewModel { get; }

        private async void TestButton_OnClick(object sender, RoutedEventArgs e)
        {
            ICommandBridgeClientService bridgeService = App.Current.BridgeClientService;
            var command = new RegistryReadIntValueCommand(
                RegistryHive.CurrentUser,
                RegistryKey.Text,
                RegistryValueName.Text,
                0);
            IServiceCommandResponse response = await bridgeService.SendCommandAsync(command);
            TestResult.Text = response.IsError ? response.ErrorMessage : response.Result.ToString();
        }
    }
}
