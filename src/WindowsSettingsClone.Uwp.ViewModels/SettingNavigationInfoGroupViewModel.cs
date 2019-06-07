// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingNavigationInfoGroupViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a group of <see cref="SettingNavigationInfoViewModel"/> when the items are grouped.
    /// </summary>
    public sealed class SettingNavigationInfoGroupViewModel : IGrouping<string, SettingNavigationInfoViewModel>
    {
        private readonly List<SettingNavigationInfoViewModel> _items;

        public SettingNavigationInfoGroupViewModel(string groupName, IEnumerable<SettingNavigationInfoViewModel> items)
        {
            Key = groupName;
            _items = items.ToList();
        }

        public string Key { get; }
        public string GroupName => Key;

        public IEnumerator<SettingNavigationInfoViewModel> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
