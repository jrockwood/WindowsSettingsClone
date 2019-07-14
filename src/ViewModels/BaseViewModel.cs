// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Abstract base class for a ViewModel.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        //// ===========================================================================================================
        //// Events
        //// ===========================================================================================================

        public event PropertyChangedEventHandler PropertyChanged;

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Sets the specified property value. If the property value changed, the <see
        /// cref="INotifyPropertyChanged.PropertyChanged"/> event is raised.
        /// </summary>
        /// <typeparam name="T">The type of the field to change.</typeparam>
        /// <param name="field">A reference to the field to change.</param>
        /// <param name="value">The value to change to.</param>
        /// <param name="propertyName">
        /// The name of the property that is changing and that will be specified in the <see
        /// cref="INotifyPropertyChanged.PropertyChanged"/> event.
        /// </param>
        /// <returns>True if the property was changed; otherwise, false.</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }

            return false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
