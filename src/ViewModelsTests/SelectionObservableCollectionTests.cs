// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectionObservableCollectionTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace WindowsSettingsClone.ViewModels.Tests
{
    using System.ComponentModel;
    using FluentAssertions;
    using FluentAssertions.Events;
    using NUnit.Framework;

    public class SelectionObservableCollectionTests
    {
        //// ===========================================================================================================
        //// Ctor Tests
        //// ===========================================================================================================

        [Test]
        public void Ctor_should_populate_the_collection()
        {
            var collection = new SelectionObservableCollection<string>(new[] { "a", "b", "c" });
            collection.Should().BeEquivalentTo("a", "b", "c");
        }

        [Test]
        public void Ctor_should_default_to_nothing_being_selected()
        {
            var collection = new SelectionObservableCollection<string>(new[] { "a", "b", "c" });
            collection.SelectedItem.Should().BeNull();
        }

        [Test]
        public void Ctor_should_select_the_specified_item()
        {
            var collection = new SelectionObservableCollection<string>(new[] { "a", "b", "c" }, selectedIndex: 1);
            collection.SelectedItem.Should().Be("b");
        }

        //// ===========================================================================================================
        //// SelectedItem Tests
        //// ===========================================================================================================

        [Test]
        public void SelectedItem_should_raise_the_PropertyChanged_event_when_changed()
        {
            var collection = new SelectionObservableCollection<string>(new[] { "a", "b", "c" });
            using (IMonitor<INotifyPropertyChanged> monitoredCollection = collection.Monitor<INotifyPropertyChanged>())
            {
                collection.SelectedItem = "c";

                // Since ObservableCollection implements INotifyPropertyChanged explicitly, we can't use the fluent
                // assertions shortcut `Should().RaisePropertyChangedEventFor`.
                monitoredCollection.Should()
                    .Raise("PropertyChanged")
                    .WithSender(collection)
                    .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == "SelectedItem");
            }
        }

        [Test]
        public void SelectedItem_should_raise_the_SelectedItemChanged_event_when_changed()
        {
            var collection = new SelectionObservableCollection<string>(new[] { "a", "b", "c" });
            using (IMonitor<SelectionObservableCollection<string>> monitoredCollection = collection.Monitor())
            {
                collection.SelectedItem = "c";
                monitoredCollection.Should().Raise("SelectedItemChanged").WithSender(collection);
            }
        }

        [Test]
        public void SelectedItem_should_not_raise_the_PropertyChanged_event_when_changed()
        {
            var collection = new SelectionObservableCollection<string>(new[] { "a", "b", "c" }, selectedIndex: 2);
            using (IMonitor<INotifyPropertyChanged> monitoredCollection = collection.Monitor<INotifyPropertyChanged>())
            {
                collection.SelectedItem = "c";

                // Since ObservableCollection implements INotifyPropertyChanged explicitly, we can't use the fluent
                // assertions shortcut `Should().NotRaisePropertyChangedEventFor`.
                monitoredCollection.Should().NotRaise("PropertyChanged");
            }
        }

        [Test]
        public void SelectedItem_should_not_raise_the_SelectedItemChanged_event_when_changed()
        {
            var collection = new SelectionObservableCollection<string>(new[] { "a", "b", "c" }, selectedIndex: 2);
            using (IMonitor<SelectionObservableCollection<string>> monitoredCollection = collection.Monitor())
            {
                collection.SelectedItem = "c";
                monitoredCollection.Should().NotRaise("SelectedItemChanged");
            }
        }

        //// ===========================================================================================================
        //// Extensions Tests
        //// ===========================================================================================================

        [Test]
        public void Select_should_find_the_named_value_with_a_specific_value()
        {
            var collection = new SelectionObservableCollection<NamedValue<int>>(new[]
            {
                new NamedValue<int>("One", 1),
                new NamedValue<int>("Two", 2),
                new NamedValue<int>("Three", 3),
            });

            collection.Select(2);
            collection.SelectedItem.Value.Should().Be(2);
        }

        [Test]
        public void Select_should_do_nothing_if_the_value_cannot_be_found()
        {
            var collection = new SelectionObservableCollection<NamedValue<int>>(new[]
            {
                new NamedValue<int>("One", 1),
                new NamedValue<int>("Two", 2),
                new NamedValue<int>("Three", 3),
            });

            collection.Select(4);
            collection.SelectedItem.Should().BeNull();
        }
    }
}
