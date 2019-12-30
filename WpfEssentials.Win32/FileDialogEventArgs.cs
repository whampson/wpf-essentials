using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using WpfEssentials.Win32.Properties;

namespace WpfEssentials.Win32
{
    /// <summary>
    /// The available <see cref="FileDialog"/> types.
    /// </summary>
    public enum FileDialogType
    {
        /// <summary>
        /// Represents the <see cref="Microsoft.Win32.OpenFileDialog"/>.
        /// </summary>
        OpenFileDialog,

        /// <summary>
        /// Represents the <see cref="Microsoft.Win32.SaveFileDialog"/>.
        /// </summary>
        SaveFileDialog
    }

    /// <summary>
    /// Provides data for invoking a <see cref="FileDialog"/> via an event.
    /// The available properties mirror those of the <see cref="FileDialog"/>
    /// class.
    /// </summary>
    public class FileDialogEventArgs : EventArgs
    {
        private readonly FileDialog m_dialog;

        /// <summary>
        /// Gets the <see cref="FileDialogType"/> indicating which type of <see cref="FileDialog"/> to show.
        /// </summary>
        public FileDialogType DialogType { get; }

        /// <summary>
        /// Gets or sets the callback <see cref="Action{T}"/> that is invoked after the file dialog is closed.
        /// </summary>
        public Action<bool?, FileDialogEventArgs> Callback { get; set; }

        /// <summary>
        /// Gets a value indicating whether a <see cref="OpenFileDialog"/> will be shown.
        /// </summary>
        public bool IsOpenFileDialog
        {
            get { return DialogType == FileDialogType.OpenFileDialog; }
        }

        /// <summary>
        /// Gets a value indicating whether a <see cref="SaveFileDialog"/> will be shown.
        /// </summary>
        public bool IsSaveFileDialog
        {
            get { return DialogType == FileDialogType.SaveFileDialog; }
        }

        /// <summary>
        /// See <see cref="FileDialog.AddExtension"/>.
        /// </summary>
        public bool AddExtension
        {
            get { return m_dialog.AddExtension; }
            set { m_dialog.AddExtension = value; }
        }

        /// <summary>
        /// See <see cref="FileDialog.CheckFileExists"/>.
        /// </summary>
        public bool CheckFileExists
        {
            get { return m_dialog.CheckFileExists; }
            set { m_dialog.CheckFileExists = value; }
        }

        /// <summary>
        /// See <see cref="FileDialog.CheckPathExists"/>.
        /// </summary>
        public bool CheckPathExists
        {
            get { return m_dialog.CheckPathExists; }
            set { m_dialog.CheckPathExists = value; }
        }

        /// <summary>
        /// See <see cref="SaveFileDialog.CreatePrompt"/>.
        /// </summary>
        public bool CreatePrompt
        {
            get
            {
                if (IsSaveFileDialog)
                {
                    return (m_dialog as SaveFileDialog).CreatePrompt;
                }

                return false;
            }

            set
            {
                if (IsSaveFileDialog)
                {
                    (m_dialog as SaveFileDialog).CreatePrompt = value;
                }
            }
        }

        /// <summary>
        /// See <see cref="FileDialog.CustomPlaces"/>.
        /// </summary>
        public IList<FileDialogCustomPlace> CustomPlaces
        {
            get { return m_dialog.CustomPlaces; }
            set { m_dialog.CustomPlaces = value; }
        }

        /// <summary>
        /// See <see cref="FileDialog.DefaultExt"/>.
        /// </summary>
        public string DefaultExt
        {
            get { return m_dialog.DefaultExt; }
            set { m_dialog.DefaultExt = value; }
        }

        /// <summary>
        /// See <see cref="FileDialog.DereferenceLinks"/>.
        /// </summary>
        public bool DereferenceLinks
        {
            get { return m_dialog.DereferenceLinks; }
            set { m_dialog.DereferenceLinks = value; }
        }

        /// <summary>
        /// See <see cref="FileDialog.FileName"/>.
        /// </summary>
        public string FileName
        {
            get { return m_dialog.FileName; }
            set { m_dialog.FileName = value; }
        }

        /// <summary>
        /// See <see cref="FileDialog.FileNames"/>.
        /// </summary>
        public string[] FileNames
        {
            get { return m_dialog.FileNames; }
        }

        /// <summary>
        /// See <see cref="FileDialog.Filter"/>.
        /// </summary>
        public string Filter
        {
            get { return m_dialog.Filter; }
            set { m_dialog.Filter = value; }
        }

        /// <summary>
        /// See <see cref="FileDialog.FilterIndex"/>.
        /// </summary>
        public int FilterIndex
        {
            get { return m_dialog.FilterIndex; }
            set { m_dialog.FilterIndex = value; }
        }

        /// <summary>
        /// See <see cref="FileDialog.InitialDirectory"/>.
        /// </summary>
        public string InitialDirectory
        {
            get { return m_dialog.InitialDirectory; }
            set { m_dialog.InitialDirectory = value; }
        }

        /// <summary>
        /// See <see cref="OpenFileDialog.Multiselect"/>.
        /// </summary>
        public bool Multiselect
        {
            get
            {
                if (IsOpenFileDialog)
                {
                    return (m_dialog as OpenFileDialog).Multiselect;
                }

                return false;
            }

            set
            {
                if (IsOpenFileDialog)
                {
                    (m_dialog as OpenFileDialog).Multiselect = value;
                }
            }
        }

        /// <summary>
        /// See <see cref="SaveFileDialog.OverwritePrompt"/>.
        /// </summary>
        public bool OverwritePrompt
        {
            get
            {
                if (IsSaveFileDialog)
                {
                    return (m_dialog as SaveFileDialog).OverwritePrompt;
                }

                return false;
            }

            set
            {
                if (IsSaveFileDialog)
                {
                    (m_dialog as SaveFileDialog).OverwritePrompt = value;
                }
            }
        }

        /// <summary>
        /// See <see cref="FileDialog.SafeFileName"/>.
        /// </summary>
        public string SafeFileName
        {
            get { return m_dialog.SafeFileName; }
        }

        /// <summary>
        /// See <see cref="FileDialog.SafeFileNames"/>.
        /// </summary>
        public string[] SafeFileNames
        {
            get { return m_dialog.SafeFileNames; }
        }

        /// <summary>
        /// See <see cref="FileDialog.Title"/>.
        /// </summary>
        public string Title
        {
            get { return m_dialog.Title; }
            set { m_dialog.Title = value; }
        }

        /// <summary>
        /// See <see cref="FileDialog.ValidateNames"/>.
        /// </summary>
        public bool ValidateNames
        {
            get { return m_dialog.ValidateNames; }
            set { m_dialog.ValidateNames = value; }
        }

        /// <summary>
        /// See <see cref="FileDialog.FileOk"/>.
        /// </summary>
        public event CancelEventHandler FileOk
        {
            add { m_dialog.FileOk += value; }
            remove { m_dialog.FileOk -= value; }
        }

        public FileDialogEventArgs(FileDialogType dialogType,
            Action<bool?, FileDialogEventArgs> callback = null)
        {
            DialogType = dialogType;
            Callback = callback;

            switch (DialogType)
            {
                case FileDialogType.OpenFileDialog:
                    m_dialog = new OpenFileDialog();
                    break;
                case FileDialogType.SaveFileDialog:
                    m_dialog = new SaveFileDialog();
                    break;
            }

        }

        public void ShowDialog()
        {
            ShowDialog(null);
        }

        public void ShowDialog(Window owner)
        {
            bool? result = (owner == null)
                ? m_dialog.ShowDialog()
                : m_dialog.ShowDialog(owner);

            Callback?.Invoke(result, this);
        }

        public Stream OpenFile()
        {
            if (IsOpenFileDialog)
            {
                return (m_dialog as OpenFileDialog).OpenFile();
            }

            return (m_dialog as SaveFileDialog).OpenFile();
        }

        public Stream[] OpenFiles()
        {
            if (IsOpenFileDialog)
            {
                return (m_dialog as OpenFileDialog).OpenFiles();
            }

            throw new NotSupportedException(Resources.NotSupported_FileDialog);
        }
    }
}
