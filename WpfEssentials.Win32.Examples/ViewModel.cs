using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using WpfEssentials.Win32;

namespace WpfEssentials.Examples
{
    public class ViewModel : ObservableObject
    {
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequested;
        public event EventHandler<FileDialogEventArgs> FileDialogRequested;

        private bool m_userCanceledExit;
        private bool m_buttonClicked;
        private ObservableCollection<string> m_openFiles;
        private int m_openFileSelectedIndex;
        private string m_text;

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

        public ObservableCollection<string> OpenFiles
        {
            get { return m_openFiles; }
            set { m_openFiles = value; OnPropertyChanged(); }
        }

        public int OpenFileSelectedIndex
        {
            get { return m_openFileSelectedIndex; }
            set { m_openFileSelectedIndex = value; OnPropertyChanged(); }
        }

        public string Text
        {
            get { return m_text; }
            set { m_text = value; OnPropertyChanged(); }
        }

        public ICommand ClickMeCommand
        {
            get
            {
                return new RelayCommand(
                    () => MessageBoxRequested?.Invoke(this, new MessageBoxEventArgs(
                        "Now you can choose a file!", "Hint",
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
                            Title = "Open File(s)...",
                            Filter = "Text Documents|*.txt",
                            Multiselect = true,
                            ValidateNames = true,
                        }
                    ),

                    // When
                    () => ButtonClicked
                );
            }
        }

        public void ReadSelectedFile()
        {
            if (m_openFileSelectedIndex < 0 || m_openFileSelectedIndex >= m_openFiles.Count)
            {
                Text = "";
                return;
            }

            string path = m_openFiles[m_openFileSelectedIndex];
            Text = File.ReadAllText(path);
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
            if (result != true)
            {
                MessageBoxRequested?.Invoke(this, new MessageBoxEventArgs(
                    "Operation canceled!",
                    "Canceled",
                    icon: MessageBoxImage.Exclamation));
                return;
            }

            OpenFiles = new ObservableCollection<string>(e.FileNames);
            ReadSelectedFile();
        }
    }
}
