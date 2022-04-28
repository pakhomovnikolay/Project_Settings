using Project_Settings.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        private void MyDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void btnMyColor_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    var row_list = GetDataGridRows(MyDataGrid);
            //    foreach (var single_row in row_list)
            //    {
            //        if (single_row.IsSelected == true)
            //        {
            //            single_row.Background = btnMyColor.Background;
            //            //MessageBox.Show("the row no." + (single_row.GetIndex() + 1).ToString() + " is selected!");
            //        }
            //    }

            //}
            //catch { }
        }

        private IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (itemsSource == null) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null) yield return row;
            }
        }
    }
}
