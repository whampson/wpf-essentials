# WpfEssentials
[![Nuget (WpfEssentials)](https://img.shields.io/nuget/v/WpfEssentials)](https://www.nuget.org/packages/WpfEssentials/)
[![Build Status](https://github.com/whampson/WpfEssentials/workflows/WpfEssentials/badge.svg)](https://github.com/whampson/WpfEssentials/actions)

A .NET package containing data types and extension methods designed to simplify
WPF app development.

### Features
- `ObservableObject` - An `INotifyPropertyChanged` implementation that allows
the view to observe changes in an object's state. To use, simply inherit from
this class and call `OnPropertyChanged()` in your setters.
- `FullyObservableCollection` - An `ObservableCollection` that allows the view
to monitor changes to the state of each item in the collection, in addition to
being able to monitor changes made to the collection itself. You can bind your
view to the `ItemStateChanged` event to listen for item state changes.
- `EnumExtensions` - Extension methods for the `Enum` class. Provides useful
methods like `GetDescription()` for extracting a description string from a
`DescriptionAttribute`, and the more generic `GetAttribute<T>()` for getting a
reference to an arbitrary `Attribute` attached to an enum value.
- *And more!*

## WpfEssentials.Win32
[![Nuget (WpfEssentials.Win32)](https://img.shields.io/nuget/v/WpfEssentials.Win32)](https://www.nuget.org/packages/WpfEssentials.Win32/)
[![Build Status](https://github.com/whampson/WpfEssentials/workflows/WpfEssentials.Win32/badge.svg)](https://github.com/whampson/WpfEssentials/actions)

A Win32-specific extension of **WpfEssentials**.

### Features
- `RelayCommand` - An `ICommand` implementation that lets you bind functions to
components in a view. Commands can be unconditional or made to execute only when
a condition is met (e.g. only when a file is open in the program).
- `EnumValueCollection` - A `MarkupExtension` that lets you get a collection of
all the values in an enum in XAML. This makes it easy to put all values of an
enum in a combo box.
- `EnumDescriptionConverter` - An `IValueConverter` that will extract the
description from an enum value if decorated with a `DescriptionAttribute`. This
comes in handy when using enum values in combo boxes.
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
- *And more!*

See examples of these classes in use [here](https://github.com/whampson/WpfEssentials/tree/master/WpfEssentials.Win32.Examples)!

## Contributing
If you have an idea, request, or feedback, please [open an issue](https://github.com/whampson/WpfEssentials/issues/new)
and tell me what you think.

If you'd like to contribute, you may fork the repository and make changes as
you like. Pull requests are warmly welcomed!

## License
MIT
