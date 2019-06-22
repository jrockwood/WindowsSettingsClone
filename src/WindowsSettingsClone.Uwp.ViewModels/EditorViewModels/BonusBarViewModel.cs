// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BonusBarViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.EditorViewModels
{
    using System.Collections.Generic;
    using Utility;

    /// <summary>
    /// ViewModel for the bonus bar, which is the list of related settings, questions, and other content on the right
    /// pane within a category page.
    /// </summary>
    public class BonusBarViewModel : BaseViewModel
    {
        public BonusBarViewModel(params BonusBarSection[] sections)
        {
            Sections = new List<BonusBarSection>(sections).AsReadOnly();
        }

        public IReadOnlyCollection<BonusBarSection> Sections { get; }
    }

    public class BonusBarSection : BaseViewModel
    {
        public BonusBarSection(string headerDisplayName, params BonusBarItem[] items)
        {
            HeaderDisplayName = Param.VerifyString(headerDisplayName, nameof(headerDisplayName));
            Items = new List<BonusBarItem>(items).AsReadOnly();
        }

        public string HeaderDisplayName { get; }
        public IReadOnlyList<BonusBarItem> Items { get; }
    }

    public enum BonusBarContentKind
    {
        /// <summary>
        /// A textual description that usually gives more details about a setting. For example, the "Sleep better"
        /// section in System/Display.
        /// </summary>
        Description,

        /// <summary>
        /// A link to another setting page within the application.
        /// </summary>
        NavigationLink,

        /// <summary>
        /// A link that launches the default web browser to a specific URL.
        /// </summary>
        WebLink,

        /// <summary>
        /// A link that launches an external Windows application. For example, the Feedback Hub.
        /// </summary>
        LaunchAppLink,
    }

    public abstract class BonusBarItem : BaseViewModel
    {
        protected BonusBarItem(BonusBarContentKind contentKind)
        {
            ContentKind = contentKind;
        }

        public BonusBarContentKind ContentKind { get; }
    }

    public class BonusBarDescriptionItem : BonusBarItem
    {
        public BonusBarDescriptionItem(string description)
            : base(BonusBarContentKind.Description)
        {
            Description = Param.VerifyString(description, nameof(description));
        }

        public string Description { get; }
    }

    public class BonusBarNavigationLink : BonusBarItem
    {
        public BonusBarNavigationLink(string displayName, EditorKind editorTarget)
            : base(BonusBarContentKind.NavigationLink)
        {
            DisplayName = displayName;
            EditorTarget = editorTarget;
        }

        public string DisplayName { get; }
        public EditorKind EditorTarget { get; }
    }

    public class BonusBarWebLink : BonusBarItem
    {
        public BonusBarWebLink(string displayName, string url)
            : base(BonusBarContentKind.WebLink)
        {
            DisplayName = displayName;
            Url = url;
        }

        public string DisplayName { get; }
        public string Url { get; }
    }

    public class BonusBarLaunchAppLink : BonusBarItem
    {
        public BonusBarLaunchAppLink(string displayName)
            : base(BonusBarContentKind.LaunchAppLink)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}
