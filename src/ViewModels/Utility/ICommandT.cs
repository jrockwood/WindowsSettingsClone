// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandT.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.Utility
{
    using System.Windows.Input;

    /// <summary>
    /// Type-safe command for use in ViewModels.
    /// </summary>
    /// <typeparam name="T">The type of the parameter that is passed into the methods.</typeparam>
    public interface ICommand<in T> : ICommand
    {
        bool CanExecute(T parameter);

        void Execute(T parameter);
    }
}
