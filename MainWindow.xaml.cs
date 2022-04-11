using System;
using System.ComponentModel;
using System.Windows;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Linq;

namespace Project_Settings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.Resources["ResultBackground"] = this.TryFindResource("BlackBackground");
            this.Resources["ResultForeground"] = this.TryFindResource("BlackForeground");
            this.Resources["ResultkMenuItemBackground"] = this.TryFindResource("BlackMenuItemBackground");
            this.Resources["ResultMenuItemForeground"] = this.TryFindResource("BlackMenuItemForeground");
            this.Resources["ResultBorderBrush"] = this.TryFindResource("BlackBorderBrush");
            this.Resources["ResultTreeViewItemBackground"] = this.TryFindResource("BlackTreeViewItemBackground");
            this.Resources["ResultTreeViewItemForeground"] = this.TryFindResource("BlackTreeViewItemForeground");

            //myVersion.Text = Assembly.GetEntryAssembly().GetName().Version.ToString();

            CboxBlackTheames.IsChecked = true;

            var ioMap = ReadMappingFile("Resource Dictionary/TreeViewList.json");
            var mygridMap = ReadGridSheets("Resource Dictionary/Grid.json");

            foreach (var io in ioMap.Lists)
            {
                myTreeView.Items.Add(io.Item);
            }

            //myDataGrid.AutoGenerateColumns = false;
            //myDataGrid.ItemsSource = mygridMap.Columns.ToList();

            //int i=0;
            //foreach (var io in mygridMap.Columns)
            //{
            //    i++;
            //    myDataGrid.ColumnWidth = 70;
            //    myDataGrid.RowHeaderWidth = 30;
            //    myDataGrid.Columns.Add(new DataGridTextColumn
            //    {
            //        Header = "Номер\nкорзины"
            //    });
            //}
        }

        private static MappingConfigTreeView ReadMappingFile(string fileName)
        {
            byte[] jsonUtf8Bytes = File.ReadAllBytes(fileName);
            var readOnlySpan = new ReadOnlySpan<byte>(jsonUtf8Bytes);
            var mapping = JsonSerializer.Deserialize<MappingConfigTreeView>(readOnlySpan);
            return mapping;
        }

        private static MappingConfigGrid ReadGridSheets(string fileName)
        {
            byte[] jsonUtf8Bytes = File.ReadAllBytes(fileName);
            var readOnlySpan = new ReadOnlySpan<byte>(jsonUtf8Bytes);
            var mapping = JsonSerializer.Deserialize<MappingConfigGrid>(readOnlySpan);
            return mapping;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                e.Cancel = true;
                return;
            }
            Application.Current.Shutdown();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool fl = CboxBlackTheames.IsChecked.Value;
            this.Resources["ResultBackground"] = fl ? (this.TryFindResource("BlackBackground")) : (this.TryFindResource("WhiteBackground"));
            this.Resources["ResultForeground"] = fl ? (this.TryFindResource("BlackForeground")) : (this.TryFindResource("WhiteForeground"));
            this.Resources["ResultkMenuItemBackground"] = fl ? (this.TryFindResource("BlackMenuItemBackground")) : (this.TryFindResource("WhitekMenuItemBackground"));
            this.Resources["ResultMenuItemForeground"] = fl ? (this.TryFindResource("BlackMenuItemForeground")) : (this.TryFindResource("WhiteMenuItemForeground"));
            this.Resources["ResultBorderBrush"] = fl ? (this.TryFindResource("BlackBorderBrush")) : (this.TryFindResource("WhiteBorderBrush"));
            this.Resources["ResultTreeViewItemBackground"] = fl ? (this.TryFindResource("BlackTreeViewItemBackground")) : (this.TryFindResource("WhiteTreeViewItemBackground"));
            this.Resources["ResultTreeViewItemForeground"] = fl ? (this.TryFindResource("BlackTreeViewItemForeground")) : (this.TryFindResource("WhiteTreeViewItemForeground"));
        }

        //ExportExcel


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //using (FileStream fs = new FileStream("Resource Dictionary/Grid.json", FileMode.OpenOrCreate))
            //{
            //    MappingConfigGrid gridSheets = new();
            //    foreach (var item in myDataGrid.ItemsSource)
            //    {
            //        item.Equals(gridSheets);
            //        gridSheets.Columns[myDataGrid.Columns.Count].Item = item.Header;
            //        item.Item = myDataGrid.SelectAllCells();
            //        item.Value =
            //        gridSheets.Item = item.ToString();
            //        gridSheets.Value = item.ToString();
            //        JsonSerializer.Serialize(fs, gridSheets);
            //    }
            //}
        }
    }
}
