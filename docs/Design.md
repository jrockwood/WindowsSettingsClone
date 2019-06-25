# Windows Settings Clone Design Documentation

## General Architecture

The solution uses a fairly strict MVVM (Model-View-ViewModel) architecture, which gives very clear boundaries and the
ability to unit test layers independently from each other.

## Project Structure

The projects are organized using solution folders as a layered architecture, with the projects on the top depending on
projects below them. So, the `Shared` project is on the very bottom and has no dependencies and the `UwpApp` is on the
top, depending either directly or indirectly on everything below it.

### View Layer (Apps)

Depends on the `ViewModel` layer (and indirectly the `Model` layer). Implements many of the service contracts contained
in the `Services` layer.

- **UwpApp** (_UWP Application_)
  - Main Universal Windows Platform (UWP) application entry point.
  - Serves as the "client" in the `AppServiceConnection` client-server relationship to implement calling a service and
    receiving responses from the full-trust `DesktopServicesApp` application.
- **DesktopServicesApp** _(.NET Framework Windows Application)_
  - In order to read registry settings, perform Win32 API calls, or read protected files a UWP application needs to
    register a separate executable and run it in a separate full-trust process. This is that application, which acts as
    a server end-point and listens to `AppServiceConnection` requests from the main UWP application.
  - Contains the implementation of the interfaces defined in the `ServiceContracts` project.
  - Serves as the "server" in the `AppServiceConnection` client-server relationship to implement receiving reqests and
    sending responses back to the UWP app.
- **Packaging** (_UWP Packaging Project_)
  - This takes `UwpApp` and `DesktopServicesApp` and bundles them together into a UWP application.

### ViewModel Layer

Depends on the `Model` layer.

- **ViewModels** (_.NET Standard Library_)
  - Contains all of the ViewModels for the application.
- **ViewModelsTests:** (_.NET Core Unit Test Project_)
  - Contains unit tests for the `ViewModels` project.

### Model Layer

Depends on the `Services` layer.

- **Models** (_.NET Standard Library_)
  - Contains all of the Models for the application.
- **ModelsTests:** (_.NET Core Unit Test Project_)
  - Contains unit tests for the `Models` project.

### Services Layer

No dependencies on other projects (except in the `Shared` layer).

- **ServiceContracts** (_.NET Standard Library_)
  - Contains interfaces and some implementation for the various services that are needed in the application.
  - Abstracts the communication channel (which uses the UWP `AppServiceConnection`) and handles low-level requests and
    responses by serializing and deserializing commands.

### Shared Layer

No dependencies on other projects.

- **Shared** (_.NET Standard Library_)
  - Contains utility methods and other classes that are used in all projects.
- **SharedTests:** (_.NET Core Unit Test Project_)
  - Contains unit tests for the `Shared` project.

## Services

### Full-trust Services

The `Model` layer needs to retrieve various settings from the underlying operating system. Some are stored in the
Windows Registry, some in files in the `Windows` directory or other system files, and some are accessed via Win32 API
function calls. The interfaces for these services are defined in the `ServiceContracts` project, but the implementation
is in the `DesktopServices` full-trust application.

### View Services

There are a few interfaces that define services that the `ViewModel` layer needs from the `View` layer. However, with an
MVVM architecture (and the way the projects are configured), the `ViewModel` layer cannot access types in UWP
assemblies. Therefore we create interfaces in the `Services` layer that the `View` layer implements. These interfaces
also greatly aid with unit testing.

- `INavigationViewService` - abstracts the way the application navigates to different pages and keeps track of the
  history stack for the back button. Implemented in the View layer using `Frame.Navigate` and other UWP `Frame` methods.
- `IPlatformCapabilityService` - abstracts any platform-specific capabilities. For example, being able to query the
  Windows version or other capabilities via the `ApiInformation` class.
- `IThreadDispatcher` - abstracts the ability to run code on either a background or UI thread. Uses
  `CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync()` as the implementation in the View layer. This is also
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
2. `RootPage.OnLoaded` calls `NavigationViewService.NavigateTo(typeof(HomePageViewModel))`
3. `NavigationViewService` knows how to create a page given the ViewModel type and then navigates to (in this case)
   `HomePage`.
4. `HomePage` creates a new `HomePageViewModel` in its constructor.
5. TODO
