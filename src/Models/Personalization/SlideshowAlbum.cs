// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SlideshowAlbum.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Models.Personalization
{
    public class SlideshowAlbum
    {
        public SlideshowAlbum(string displayName, string folderPath)
        {
            DisplayName = displayName;
            FolderPath = folderPath;
        }

        public string DisplayName { get; }
        public string FolderPath { get; }
    }
}
