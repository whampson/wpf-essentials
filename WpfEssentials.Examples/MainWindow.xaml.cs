using System.ComponentModel;
using System.Windows;
using WpfEssentials.Events;

namespace WpfEssentials.Examples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel ViewModel
        {
            get { return (ViewModel) DataContext; }
            set { DataContext = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            ViewModel.MessageBoxRequested += ViewModel_MessageBoxRequested;
            ViewModel.FileDialogRequested += ViewModel_FileDialogRequested;
        }

        private void ViewModel_MessageBoxRequested(object sender, MessageBoxEventArgs e)
        {
            e.Show(this);
        }

        private void ViewModel_FileDialogRequested(object sender, FileDialogEventArgs e)
        {
            e.ShowDialog(this);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewModel.ConfirmAppExit();

            if (ViewModel.UserCanceledExit)
            {
                e.Cancel = true;
            }
        }
    }
}
