// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="RelayCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.Utility
{
    using System;
    using System.Windows.Input;
    using Shared.Utility;

    /// <summary>
    /// Implements an <see cref="ICommand"/> interface by calling delegates specified in the constructor.
    /// </summary>
    internal class RelayCommand<T> : ICommand<T>
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private bool _enabled;
        private readonly Action<T> _executeAction;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public RelayCommand(Action<T> executeAction, bool enabled = true)
        {
            _executeAction = Param.VerifyNotNull(executeAction, nameof(executeAction));
            _enabled = enabled;
        }

        //// ===========================================================================================================
        //// Events
        //// ===========================================================================================================

        public event EventHandler CanExecuteChanged;

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        bool ICommand.CanExecute(object parameter)
        {
            return _enabled;
        }

        void ICommand.Execute(object parameter)
        {
            _executeAction((T)parameter);
        }

        public bool CanExecute(T parameter)
        {
            return _enabled;
        }

        public void Execute(T parameter)
        {
            _executeAction(parameter);
        }
    }

    /// <summary>
    /// Implements an <see cref="ICommand"/> interface by calling delegates specified in the constructor.
    /// </summary>
    internal class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action executeAction, bool enabled = true)
            : base(_ => executeAction(), enabled)
        {
        }
    }
}
