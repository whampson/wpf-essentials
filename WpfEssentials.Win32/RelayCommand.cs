using System;
using System.Windows.Input;

namespace WpfEssentials.Win32
{
    /// <summary>
    /// Provides a simple way to bind functions to components in a view.
    /// </summary>
    /// <remarks>
    /// Adapted from Josh Smith's RelayCommand class.
    /// https://msdn.microsoft.com/en-us/magazine/dd419663.aspx
    /// </remarks>
    public class RelayCommand : ICommand
    {
        private readonly Func<bool> canExecute;
        private readonly Action execute;

        /// <summary>
        /// Creates a new <see cref="RelayCommand"/> instance with the specified action.
        /// </summary>
        /// <param name="what">The method to be called when the command is invoked.</param>
        public RelayCommand(Action what)
            : this(what, null)
        { }

        /// <summary>
        /// Creates a new <see cref="RelayCommand"/> instance with the specified action
        /// and condition.
        /// </summary>
        /// <param name="what">The method to be called when the command is invoked.</param>
        /// <param name="when">The method that determines whether the command can execute in its current state.</param>
        public RelayCommand(Action what, Func<bool> when)
        {
            execute = what;
            canExecute = when;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return (canExecute == null) ? true : canExecute();
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }

    /// <summary>
    /// Provides a simple way to bind functions to components in a view.
    /// </summary>
    /// <typeparam name="T">
    /// Action parameter type.
    /// </typeparam>
    /// <remarks>
    /// Adapted from Josh Smith's RelayCommand class.
    /// https://msdn.microsoft.com/en-us/magazine/dd419663.aspx
    /// </remarks>
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> canExec;
        private readonly Action<T> exec;

        /// <summary>
        /// Creates a new <see cref="RelayCommand{T}"/> instance with the specified action
        /// and condition.
        /// </summary>
        /// <param name="what">The method to be called when the command is invoked.</param>
        public RelayCommand(Action<T> what)
            : this(what, null)
        { }

        /// <summary>
        /// Creates a new <see cref="RelayCommand{T}"/> instance with the specified action
        /// and condition.
        /// </summary>
        /// <param name="what">The method to be called when the command is invoked.</param>
        /// <param name="when">The method that determines whether the command can execute in its current state.</param>
        public RelayCommand(Action<T> what, Predicate<T> when)
        {
            exec = what;
            canExec = when;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExec != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (canExec != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return (canExec == null) ? true : canExec((T) parameter);
        }

        public void Execute(object parameter)
        {
            exec((T) parameter);
        }
    }
}
