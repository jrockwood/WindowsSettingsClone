// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorViewModelToEditorViewConverter.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.UwpApp.Converters
{
    using System;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Data;
    using Editors.Personalization;
    using ViewModels;
    using ViewModels.EditorViewModels;
    using ViewModels.EditorViewModels.Personalization;

    /// <summary>
    /// Converts from a <see cref="EditorKind"/> to a settings editor view UserControl.
    /// </summary>
    internal class EditorViewModelToEditorViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var viewModel = (EditorViewModel)value;
            EditorKind editorKind = viewModel.EditorKind;

            switch (editorKind)
            {
                case EditorKind.SystemDisplay:
                    break;

                case EditorKind.SystemSound:
                    break;

                case EditorKind.SystemNotificationsAndActions:
                    break;

                case EditorKind.SystemFocusAssist:
                    break;

                case EditorKind.SystemPowerAndSleep:
                    break;

                case EditorKind.SystemStorage:
                    break;

                case EditorKind.SystemTabletMode:
                    break;

                case EditorKind.SystemMultitasking:
                    break;

                case EditorKind.SystemProjectingToThisPC:
                    break;

                case EditorKind.SystemSharedExperiences:
                    break;

                case EditorKind.SystemClipboard:
                    break;

                case EditorKind.SystemRemoteDesktop:
                    break;

                case EditorKind.SystemAbout:
                    break;

                case EditorKind.DevicesBluetoothAndOtherDevices:
                    break;

                case EditorKind.DevicesPrintersAndScanners:
                    break;

                case EditorKind.DevicesMouse:
                    break;

                case EditorKind.DevicesTyping:
                    break;

                case EditorKind.DevicesPenAndWindowsInk:
                    break;

                case EditorKind.DevicesAutoPlay:
                    break;

                case EditorKind.DevicesUsb:
                    break;

                case EditorKind.Phone:
                    break;

                case EditorKind.NetworkAndInternetNetworkStatus:
                    break;

                case EditorKind.NetworkAndInternetEthernet:
                    break;

                case EditorKind.NetworkAndInternetDialUp:
                    break;

                case EditorKind.NetworkAndInternetVpn:
                    break;

                case EditorKind.NetworkAndInternetDataUsage:
                    break;

                case EditorKind.NetworkAndInternetProxy:
                    break;

                case EditorKind.PersonalizationBackground:
                    return new BackgroundEditorView((BackgroundEditorViewModel)viewModel);

                case EditorKind.PersonalizationColors:
                    break;

                case EditorKind.PersonalizationLockScreen:
                    break;

                case EditorKind.PersonalizationThemes:
                    break;

                case EditorKind.PersonalizationFonts:
                    break;

                case EditorKind.PersonalizationStart:
                    break;

                case EditorKind.PersonalizationTaskbar:
                    break;

                case EditorKind.AppsAppsAndFeatures:
                    break;

                case EditorKind.AppsDefaultApps:
                    break;

                case EditorKind.AppsOfflineMaps:
                    break;

                case EditorKind.AppsAppsForWebsites:
                    break;

                case EditorKind.AppsVideoPlayback:
                    break;

                case EditorKind.AppsStartup:
                    break;

                case EditorKind.AccountsYourInfo:
                    break;

                case EditorKind.AccountsEmailAndAccounts:
                    break;

                case EditorKind.AccountsSignInOptions:
                    break;

                case EditorKind.AccountsAccessWorkOrSchool:
                    break;

                case EditorKind.AccountsFamilyAndOtherUsers:
                    break;

                case EditorKind.AccountsSyncYourSettings:
                    break;

                case EditorKind.TimeAndLanguageDateAndTime:
                    break;

                case EditorKind.TimeAndLanguageRegion:
                    break;

                case EditorKind.TimeAndLanguageLanguage:
                    break;

                case EditorKind.TimeAndLanguageSpeech:
                    break;

                case EditorKind.GamingGameBar:
                    break;

                case EditorKind.GamingCaptures:
                    break;

                case EditorKind.GamingBroadcasting:
                    break;

                case EditorKind.GamingGameMode:
                    break;

                case EditorKind.GamingXboxNetworking:
                    break;

                case EditorKind.EaseOfAccessDisplay:
                    break;

                case EditorKind.EaseOfAccessCursorAndPointer:
                    break;

                case EditorKind.EaseOfAccessMagnifier:
                    break;

                case EditorKind.EaseOfAccessColorFilters:
                    break;

                case EditorKind.EaseOfAccessHighContrast:
                    break;

                case EditorKind.EaseOfAccessNarrator:
                    break;

                case EditorKind.EaseOfAccessAudio:
                    break;

                case EditorKind.EaseOfAccessClosedCaptions:
                    break;

                case EditorKind.EaseOfAccessSpeech:
                    break;

                case EditorKind.EaseOfAccessKeyboard:
                    break;

                case EditorKind.EaseOfAccessMouse:
                    break;

                case EditorKind.EaseOfAccessEyeControl:
                    break;

                case EditorKind.SearchPermissionsAndHistory:
                    break;

                case EditorKind.SearchSearchingWindows:
                    break;

                case EditorKind.SearchMoreDetails:
                    break;

                case EditorKind.CortanaTalkToCortana:
                    break;

                case EditorKind.CortanaPermissions:
                    break;

                case EditorKind.CortanaMoreDetails:
                    break;

                case EditorKind.PrivacyGeneral:
                    break;

                case EditorKind.PrivacySpeech:
                    break;

                case EditorKind.PrivacyInkingAndTypingPersonalization:
                    break;

                case EditorKind.PrivacyDiagnosticsAndFeedback:
                    break;

                case EditorKind.PrivacyActivityHistory:
                    break;

                case EditorKind.PrivacyLocation:
                    break;

                case EditorKind.PrivacyCamera:
                    break;

                case EditorKind.PrivacyMicrophone:
                    break;

                case EditorKind.PrivacyVoiceActivation:
                    break;

                case EditorKind.PrivacyNotifications:
                    break;

                case EditorKind.PrivacyAccountInfo:
                    break;

                case EditorKind.PrivacyContacts:
                    break;

                case EditorKind.PrivacyCalendar:
                    break;

                case EditorKind.PrivacyPhoneCalls:
                    break;

                case EditorKind.PrivacyCallHistory:
                    break;

                case EditorKind.PrivacyEmail:
                    break;

                case EditorKind.PrivacyTasks:
                    break;

                case EditorKind.PrivacyMessaging:
                    break;

                case EditorKind.PrivacyRadios:
                    break;

                case EditorKind.PrivacyOtherDevices:
                    break;

                case EditorKind.PrivacyBackgroundApps:
                    break;

                case EditorKind.PrivacyAppDiagnostics:
                    break;

                case EditorKind.PrivacyAutomaticFileDownloads:
                    break;

                case EditorKind.PrivacyDocuments:
                    break;

                case EditorKind.PrivacyPictures:
                    break;

                case EditorKind.PrivacyVideos:
                    break;

                case EditorKind.PrivacyFileSystem:
                    break;

                case EditorKind.UpdateAndSecurityWindowsUpdate:
                    break;

                case EditorKind.UpdateAndSecurityDeliveryOptimization:
                    break;

                case EditorKind.UpdateAndSecurityWindowsSecurity:
                    break;

                case EditorKind.UpdateAndSecurityBackup:
                    break;

                case EditorKind.UpdateAndSecurityTroubleshoot:
                    break;

                case EditorKind.UpdateAndSecurityRecovery:
                    break;

                case EditorKind.UpdateAndSecurityActivation:
                    break;

                case EditorKind.UpdateAndSecurityFindMyDevice:
                    break;

                case EditorKind.UpdateAndSecurityForDevelopers:
                    break;

                case EditorKind.UpdateAndSecurityWindowsInsiderProgram:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new TextBlock { Text = editorKind.ToString() };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) =>
            throw new NotImplementedException();
    }
}
