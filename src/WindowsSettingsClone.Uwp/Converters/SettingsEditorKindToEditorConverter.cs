// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsEditorKindToEditorConverter.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.Converters
{
    using System;
    using SettingsEditors.Personalization;
    using ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Converts from a <see cref="SettingsEditorKind"/> to a settings editor user control.
    /// </summary>
    internal class SettingsEditorKindToEditorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var editorKind = (SettingsEditorKind)value;

            switch (editorKind)
            {
                case SettingsEditorKind.SystemDisplay:
                    break;

                case SettingsEditorKind.SystemSound:
                    break;

                case SettingsEditorKind.SystemNotificationsAndActions:
                    break;

                case SettingsEditorKind.SystemFocusAssist:
                    break;

                case SettingsEditorKind.SystemPowerAndSleep:
                    break;

                case SettingsEditorKind.SystemStorage:
                    break;

                case SettingsEditorKind.SystemTabletMode:
                    break;

                case SettingsEditorKind.SystemMultitasking:
                    break;

                case SettingsEditorKind.SystemProjectingToThisPC:
                    break;

                case SettingsEditorKind.SystemSharedExperiences:
                    break;

                case SettingsEditorKind.SystemClipboard:
                    break;

                case SettingsEditorKind.SystemRemoteDesktop:
                    break;

                case SettingsEditorKind.SystemAbout:
                    break;

                case SettingsEditorKind.DevicesBluetoothAndOtherDevices:
                    break;

                case SettingsEditorKind.DevicesPrintersAndScanners:
                    break;

                case SettingsEditorKind.DevicesMouse:
                    break;

                case SettingsEditorKind.DevicesTyping:
                    break;

                case SettingsEditorKind.DevicesPenAndWindowsInk:
                    break;

                case SettingsEditorKind.DevicesAutoPlay:
                    break;

                case SettingsEditorKind.DevicesUsb:
                    break;

                case SettingsEditorKind.Phone:
                    break;

                case SettingsEditorKind.NetworkAndInternetNetworkStatus:
                    break;

                case SettingsEditorKind.NetworkAndInternetEthernet:
                    break;

                case SettingsEditorKind.NetworkAndInternetDialUp:
                    break;

                case SettingsEditorKind.NetworkAndInternetVpn:
                    break;

                case SettingsEditorKind.NetworkAndInternetDataUsage:
                    break;

                case SettingsEditorKind.NetworkAndInternetProxy:
                    break;

                case SettingsEditorKind.PersonalizationBackground:
                    return new BackgroundSettingsEditor();

                case SettingsEditorKind.PersonalizationColors:
                    break;

                case SettingsEditorKind.PersonalizationLockScreen:
                    break;

                case SettingsEditorKind.PersonalizationThemes:
                    break;

                case SettingsEditorKind.PersonalizationFonts:
                    break;

                case SettingsEditorKind.PersonalizationStart:
                    break;

                case SettingsEditorKind.PersonalizationTaskbar:
                    break;

                case SettingsEditorKind.AppsAppsAndFeatures:
                    break;

                case SettingsEditorKind.AppsDefaultApps:
                    break;

                case SettingsEditorKind.AppsOfflineMaps:
                    break;

                case SettingsEditorKind.AppsAppsForWebsites:
                    break;

                case SettingsEditorKind.AppsVideoPlayback:
                    break;

                case SettingsEditorKind.AppsStartup:
                    break;

                case SettingsEditorKind.AccountsYourInfo:
                    break;

                case SettingsEditorKind.AccountsEmailAndAccounts:
                    break;

                case SettingsEditorKind.AccountsSignInOptions:
                    break;

                case SettingsEditorKind.AccountsAccessWorkOrSchool:
                    break;

                case SettingsEditorKind.AccountsFamilyAndOtherUsers:
                    break;

                case SettingsEditorKind.AccountsSyncYourSettings:
                    break;

                case SettingsEditorKind.TimeAndLanguageDateAndTime:
                    break;

                case SettingsEditorKind.TimeAndLanguageRegion:
                    break;

                case SettingsEditorKind.TimeAndLanguageLanguage:
                    break;

                case SettingsEditorKind.TimeAndLanguageSpeech:
                    break;

                case SettingsEditorKind.GamingGameBar:
                    break;

                case SettingsEditorKind.GamingCaptures:
                    break;

                case SettingsEditorKind.GamingBroadcasting:
                    break;

                case SettingsEditorKind.GamingGameMode:
                    break;

                case SettingsEditorKind.GamingXboxNetworking:
                    break;

                case SettingsEditorKind.EaseOfAccessDisplay:
                    break;

                case SettingsEditorKind.EaseOfAccessCursorAndPointer:
                    break;

                case SettingsEditorKind.EaseOfAccessMagnifier:
                    break;

                case SettingsEditorKind.EaseOfAccessColorFilters:
                    break;

                case SettingsEditorKind.EaseOfAccessHighContrast:
                    break;

                case SettingsEditorKind.EaseOfAccessNarrator:
                    break;

                case SettingsEditorKind.EaseOfAccessAudio:
                    break;

                case SettingsEditorKind.EaseOfAccessClosedCaptions:
                    break;

                case SettingsEditorKind.EaseOfAccessSpeech:
                    break;

                case SettingsEditorKind.EaseOfAccessKeyboard:
                    break;

                case SettingsEditorKind.EaseOfAccessMouse:
                    break;

                case SettingsEditorKind.EaseOfAccessEyeControl:
                    break;

                case SettingsEditorKind.SearchPermissionsAndHistory:
                    break;

                case SettingsEditorKind.SearchSearchingWindows:
                    break;

                case SettingsEditorKind.SearchMoreDetails:
                    break;

                case SettingsEditorKind.CortanaTalkToCortana:
                    break;

                case SettingsEditorKind.CortanaPermissions:
                    break;

                case SettingsEditorKind.CortanaMoreDetails:
                    break;

                case SettingsEditorKind.PrivacyGeneral:
                    break;

                case SettingsEditorKind.PrivacySpeech:
                    break;

                case SettingsEditorKind.PrivacyInkingAndTypingPersonalization:
                    break;

                case SettingsEditorKind.PrivacyDiagnosticsAndFeedback:
                    break;

                case SettingsEditorKind.PrivacyActivityHistory:
                    break;

                case SettingsEditorKind.PrivacyLocation:
                    break;

                case SettingsEditorKind.PrivacyCamera:
                    break;

                case SettingsEditorKind.PrivacyMicrophone:
                    break;

                case SettingsEditorKind.PrivacyVoiceActivation:
                    break;

                case SettingsEditorKind.PrivacyNotifications:
                    break;

                case SettingsEditorKind.PrivacyAccountInfo:
                    break;

                case SettingsEditorKind.PrivacyContacts:
                    break;

                case SettingsEditorKind.PrivacyCalendar:
                    break;

                case SettingsEditorKind.PrivacyPhoneCalls:
                    break;

                case SettingsEditorKind.PrivacyCallHistory:
                    break;

                case SettingsEditorKind.PrivacyEmail:
                    break;

                case SettingsEditorKind.PrivacyTasks:
                    break;

                case SettingsEditorKind.PrivacyMessaging:
                    break;

                case SettingsEditorKind.PrivacyRadios:
                    break;

                case SettingsEditorKind.PrivacyOtherDevices:
                    break;

                case SettingsEditorKind.PrivacyBackgroundApps:
                    break;

                case SettingsEditorKind.PrivacyAppDiagnostics:
                    break;

                case SettingsEditorKind.PrivacyAutomaticFileDownloads:
                    break;

                case SettingsEditorKind.PrivacyDocuments:
                    break;

                case SettingsEditorKind.PrivacyPictures:
                    break;

                case SettingsEditorKind.PrivacyVideos:
                    break;

                case SettingsEditorKind.PrivacyFileSystem:
                    break;

                case SettingsEditorKind.UpdateAndSecurityWindowsUpdate:
                    break;

                case SettingsEditorKind.UpdateAndSecurityDeliveryOptimization:
                    break;

                case SettingsEditorKind.UpdateAndSecurityWindowsSecurity:
                    break;

                case SettingsEditorKind.UpdateAndSecurityBackup:
                    break;

                case SettingsEditorKind.UpdateAndSecurityTroubleshoot:
                    break;

                case SettingsEditorKind.UpdateAndSecurityRecovery:
                    break;

                case SettingsEditorKind.UpdateAndSecurityActivation:
                    break;

                case SettingsEditorKind.UpdateAndSecurityFindMyDevice:
                    break;

                case SettingsEditorKind.UpdateAndSecurityForDevelopers:
                    break;

                case SettingsEditorKind.UpdateAndSecurityWindowsInsiderProgram:
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
