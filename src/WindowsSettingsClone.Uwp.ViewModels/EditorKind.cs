// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorKind.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    /// <summary>
    /// Enumerates the different types of settings editors.
    /// </summary>
    public enum EditorKind
    {
        SystemDisplay,
        SystemSound,
        SystemNotificationsAndActions,
        SystemFocusAssist,
        SystemPowerAndSleep,
        SystemStorage,
        SystemTabletMode,
        SystemMultitasking,
        SystemProjectingToThisPC,
        SystemSharedExperiences,
        SystemClipboard,
        SystemRemoteDesktop,
        SystemAbout,

        DevicesBluetoothAndOtherDevices,
        DevicesPrintersAndScanners,
        DevicesMouse,
        DevicesTyping,
        DevicesPenAndWindowsInk,
        DevicesAutoPlay,
        DevicesUsb,

        Phone,

        NetworkAndInternetNetworkStatus,
        NetworkAndInternetEthernet,
        NetworkAndInternetDialUp,
        NetworkAndInternetVpn,
        NetworkAndInternetDataUsage,
        NetworkAndInternetProxy,

        PersonalizationBackground,
        PersonalizationColors,
        PersonalizationLockScreen,
        PersonalizationThemes,
        PersonalizationFonts,
        PersonalizationStart,
        PersonalizationTaskbar,

        AppsAppsAndFeatures,
        AppsDefaultApps,
        AppsOfflineMaps,
        AppsAppsForWebsites,
        AppsVideoPlayback,
        AppsStartup,

        AccountsYourInfo,
        AccountsEmailAndAccounts,
        AccountsSignInOptions,
        AccountsAccessWorkOrSchool,
        AccountsFamilyAndOtherUsers,
        AccountsSyncYourSettings,

        TimeAndLanguageDateAndTime,
        TimeAndLanguageRegion,
        TimeAndLanguageLanguage,
        TimeAndLanguageSpeech,

        GamingGameBar,
        GamingCaptures,
        GamingBroadcasting,
        GamingGameMode,
        GamingXboxNetworking,

        EaseOfAccessDisplay,
        EaseOfAccessCursorAndPointer,
        EaseOfAccessMagnifier,
        EaseOfAccessColorFilters,
        EaseOfAccessHighContrast,
        EaseOfAccessNarrator,
        EaseOfAccessAudio,
        EaseOfAccessClosedCaptions,
        EaseOfAccessSpeech,
        EaseOfAccessKeyboard,
        EaseOfAccessMouse,
        EaseOfAccessEyeControl,

        SearchPermissionsAndHistory,
        SearchSearchingWindows,
        SearchMoreDetails,

        CortanaTalkToCortana,
        CortanaPermissions,
        CortanaMoreDetails,

        PrivacyGeneral,
        PrivacySpeech,
        PrivacyInkingAndTypingPersonalization,
        PrivacyDiagnosticsAndFeedback,
        PrivacyActivityHistory,
        PrivacyLocation,
        PrivacyCamera,
        PrivacyMicrophone,
        PrivacyVoiceActivation,
        PrivacyNotifications,
        PrivacyAccountInfo,
        PrivacyContacts,
        PrivacyCalendar,
        PrivacyPhoneCalls,
        PrivacyCallHistory,
        PrivacyEmail,
        PrivacyTasks,
        PrivacyMessaging,
        PrivacyRadios,
        PrivacyOtherDevices,
        PrivacyBackgroundApps,
        PrivacyAppDiagnostics,
        PrivacyAutomaticFileDownloads,
        PrivacyDocuments,
        PrivacyPictures,
        PrivacyVideos,
        PrivacyFileSystem,

        UpdateAndSecurityWindowsUpdate,
        UpdateAndSecurityDeliveryOptimization,
        UpdateAndSecurityWindowsSecurity,
        UpdateAndSecurityBackup,
        UpdateAndSecurityTroubleshoot,
        UpdateAndSecurityRecovery,
        UpdateAndSecurityActivation,
        UpdateAndSecurityFindMyDevice,
        UpdateAndSecurityForDevelopers,
        UpdateAndSecurityWindowsInsiderProgram,
    }
}
