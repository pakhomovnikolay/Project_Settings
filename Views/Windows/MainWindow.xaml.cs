using Project_Settings.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace Project_Settings
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowsViewModel();
        }
        private void Window_Closing(object Sender, CancelEventArgs E)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                E.Cancel = true;
                return;
            }
            Application.Current.Shutdown();
        }
    }
}
