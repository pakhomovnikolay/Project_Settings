using Project_Settings.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace Project_Settings
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainWindowsViewModel();
        }

        private void Window_Closing(object Sender, CancelEventArgs e)
        {
            bool NoClose = MessageBox.Show("Вы действительно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes;
            if (NoClose)
            {
                e.Cancel = true;
                return;
            }

            //bool NoSave = MessageBox.Show("Сохранить проект?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes;
            //if (!NoSave)
            //{

            //}

            //if (MessageBox.Show("Сохранить проект?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            //{
            //    Application.Current.Shutdown();
            //}
        }
    }
}
