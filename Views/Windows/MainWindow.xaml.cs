using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Project_Settings
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
