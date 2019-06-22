// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NotYetImplementedEditorViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.EditorViewModels
{
    using System.Threading;
    using System.Threading.Tasks;

    public class NotYetImplementedEditorViewModel : EditorViewModel
    {
        public NotYetImplementedEditorViewModel(EditorKind editorKind, string displayName)
            : base(new BonusBarViewModel(null))
        {
            EditorKind = editorKind;
            DisplayName = displayName;
        }

        public override EditorKind EditorKind { get; }
        public override string DisplayName { get; }

        protected override Task LoadInternalAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
