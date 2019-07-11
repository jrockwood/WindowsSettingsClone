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
