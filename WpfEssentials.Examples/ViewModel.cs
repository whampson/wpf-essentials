using System;
using System.Windows;
using System.Windows.Input;
using WpfEssentials.Events;

namespace WpfEssentials.Examples
{
    public class ViewModel : ObservableObject
    {
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequested;
        public event EventHandler<FileDialogEventArgs> FileDialogRequested;

        private bool m_userCanceledExit;
        private bool m_buttonClicked;

        public bool UserCanceledExit
        {
            get { return m_userCanceledExit; }
            set { m_userCanceledExit = value; OnPropertyChanged(); }
        }

        public bool ButtonClicked
        {
            get { return m_buttonClicked; }
            set { m_buttonClicked = value; OnPropertyChanged(); }
        }

        public ICommand ClickMeCommand
        {
            get
            {
                return new RelayCommand(
                    () => MessageBoxRequested?.Invoke(this, new MessageBoxEventArgs(
                        "Now you can choose a file!", "Information",
                        icon: MessageBoxImage.Information,
                        callback: (x) => ButtonClicked = true))
                );
            }
        }

        public ICommand OpenFileCommand
        {
            get
            {
                return new RelayCommand(
                    // What
                    () => FileDialogRequested?.Invoke(this, new FileDialogEventArgs(
                        FileDialogType.OpenFileDialog, OpenFileCommand_Callback)
                        {
                            //Title = "Select a File...",
                            //Filter = "Text Documents|*.txt|All Files|*.*",
                            //FilterIndex = 2,
                            //FileName = "foo.txt",
                            //Multiselect = false,
                            //ValidateNames = true,
                            ValidateNames = false,
                            CheckFileExists = false,
                            CheckPathExists = true,
                            FileName = "Folder Selection."
                        }
                    ),

                    // When
                    () => ButtonClicked
                );
            }
        }

        public void ConfirmAppExit()
        {
            MessageBoxRequested?.Invoke(this, new MessageBoxEventArgs(
                "Are you sure you want to exit?",
                "Confirm Exit",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No,
                callback: ConfirmAppExit_Callback));
        }

        private void ConfirmAppExit_Callback(MessageBoxResult result)
        {
            UserCanceledExit = (result != MessageBoxResult.Yes);
        }

        private void OpenFileCommand_Callback(bool? result, FileDialogEventArgs e)
        {
            if (result == true)
            {
                MessageBoxRequested?.Invoke(this, new MessageBoxEventArgs(
                    "You selected: " + e.FileName,
                    "Selected File",
                    icon: MessageBoxImage.Information));
            }
            else
            {
                MessageBoxRequested?.Invoke(this, new MessageBoxEventArgs(
                    "Operation canceled.",
                    "Canceled",
                    icon: MessageBoxImage.Exclamation));
            }
        }
    }
}
