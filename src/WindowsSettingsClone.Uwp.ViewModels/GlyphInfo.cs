// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="GlyphInfo.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    /// <summary>
    /// Contains information about a glyph code and which font its defined in.
    /// </summary>
    public class GlyphInfo
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        public const string SegoeMdl2FontFamilyName = "Segoe MDL2 Assets";
        public const string SettingsMdl2FontFamilyName = "Settings MDL2 Assets";

        public static readonly GlyphInfo AllApps = new GlyphInfo("\uE71D");
        public static readonly GlyphInfo CellPhone = new GlyphInfo("\uE8EA");
        public static readonly GlyphInfo Characters = new GlyphInfo("\uE8C1");
        public static readonly GlyphInfo Connected = new GlyphInfo("\uF0B9");
        public static readonly GlyphInfo Contact = new GlyphInfo("\uE77B");
        public static readonly GlyphInfo Cortana = new GlyphInfo("\uE832", SettingsMdl2FontFamilyName);
        public static readonly GlyphInfo Devices = new GlyphInfo("\uE772");
        public static readonly GlyphInfo DialUp = new GlyphInfo("\uE83C");
        public static readonly GlyphInfo EaseOfAccess = new GlyphInfo("\uE776");
        public static readonly GlyphInfo Error = new GlyphInfo("\uE783");
        public static readonly GlyphInfo Ethernet = new GlyphInfo("\uE839");
        public static readonly GlyphInfo Globe = new GlyphInfo("\uE774");
        public static readonly GlyphInfo HardDrive = new GlyphInfo("\uEDA2");
        public static readonly GlyphInfo Home = new GlyphInfo("\uE80F");
        public static readonly GlyphInfo Info = new GlyphInfo("\uE946");
        public static readonly GlyphInfo KeyboardClassic = new GlyphInfo("\uE765");
        public static readonly GlyphInfo Lock = new GlyphInfo("\uE72E");
        public static readonly GlyphInfo Message = new GlyphInfo("\uE8BD");
        public static readonly GlyphInfo Mouse = new GlyphInfo("\uE962");
        public static readonly GlyphInfo MyNetwork = new GlyphInfo("\uEC27");
        public static readonly GlyphInfo TaskView = new GlyphInfo("\uE7C4");
        public static readonly GlyphInfo QuietHours = new GlyphInfo("\uE708");
        public static readonly GlyphInfo Paste = new GlyphInfo("\uE77F");
        public static readonly GlyphInfo PenWorkspace = new GlyphInfo("\uEDC6");
        public static readonly GlyphInfo Personalize = new GlyphInfo("\uE771");
        public static readonly GlyphInfo PieSingle = new GlyphInfo("\uEB05");
        public static readonly GlyphInfo PlaybackRate1x = new GlyphInfo("\uEC57");
        public static readonly GlyphInfo PowerButton = new GlyphInfo("\uE7E8");
        public static readonly GlyphInfo Print = new GlyphInfo("\uE749");
        public static readonly GlyphInfo Project = new GlyphInfo("\uEBC6");
        public static readonly GlyphInfo Remote = new GlyphInfo("\uE8AF");
        public static readonly GlyphInfo Search = new GlyphInfo("\uE721");
        public static readonly GlyphInfo Sync = new GlyphInfo("\uE895");
        public static readonly GlyphInfo System = new GlyphInfo("\uE770");
        public static readonly GlyphInfo TabletMode = new GlyphInfo("\uEBFC");
        public static readonly GlyphInfo TimeLanguage = new GlyphInfo("\uE775");
        public static readonly GlyphInfo TvMonitor = new GlyphInfo("\uE7F4");
        public static readonly GlyphInfo Usb = new GlyphInfo("\uE88E");
        public static readonly GlyphInfo Volume = new GlyphInfo("\uE767");
        public static readonly GlyphInfo Vpn = new GlyphInfo("\uE705");
        public static readonly GlyphInfo XboxLogo = new GlyphInfo("\uF20B", SettingsMdl2FontFamilyName);

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private GlyphInfo(string glyph, string fontFamilyName = SegoeMdl2FontFamilyName)
        {
            Glyph = glyph;
            FontFamilyName = fontFamilyName;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public string Glyph { get; }
        public string FontFamilyName { get; }
    }
}
