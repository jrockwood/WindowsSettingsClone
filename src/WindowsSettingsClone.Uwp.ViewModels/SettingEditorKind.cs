// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingEditorKind.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    /// <summary>
    /// Enumerates the different types of settings editors.
    /// </summary>
    public enum SettingEditorKind
    {
        #region System

        Display,
        Sound,
        NotificationsAndActions,
        FocusAssist,
        PowerAndSleep,
        Storage,
        TabletMode,
        Multitasking,
        ProjectingToThisPC,
        SharedExperiences,
        Clipboard,
        RemoteDesktop,
        About,

        #endregion System

        #region Devices

        BluetoothAndOtherDevices,
        PrintersAndScanners,
        Mouse,
        Typing,
        PenAndWindowsInk,
        AutoPlay,
        Usb,

        #endregion Devices

        #region Phone

        Phone,

        #endregion Phone

        #region Network & Internet

        NetworkStatus,
        Ethernet,
        DialUp,
        Vpn,
        DataUsage,
        Proxy,

        #endregion Network & Internet

        #region Personalization

        Background,
        Colors,
        LockScreen,
        Themes,
        Fonts,
        Start,
        Taskbar,

        #endregion Personalization

        #region Apps

        AppsAndFeatures,
        DefaultApps,
        OfflineMaps,
        AppsForWebsites,
        VideoPlayback,
        Startup,

        #endregion Apps

        #region Accounts

        YourInfo,
        EmailAndAccounts,
        SignInOptions,
        AccessWorkOrSchool,
        FamilyAndOtherUsers,
        SyncYourSettings,

        #endregion Accounts

        #region Time & Language

        DateAndTime,
        Region,
        Language,
        Speech,

        #endregion Time & Language

        #region Gaming

        GameBar,
        Captures,
        Broadcasting,
        GameMode,
        XboxNetworking,

        #endregion Gaming
    }
}
