// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectionObservableCollection.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    public class SelectionObservableCollection<T> : ObservableCollection<T>
    {
        private T _selectedItem;

        public SelectionObservableCollection()
        {
        }

        public SelectionObservableCollection(IEnumerable<T> collection, int selectedIndex = -1)
            : base(collection)
        {
            if (selectedIndex >= 0)
            {
                SelectedItem = Items[selectedIndex];
            }
        }

        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(_selectedItem, value))
                {
                    _selectedItem = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedItem)));
                }
            }
        }
    }

    public static class SelectionObservableCollectionExtensions
    {
        public static void Select<T>(this SelectionObservableCollection<NamedValue<T>> collection, T itemToFind)
        {
            try
            {
                NamedValue<T> first = collection.First(namedValue => EqualityComparer<T>.Default.Equals(namedValue.Value));
                collection.SelectedItem = first;
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
