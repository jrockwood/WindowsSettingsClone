// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BonusBarViewModel.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.Uwp.ViewModels.EditorViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class BonusBarViewModel
    {
        public BonusBarViewModel(
            IEnumerable<RelatedSettingLink> relatedSettingsLinks,
            IEnumerable<WebLink> questionLinks)
        {
            RelatedSettingsLinks =
                new ReadOnlyCollection<RelatedSettingLink>(new List<RelatedSettingLink>(relatedSettingsLinks));
            QuestionLinks = new ReadOnlyCollection<WebLink>(new List<WebLink>(questionLinks));
        }

        public IReadOnlyList<RelatedSettingLink> RelatedSettingsLinks { get; }
        public IReadOnlyList<WebLink> QuestionLinks { get; }
        public LaunchAppLink GetHelpLink { get; } = new LaunchAppLink(Strings.GetHelpLink);
        public LaunchAppLink FeedbackLink { get; } = new LaunchAppLink(Strings.GiveUsFeedbackLink);
    }

    public class RelatedSettingLink
    {
        public RelatedSettingLink(string displayName, EditorKind editorTarget)
        {
            DisplayName = displayName;
            EditorTarget = editorTarget;
        }

        public string DisplayName { get; }
        public EditorKind EditorTarget { get; }
    }

    public class WebLink
    {
        public WebLink(string displayName, string url)
        {
            DisplayName = displayName;
            Url = url;
        }

        public string DisplayName { get; }
        public string Url { get; }
    }

    public class LaunchAppLink
    {
        public LaunchAppLink(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}
