# WpfEssentials
[![Nuget (WpfEssentials)](https://img.shields.io/nuget/v/WpfEssentials)](https://www.nuget.org/packages/WpfEssentials/)
[![Build Status](https://github.com/whampson/WpfEssentials/workflows/.github/workflows/dotnetcore.yml/badge.svg)](https://github.com/whampson/WpfEssentials/actions)

A .NET package containing helper classes, extension methods, converters, and
base classes designed to simplify Win32 WPF app development.

## Features
- `ObservableObject` - An `INotifyPropertyChanged` implementation that makes
changes to the state of an object observable by the view. To use, simply inherit
from this class and call `OnPropertyChanged()` in your setters.
- `FullyObservableCollection` - An `ObservableCollection` that allows the view
to monitor changes to the state of each item in the collection, in addition to
being able to monitor changes made to the collection itself. You can bind your
view to the `ItemPropertyChanged` event to listen for item state changes.
- `RelayCommand` - An `ICommand` implementation that lets you bind functions to
components in a view. Commands can be unconditional or made to execute only when
a condition is met (e.g. only when a file is open in the program).
- `EnumDescriptionConverter` - An `IValueConverter` that will extract the
description from an enum value if decorated with a `DescriptionAttribute`. This
comes in handy when using enums for combo boxes.
- `MessageBoxEx` - A Win32 `MessageBox` wrapper that's plays nicer with WPF
applications. Message boxes can be made modal and will center over the parent
window. All features of the standard Win32 `MessageBox` are included.
- `MessageBoxEventArgs` - An `EventArgs` class for passing arguments to a
message box. You can also specify a callback function that's invoked with the
dialog result when closed. Useful if you're following MVVM and want to display a
message box with little code-behind.
- `FileDialogEventArgs` An `EventArgs` class for passing arguments to a
`FileDialog`. Includes a callback function. You can also specify a callback
function that's invoked with the dialog result and dialog state when closed.
Useful if you're following MVVM and want to display a file dialog for
opening/saving a file with little code-behind.

See examples of these classes in use [here](https://github.com/whampson/WpfEssentials/tree/master/WpfEssentials.Examples)!

## Contributing
If you have an idea, request, or feedback, please [open an issue](https://github.com/whampson/WpfEssentials/issues/new)
and tell me what you think.

If you'd like to contribute, you may fork the repository and make changes as
you like. Pull requests are warmly welcomed!

## License
MIT
