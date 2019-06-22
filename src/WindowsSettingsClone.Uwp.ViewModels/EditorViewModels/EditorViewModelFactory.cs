// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorViewModelFactory.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.EditorViewModels
{
    using System;
    using Personalization;

    internal static class EditorViewModelFactory
    {
        public static EditorViewModel CreateFromKind(EditorKind kind)
        {
            switch (kind)
            {
                case EditorKind.SystemDisplay:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemSound:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemNotificationsAndActions:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemFocusAssist:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemPowerAndSleep:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemStorage:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemTabletMode:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemMultitasking:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemProjectingToThisPC:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemSharedExperiences:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemClipboard:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemRemoteDesktop:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SystemAbout:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.DevicesBluetoothAndOtherDevices:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.DevicesPrintersAndScanners:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.DevicesMouse:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.DevicesTyping:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.DevicesPenAndWindowsInk:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.DevicesAutoPlay:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.DevicesUsb:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.Phone:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.NetworkAndInternetNetworkStatus:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.NetworkAndInternetEthernet:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.NetworkAndInternetDialUp:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.NetworkAndInternetVpn:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.NetworkAndInternetDataUsage:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.NetworkAndInternetProxy:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PersonalizationBackground:
                    return new BackgroundEditorViewModel();

                case EditorKind.PersonalizationColors:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PersonalizationLockScreen:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PersonalizationThemes:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PersonalizationFonts:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PersonalizationStart:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PersonalizationTaskbar:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AppsAppsAndFeatures:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AppsDefaultApps:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AppsOfflineMaps:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AppsAppsForWebsites:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AppsVideoPlayback:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AppsStartup:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AccountsYourInfo:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AccountsEmailAndAccounts:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AccountsSignInOptions:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AccountsAccessWorkOrSchool:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AccountsFamilyAndOtherUsers:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.AccountsSyncYourSettings:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.TimeAndLanguageDateAndTime:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.TimeAndLanguageRegion:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.TimeAndLanguageLanguage:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.TimeAndLanguageSpeech:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.GamingGameBar:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.GamingCaptures:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.GamingBroadcasting:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.GamingGameMode:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.GamingXboxNetworking:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessDisplay:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessCursorAndPointer:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessMagnifier:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessColorFilters:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessHighContrast:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessNarrator:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessAudio:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessClosedCaptions:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessSpeech:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessKeyboard:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessMouse:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.EaseOfAccessEyeControl:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SearchPermissionsAndHistory:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SearchSearchingWindows:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.SearchMoreDetails:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.CortanaTalkToCortana:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.CortanaPermissions:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.CortanaMoreDetails:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyGeneral:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacySpeech:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyInkingAndTypingPersonalization:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyDiagnosticsAndFeedback:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyActivityHistory:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyLocation:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyCamera:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyMicrophone:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyVoiceActivation:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyNotifications:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyAccountInfo:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyContacts:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyCalendar:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyPhoneCalls:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyCallHistory:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyEmail:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyTasks:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyMessaging:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyRadios:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyOtherDevices:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyBackgroundApps:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyAppDiagnostics:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyAutomaticFileDownloads:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyDocuments:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyPictures:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyVideos:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.PrivacyFileSystem:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.UpdateAndSecurityWindowsUpdate:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.UpdateAndSecurityDeliveryOptimization:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.UpdateAndSecurityWindowsSecurity:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.UpdateAndSecurityBackup:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.UpdateAndSecurityTroubleshoot:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.UpdateAndSecurityRecovery:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.UpdateAndSecurityActivation:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.UpdateAndSecurityFindMyDevice:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.UpdateAndSecurityForDevelopers:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                case EditorKind.UpdateAndSecurityWindowsInsiderProgram:
                    return new NotYetImplementedEditorViewModel(kind, kind.ToString());

                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }
    }
}
