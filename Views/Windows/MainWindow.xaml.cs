using System.ComponentModel;
using System.Windows;

namespace Project_Settings
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object Sender, CancelEventArgs e)
        {
            bool NoClose = MessageBox.Show("Вы действительно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes;
            if (NoClose)
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
