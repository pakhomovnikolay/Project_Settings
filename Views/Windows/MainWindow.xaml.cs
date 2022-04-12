using System.Windows;

namespace Project_Settings
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Window_Closing(object Sender, CancelEventArgs E)
        //{
        //    if (MessageBox.Show("Вы действительно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
        //    {
        //        E.Cancel = true;
        //        return;
        //    }
        //    Application.Current.Shutdown();
        //}
    }
}

//{
//    / < summary >
//    / Interaction logic for MainWindow.xaml
//    / </ summary >


//    {


//        {



//            this.Resources["ResultBackground"] = this.TryFindResource("BlackBackground");
//            this.Resources["ResultForeground"] = this.TryFindResource("BlackForeground");
//            this.Resources["ResultkMenuItemBackground"] = this.TryFindResource("BlackMenuItemBackground");
//            this.Resources["ResultMenuItemForeground"] = this.TryFindResource("BlackMenuItemForeground");
//            this.Resources["ResultBorderBrush"] = this.TryFindResource("BlackBorderBrush");
//            this.Resources["ResultTreeViewItemBackground"] = this.TryFindResource("BlackTreeViewItemBackground");
//            this.Resources["ResultTreeViewItemForeground"] = this.TryFindResource("BlackTreeViewItemForeground");

//            //myVersion.Text = Assembly.GetEntryAssembly().GetName().Version.ToString();

//            CboxBlackTheames.IsChecked = true;

//            var ioMap = ReadMappingFile("Resource Dictionary/TreeViewList.json");
//            var mygridMap = ReadGridSheets("Resource Dictionary/Grid.json");

//            foreach (var io in ioMap.Lists)
//            {
//                myTreeView.Items.Add(io.Item);
//            }

//            //myDataGrid.AutoGenerateColumns = false;
//            //myDataGrid.ItemsSource = mygridMap.Columns.ToList();

//            //int i=0;
//            //foreach (var io in mygridMap.Columns)
//            //{
//            //    i++;
//            //    myDataGrid.ColumnWidth = 70;
//            //    myDataGrid.RowHeaderWidth = 30;
//            //    myDataGrid.Columns.Add(new DataGridTextColumn
//            //    {
//            //        Header = "Номер\nкорзины"
//            //    });
//            //}
//        }

//        private static MappingConfigTreeView ReadMappingFile(string fileName)
//        {
//            byte[] jsonUtf8Bytes = File.ReadAllBytes(fileName);
//            var readOnlySpan = new ReadOnlySpan<byte>(jsonUtf8Bytes);
//            var mapping = JsonSerializer.Deserialize<MappingConfigTreeView>(readOnlySpan);
//            return mapping;
//        }

//        private static MappingConfigGrid ReadGridSheets(string fileName)
//        {
//            byte[] jsonUtf8Bytes = File.ReadAllBytes(fileName);
//            var readOnlySpan = new ReadOnlySpan<byte>(jsonUtf8Bytes);
//            var mapping = JsonSerializer.Deserialize<MappingConfigGrid>(readOnlySpan);
//            return mapping;
//        }

//        private void Window_Closing(object sender, CancelEventArgs e)
//        {
//            if (MessageBox.Show("Вы действительно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
//            {
//                e.Cancel = true;
//                return;
//            }
//            Application.Current.Shutdown();
//        }



//        //ExportExcel


//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            //using (FileStream fs = new FileStream("Resource Dictionary/Grid.json", FileMode.OpenOrCreate))
//            //{
//            //    MappingConfigGrid gridSheets = new();
//            //    foreach (var item in myDataGrid.ItemsSource)
//            //    {
//            //        item.Equals(gridSheets);
//            //        gridSheets.Columns[myDataGrid.Columns.Count].Item = item.Header;
//            //        item.Item = myDataGrid.SelectAllCells();
//            //        item.Value =
//            //        gridSheets.Item = item.ToString();
//            //        gridSheets.Value = item.ToString();
//            //        JsonSerializer.Serialize(fs, gridSheets);
//            //    }
//            //}
//        }
//    }
//}
