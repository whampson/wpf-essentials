using System;
using System.Windows.Input;

namespace WpfEssentials
{
    /// <summary>
    /// Provides a simple way to bind commands to components in a view.
    /// </summary>
    /// <remarks>
    /// Adapted from Josh Smith's RelayCommand class.
    /// https://msdn.microsoft.com/en-us/magazine/dd419663.aspx
    /// </remarks>
    public class RelayCommand : ICommand
    {
        private readonly Func<bool> canExecute;
        private readonly Action execute;

        public RelayCommand(Action what)
            : this(what, null)
        { }

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
    /// Provides a simple way to bind commands to components in a view.
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

        public RelayCommand(Action<T> what)
            : this(what, null)
        { }

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
