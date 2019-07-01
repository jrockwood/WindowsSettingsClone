// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="NotYetImplementedEditorViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.EditorViewModels
{
    using System.Threading;
    using System.Threading.Tasks;
    using ServiceContracts.FullTrust;

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

        protected override Task LoadInternalAsync(
            IRegistryReadService registryReadService,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
