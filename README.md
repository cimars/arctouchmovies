# Arctouch Upcoming Movie app

The application is an MVP with an UI pretty simple, but meets all the requirements requested; Upcoming movie browse with pagination, viewing movie details, and movie searching. The application can run on iOS and Android, and it can work on landscape and portrait orientations.

## Requirements
+ Visual Studio 2017 or Visual Studio for Mac
+ iOS
	+ Xcode 9
	+ Minimum SDK iOS 8.0
+ Android
	+ Minimum Android SDK 5.0

## Running application
+ Clone repository
+ Open Arctouch.Movies.sln solution file with Visual Studio or Visual Studio for Mac
+ Restore packages for all projects
+ Build solution
+ Run on Android
    + Make Arctouch.Movies.UI.Droid project as default to start, and run it on an emulator with minimum Android 4.0.3
+ Run on iOS
    + Make Arctouch.Movies.UI.iOS project as default to start, and run it on the iOS simulator with minimun iOS version 8.0

## Main technologies and frameworks used
+ Xamarin
+ C#
+ Xamarin Forms
+ Prism used to build the UI using MVVM pattern (https://github.com/PrismLibrary/Prism)
+ .Net HttpClient
+ NUnit

## Description of the architecture
+ Components organized on Layers
	+ Data and Service Layer
		+ Arctouch.Movies.Services holds the classes that implement the component that consumes MovieDB web api. They build requests, execute them, and parse results.
	+ Business Layer
		+ Arctouch.Movies.Common project holds all common clases that can be used by all the components such as interfaces, helpers, domain entities, etc.
		+ Arctouch.Movies.Config holds the configuration of the dependency injection for all the components.
		+ Arctouch.Movies.Managers holds the classes that implement business and application logic on the app.
	+ UI Layer
		+ Arctouch.Movies.UI project that holds all the base screens/pages using Prism to build UI for iOS and Android
		+ Arctouch.Movies.UI.Droid project that holds UI for Android
		+ Arctouch.Movies.UI.iOS project that holds UI for iOS		
+ Testing
	+ It was included unit testing for the Manager and Service components. Additionally, could be included UI tests using Xamarin UITest or Appium, but for this initial MVP was not included yet.
	+ Projects that holds tests for Services and Managers are: Arctouch.Movies.Services.Tests and Arctouch.Movies.Managers.Tests respectively.