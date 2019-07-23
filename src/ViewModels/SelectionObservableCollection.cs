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

    /// <summary>
    /// Represents an <see cref="ObservableCollection{T}"/> that keeps track of the currently selected item.
    /// </summary>
    /// <typeparam name="T">The type of objects in the collection.</typeparam>
    public class SelectionObservableCollection<T> : ObservableCollection<T> where T : class
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private T _selectedItem;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

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

        //// ===========================================================================================================
        //// Events
        //// ===========================================================================================================

        public event EventHandler SelectedItemChanged;

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(_selectedItem, value))
                {
                    _selectedItem = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedItem)));
                    RaiseSelectedItemChanged();
                }
            }
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        protected virtual void RaiseSelectedItemChanged()
        {
            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Extension methods for the <see cref="SelectionObservableCollection{T}"/> class.
    /// </summary>
    public static class SelectionObservableCollectionExtensions
    {
        /// <summary>
        /// Selects the item that contains the specified value in a collection of <see cref="NamedValue{T}"/> items. If
        /// the item isn't found, the selection doesn't change.
        /// </summary>
        /// <typeparam name="T">The type of item to find.</typeparam>
        /// <param name="collection">The source collection.</param>
        /// <param name="itemToFind">The item to find.</param>
        public static void Select<T>(this SelectionObservableCollection<NamedValue<T>> collection, T itemToFind)
        {
            try
            {
                NamedValue<T> first = collection.First(
                    namedValue => EqualityComparer<T>.Default.Equals(namedValue.Value, itemToFind));
                collection.SelectedItem = first;
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
