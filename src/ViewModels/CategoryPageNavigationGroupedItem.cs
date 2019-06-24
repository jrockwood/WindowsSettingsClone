// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryPageNavigationGroupedItem.cs" company="Justin Rockwood">
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
    /// Represents a group of <see cref="CategoryPageNavigationItem"/> when the items are grouped.
    /// </summary>
    public sealed class
        CategoryPageNavigationGroupedItem : IGrouping<string, CategoryPageNavigationItem>
    {
        private readonly List<CategoryPageNavigationItem> _items;

        public CategoryPageNavigationGroupedItem(
            string groupName,
            IEnumerable<CategoryPageNavigationItem> items)
        {
            Key = groupName;
            _items = items.ToList();
        }

        public string Key { get; }
        public string GroupName => Key;

        public IEnumerator<CategoryPageNavigationItem> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
