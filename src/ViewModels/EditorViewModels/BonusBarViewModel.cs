// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BonusBarViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.EditorViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Utility;

    /// <summary>
    /// ViewModel for the bonus bar, which is the list of related settings, questions, and other content on the right
    /// pane within a category page.
    /// </summary>
    public class BonusBarViewModel : BaseViewModel
    {
        public BonusBarViewModel(params BonusBarSection[] sections)
        {
            Sections = new List<BonusBarSection>(sections ?? new BonusBarSection[0]).AsReadOnly();
        }

        public IReadOnlyCollection<BonusBarSection> Sections { get; }

        public static BonusBarViewModel CreateStandard(
            IEnumerable<BonusBarItem> relatedSettings,
            IEnumerable<BonusBarWebLink> supportLinks)
        {
            return CreateStandard(null, relatedSettings, supportLinks);
        }

        public static BonusBarViewModel CreateStandard(
            BonusBarOverviewSection overviewSection,
            IEnumerable<BonusBarItem> relatedSettings,
            IEnumerable<BonusBarWebLink> supportLinks)
        {
            var items = new List<BonusBarSection>();
            if (overviewSection != null)
            {
                items.Add(overviewSection);
            }

            items.Add(new BonusBarRelatedSettingsSection(relatedSettings.ToArray()));
            items.Add(new BonusBarSupportSection(supportLinks.ToArray()));
            items.Add(new BonusBarFeedbackSection());

            return new BonusBarViewModel(items.ToArray());
        }
    }

    public enum BonusBarSectionKind
    {
        Overview,
        RelatedSettings,
        PrivacyOptions,
        Support,
        Feedback,
    }

    public abstract class BonusBarSection : BaseViewModel
    {
        protected BonusBarSection(BonusBarSectionKind sectionKind, string headerDisplayName)
        {
            SectionKind = sectionKind;
            HeaderDisplayName = Param.VerifyString(headerDisplayName, nameof(headerDisplayName));
        }

        public BonusBarSectionKind SectionKind { get; }
        public string HeaderDisplayName { get; }
    }

    /// <summary>
    /// A textual description that usually gives more details about a setting. For example, the "Sleep better"
    /// section in System/Display.
    /// </summary>
    public class BonusBarOverviewSection : BonusBarSection
    {
        public BonusBarOverviewSection(string headerDisplayName, string overview, BonusBarNavigationLink actionLink)
            : base(BonusBarSectionKind.Overview, headerDisplayName)
        {
            Overview = overview ?? string.Empty;
            ActionLink = actionLink;
        }

        public string Overview { get; }
        public BonusBarNavigationLink ActionLink { get; }
        public bool ShouldDisplayActionLink => ActionLink != null;
    }

    public class BonusBarRelatedSettingsSection : BonusBarSection
    {
        public BonusBarRelatedSettingsSection(params BonusBarItem[] relatedSettings)
            : base(BonusBarSectionKind.RelatedSettings, Strings.RelatedSettingsHeader)
        {
            RelatedSettings = relatedSettings.ToList().AsReadOnly();
        }

        public IReadOnlyList<BonusBarItem> RelatedSettings { get; }
    }

    public class BonusBarPrivacyOptionsSection : BonusBarSection
    {
        public BonusBarPrivacyOptionsSection()
            : base(BonusBarSectionKind.PrivacyOptions, "Privacy options - not implemented")
        {
        }
    }

    public class BonusBarSupportSection : BonusBarSection
    {
        public BonusBarSupportSection(params BonusBarWebLink[] supportLinks)
            : base(BonusBarSectionKind.Support, Strings.HaveAQuestionHeader)
        {
            SupportLinks = supportLinks.ToList().AsReadOnly();
        }

        public IReadOnlyList<BonusBarWebLink> SupportLinks { get; }
        public BonusBarLaunchAppLink GetHelpLink { get; } = new BonusBarLaunchAppLink(Strings.GetHelpLink);
    }

    public class BonusBarFeedbackSection : BonusBarSection
    {
        public BonusBarFeedbackSection()
            : base(BonusBarSectionKind.Feedback, Strings.MakeWindowsBetterHeader)
        {
        }

        public BonusBarLaunchAppLink GetFeedbackLink { get; } = new BonusBarLaunchAppLink(Strings.GiveUsFeedbackLink);
    }

    public enum BonusBarItemKind
    {
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
        protected BonusBarItem(BonusBarItemKind itemKind)
        {
            ItemKind = itemKind;
        }

        public BonusBarItemKind ItemKind { get; }
    }

    public class BonusBarNavigationLink : BonusBarItem
    {
        public BonusBarNavigationLink(string displayName, EditorKind editorTarget)
            : base(BonusBarItemKind.NavigationLink)
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
            : base(BonusBarItemKind.WebLink)
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
            : base(BonusBarItemKind.LaunchAppLink)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}
