# Windows Settings Clone Design Documentation

## Project Structure

- **Windows.SettingsClone.Uwp:** View Layer; main Universal Windows Platform (UWP) application entry point.
- **Windows.SettingsClone.Uwp.ViewModels:** ViewModel Layer; .NET Standard project that contains all of the ViewModels
  for the application.
- **Windows.SettingsClone.Uwp.ViewModels.Tests:** Unit Tests; .NET Core project that contains unit tests for the
  ViewModels.

## View Services

There are a few interfaces that define services that the ViewModel layer needs from the View layer. However, with a MVVM
architecture (and the way the projects are configured), the ViewModel layer cannot access types in UWP assemblies.
Therefore we create interfaces in the ViewModel layer that the View layer implements. These interfaces also greatly aid
with unit testing.

- `INavigationViewService` - abstracts the way the application navigates to different pages and keeps track of the
  history stack for the back button. Implemented in the View layer using `Frame.Navigate` and other UWP `Frame` methods.
- `IPlatformCapabilityService` - abstracts any platform-specific capabilities. For example, being able to query the
  Windows version or other capabilities via the `ApiInformation` class.
- `IThreadDispatcher` - abstracts the ability to run code on either a background or UI thread. Uses
  `CoreApplication.MainView.CoreWindow.Dispatcher .RunAsync()` as the implementation in the View layer. This is also
  great for unit testing, where we don't have to actually switch threads (which takes more time) and we can simulate
  long-running operations and delays without actually delaying.

## Pages

There are only two top-level pages in the application.

- `HomePage` - this is the main landing page that shows all of the groups of settings (System, Devices, Personalization,
  etc.). Clicking on a group takes us to a `SettingGroupPage`.
- `SettingGroupPage` - this is really a template for all of the various setting groups. On the left side is a navigation
  bar that lists all of the sub-settings within the group. In the main part is an editor for the settings. For example,
  in the Personalization group, there is a Background .

## MVVM Flow

The entry point into the application is `App.OnLaunched`.

1. `App` creates a new `RootPage` in `OnLaunched` via `rootFrame.SourcePageType = typeof(RootPage)` and then calls
   `Window.Current.Activate()`.
2. `RootPage.OnLoaded` calls `NavigationService.NavigateTo(typeof(HomePageViewModel))`
3. `NavigationService` knows how to create a page given the ViewModel type and then navigates to (in this case)
   `HomePage`.
4. `HomePage` creates a new `HomePageViewModel` in its constructor.
5.
