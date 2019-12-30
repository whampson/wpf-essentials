using Microsoft.Win32;
using System;
using System.Windows;

namespace WpfEssentials.Events
{
    public sealed class FileDialogEventArgs : EventArgs
    {
        public enum FileDialogType
        {
            OpenDialog,
            SaveDialog
        }

        public FileDialogEventArgs(FileDialogType dialogType,
            string title = null,
            string filter = null,
            string fileName = null,
            string initialDirectory = null,
            bool selectDirectory = false,
            Action<bool?, FileDialogEventArgs> resultAction = null)
        {
            DialogType = dialogType;
            Title = title;
            Filter = filter;
            FileName = fileName;
            InitialDirectory = initialDirectory;
            SelectDirectory = selectDirectory;
            ResultAction = resultAction;
        }

        public FileDialogType DialogType { get; }

        public string Title { get; }

        public string Filter { get; }

        public string FileName { get; private set; }

        public string InitialDirectory { get; }

        public bool SelectDirectory { get; }

        public Action<bool?, FileDialogEventArgs> ResultAction { get; }

        public void ShowDialog()
        {
            ShowDialog(null);
        }

        public void ShowDialog(Window owner)
        {
            FileDialog dialog = null;
            switch (DialogType)
            {
                case FileDialogType.OpenDialog:
                    dialog = new OpenFileDialog();
                    break;
                case FileDialogType.SaveDialog:
                    dialog = new SaveFileDialog();
                    break;
            }

            dialog.Title = Title;
            dialog.Filter = Filter;
            dialog.FileName = FileName;
            dialog.InitialDirectory = InitialDirectory;

            // TODO: select directory only??

            bool? result;
            if (owner == null)
            {
                result = dialog.ShowDialog();
            }
            else
            {
                result = dialog.ShowDialog(owner);
            }

            if (result == true)
            {
                FileName = dialog.FileName;
            }

            ResultAction?.Invoke(result, this);
        }
    }
}
