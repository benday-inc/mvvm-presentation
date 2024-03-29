# Benday.Presentation.Controls & Benday.Presentation
Written by Benjamin Day  
Pluralsight Author | Microsoft MVP | Scrum.org Professional Scrum Trainer (PST)
https://www.benday.com  
info@benday.com 

*Got ideas for features you'd like to see? Found a bug? Let us know by submitting an issue https://github.com/benday-inc/mvvm-presentation/issues*. *Want to contribute? Submit a pull request.*

* [Documentation](https://benday-inc.github.io/mvvm-presentation/)

**[Benday.Presentation](https://www.nuget.org/packages/Benday.Presentation/)** is a collection of classes and utility methods working with the model-view-viewmodel (MVVM) pattern in .NET MAUI. These classes support the controls in the Benday.Presentation.Controls package.

Here are the classes and interfaces:

- ExceptionHandlingRelayCommand
- ILabeledField
- IMessageBoxSampleViewModel
- IMessageManager
- IProgressBarSampleViewModel
- IProgressBarViewModel
- ISelectable
- ISelectableItem
- IViewModelBase
- IVisibleField
- MessageBoxEventArgs
- MessageBoxEventHandler
- MessageBoxMessageManager
- MessageBoxRequestedEventArgs
- MessagingViewModelBase
- NavigatableList
- ProgressBarViewModel
- SelectableCollectionViewModel
- SelectableItem
- SingleSelectListViewModel
- ViewModelBase
- ViewModelField
- Plus a collection of useful Value Converters

**[Benday.Presentation.Controls](https://www.nuget.org/packages/Benday.Presentation.Controls/)** is a collection of reusable controls for displaying and editing data in an model-view-viewmodel (MVVM) application that uses MAUI .NET. These controls use the MVVM utilities in Benday.Presentation. 

More documentation coming soon but here's a list of controls:

* TextboxField
* ListboxField
* ComboboxField
* LabelField

The **Benday.Presentation.DemoApp** project in this repository is a simple sample application that uses the features of the Benday.Presentation and Benday.Presentation.Controls packages. The goal of this sample application is to move as much logic as possible out of the XAML files and into ViewModels and then to rely almost exclusively on data binding and command binding.  

| Sample Application in iPhone Simulator     | Sample Application on MacOS     | Sample Application on Windows     |
| ---- | ---- | ---- |
| <img src="./assets/images/demo-app-ios.png" alt="demo-app-ios" style="zoom: 50%;" />     | <img src="./assets/images/demo-app-macos.png" alt="demo-app-macos" style="zoom:50%;" />     | <img src="./assets/images/demo-app-windows.png" alt="demo-app-windows" style="zoom:50%;" />     |

