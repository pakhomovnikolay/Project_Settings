using Project_Settings.Infrastructure.Commands;
using Project_Settings.Models.LayotRack;
using Project_Settings.ViewModels.Default;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_Settings.ViewModels
{
    public class MainWindowsViewModel : ViewModel
    {

        private readonly Application CurrApp = Application.Current;

        #region Контроль состояний


        private bool _ChangeApllication;
        public bool ChangeApllication
        {
            get => _ChangeApllication;
            set => Set(ref _ChangeApllication, value);
        }


        private string _myPath = Environment.CurrentDirectory + "/MyResource/Jsons/Grid.json";

        public string MyPath
        {
            get => _myPath;
            set => Set(ref _myPath, value);
        }

        private string _myVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public string MyVersion
        {
            get => _myVersion;
            set => Set(ref _myVersion, value);
        }

        private string _myTitle = "Конфигуратор проекта";

        public string MyTitle
        {
            get => _myTitle;
            set => Set(ref _myTitle, value);
        }
        #endregion

        #region Смена темы
        private bool _flBlackTheames = false;
        private bool _flWhiteTheames = false;

        /// <summary>Установить темную тему для приложения</summary>
        public bool flBlackTheames
        {
            get => _flBlackTheames;
            set => Set(ref _flBlackTheames, value);
        }

        /// <summary>Установить светлую тему для приложения</summary>
        public bool flWhiteTheames
        {
            get => _flWhiteTheames;
            set => Set(ref _flWhiteTheames, value);
        }

        private SolidColorBrush _ResultBackground = (SolidColorBrush)Application.Current.TryFindResource("ResultBackground");
        private SolidColorBrush _ResultForeground = (SolidColorBrush)Application.Current.TryFindResource("ResultForeground");
        private SolidColorBrush _ResultkMenuItemBackground = (SolidColorBrush)Application.Current.TryFindResource("ResultkMenuItemBackground");
        private SolidColorBrush _ResultMenuItemForeground = (SolidColorBrush)Application.Current.TryFindResource("ResultMenuItemForeground");
        private SolidColorBrush _ResultBorderBrush = (SolidColorBrush)Application.Current.TryFindResource("ResultBorderBrush");
        private SolidColorBrush _ResultTreeViewItemBackground = (SolidColorBrush)Application.Current.TryFindResource("ResultTreeViewItemBackground");
        private SolidColorBrush _ResultTreeViewItemForeground = (SolidColorBrush)Application.Current.TryFindResource("ResultTreeViewItemForeground");

        private Color _ResultBackgroundListBox = (Color)Application.Current.TryFindResource("ResultBackgroundListBox");
        private Color _ResultForegroundListBox = (Color)Application.Current.TryFindResource("ResultForegroundListBox");
        private Color _ResultBorderBrushListBox = (Color)Application.Current.TryFindResource("ResultBorderBrushListBox");

        public SolidColorBrush ResultBackground
        {
            get => _ResultBackground;
            set => Set(ref _ResultBackground, value);
        }

        public SolidColorBrush ResultForeground
        {
            get => _ResultForeground;
            set => Set(ref _ResultForeground, value);
        }

        public SolidColorBrush ResultkMenuItemBackground
        {
            get => _ResultkMenuItemBackground;
            set => Set(ref _ResultkMenuItemBackground, value);
        }

        public SolidColorBrush ResultMenuItemForeground
        {
            get => _ResultMenuItemForeground;
            set => Set(ref _ResultMenuItemForeground, value);
        }

        public SolidColorBrush ResultBorderBrush
        {
            get => _ResultBorderBrush;
            set => Set(ref _ResultBorderBrush, value);
        }

        public SolidColorBrush ResultTreeViewItemBackground
        {
            get => _ResultTreeViewItemBackground;
            set => Set(ref _ResultTreeViewItemBackground, value);
        }

        public SolidColorBrush ResultTreeViewItemForeground
        {
            get => _ResultTreeViewItemForeground;
            set => Set(ref _ResultTreeViewItemForeground, value);
        }

        public Color ResultBackgroundListBox
        {
            get => _ResultBackgroundListBox;
            set => Set(ref _ResultBackgroundListBox, value);
        }

        public Color ResultForegroundListBox
        {
            get => _ResultForegroundListBox;
            set => Set(ref _ResultForegroundListBox, value);
        }

        public Color ResultBorderBrushListBox
        {
            get => _ResultBorderBrushListBox;
            set => Set(ref _ResultBorderBrushListBox, value);
        }

        private void ChangeTheames()
        {

            ResultBackground = (SolidColorBrush)(flBlackTheames ? CurrApp.TryFindResource("BlackBackground") : CurrApp.TryFindResource("WhiteBackground"));
            ResultForeground = (SolidColorBrush)(flBlackTheames ? CurrApp.TryFindResource("BlackForeground") : CurrApp.TryFindResource("WhiteForeground"));
            ResultkMenuItemBackground = (SolidColorBrush)(flBlackTheames ? CurrApp.TryFindResource("BlackMenuItemBackground") : CurrApp.TryFindResource("WhitekMenuItemBackground"));
            ResultMenuItemForeground = (SolidColorBrush)(flBlackTheames ? CurrApp.TryFindResource("BlackMenuItemForeground") : CurrApp.TryFindResource("WhiteMenuItemForeground"));
            ResultBorderBrush = (SolidColorBrush)(flBlackTheames ? CurrApp.TryFindResource("BlackBorderBrush") : CurrApp.TryFindResource("WhiteBorderBrush"));
            ResultTreeViewItemBackground = (SolidColorBrush)(flBlackTheames ? CurrApp.TryFindResource("BlackTreeViewItemBackground") : CurrApp.TryFindResource("WhiteTreeViewItemBackground"));
            ResultTreeViewItemForeground = (SolidColorBrush)(flBlackTheames ? CurrApp.TryFindResource("BlackTreeViewItemForeground") : CurrApp.TryFindResource("WhiteTreeViewItemForeground"));
            ResultBackgroundListBox = (Color)(flBlackTheames ? CurrApp.TryFindResource("BlackBackgroundListBox") : CurrApp.TryFindResource("WhiteBackgroundListBox"));
            ResultForegroundListBox = (Color)(flBlackTheames ? CurrApp.TryFindResource("BlackForegroundListBox") : CurrApp.TryFindResource("WhiteForegroundListBox"));
            ResultBorderBrushListBox = (Color)(flBlackTheames ? CurrApp.TryFindResource("BlackBorderBrushListBox") : CurrApp.TryFindResource("WhiteBorderBrushListBox"));

            CurrApp.Resources["ResultBackground"] = ResultBackground;
            CurrApp.Resources["ResultForeground"] = ResultForeground;
            CurrApp.Resources["ResultkMenuItemBackground"] = ResultkMenuItemBackground;
            CurrApp.Resources["ResultMenuItemForeground"] = ResultMenuItemForeground;
            CurrApp.Resources["ResultBorderBrush"] = ResultBorderBrush;
            CurrApp.Resources["ResultTreeViewItemBackground"] = ResultTreeViewItemBackground;
            CurrApp.Resources["ResultTreeViewItemForeground"] = ResultTreeViewItemForeground;
            CurrApp.Resources["ResultBackgroundListBox"] = ResultBackgroundListBox;
            CurrApp.Resources["ResultForegroundListBox"] = ResultForegroundListBox;
            CurrApp.Resources["ResultBorderBrushListBox"] = ResultBorderBrushListBox;
        }
        #endregion

        #region Команды
        /// <summary>
        /// Команда добавить строку
        /// </summary>
        public ICommand CmdAddRow{ get; }

        private bool CanCmdAddRowExecute(object p) => true;

        private void OnCmdAddRowExecuted(object p)
        {
            //var index = MyDataGridItems.IndexOf(SelectedSheets);


            //if (SelectedSheets.DataTables == null)
            //{
            //    SelectedSheets.DataTables = new();
            //}
            //SelectedSheets.DataTables.Columns.Add("Номер\nкоризны");
            //SelectedSheets.DataTables.Columns.Add("Название шкафа\nНомер шкафа");
            //SelectedSheets.DataTables.Columns.Add("Номер\nкорзины\nв шкафу");
            //SelectedSheets.DataTables.Columns.Add("Наименование модуля. Выбирайте модуль из списка, чтобы наименование было верным.");
            //SelectedSheets.DataTables.Rows.Add();


            ////for (int i = 0; i < 3; i++)
            ////{
            ////    if (SelectedSheets.DataTables == null)
            ////    {
            ////        SelectedSheets.DataTables = new();
            ////        SelectedSheets.DataTables.Columns.Add();
            ////        SelectedSheets.DataTables.NewRow();
            ////    }
            ////    else
            ////    {
            ////        SelectedSheets.DataTables.Columns.Add();
            ////        SelectedSheets.DataTables.Rows.Add();
            ////    }
            ////}
            //MyDataGridItems[index] = SelectedSheets;
            //SelectedSheets = MyDataGridItems[index];
            //MySheetsConfig.Sheet[index] = SelectedSheets;
        }

        /// <summary>
        /// Команда сохранить проект
        /// </summary>
        public ICommand CmdSaveProject { get; }

        private bool CanCmdSaveProjectExecute(object p) => true;

        private void OnCmdSaveProjectExecuted(object p)
        {
            WriteMappingFileGridSheets(MyPath, MySheetsConfig);
        }


        /// <summary>
        /// Команда на смену светлой темы
        /// </summary>
        public ICommand CmdSetBlackTheames { get; }

        private bool CanCmdSetBlackTheamesExecute(object p) => true;

        private void OnCmdSetBlackTheamesExecuted(object p)
        {
            flBlackTheames = true;
            flWhiteTheames = false;
            ChangeTheames();
        }

        /// <summary>
        /// Команда на смену темной темы
        /// </summary>
        public ICommand CmdSetWhiteTheames { get; }

        private bool CanCmdSetWhiteTheamesExecute(object p) => true;

        private void OnCmdSetWhiteTheamesExecuted(object p)
        {
            flBlackTheames = false;
            flWhiteTheames = true;
            ChangeTheames();
        }

        /// <summary>
        /// Команда на закрытие приложения
        /// </summary>
        public ICommand CmdCloseApp { get; }
        private bool CanCmdCloseAppExecute(object p) => true;

        private void OnCmdCloseAppExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
            CurrApp.Shutdown();
        }

        /// <summary>
        /// Коамнда развернуть приложение
        /// </summary>
        public ICommand CmdMaximized { get; }
        private bool CanCmdMaximizedExecute(object p) => true;

        private void OnCmdMaximizedExecuted(object p)
        {
            if (CurrApp.MainWindow.WindowState == WindowState.Maximized)
            {
                CurrApp.MainWindow.WindowState = WindowState.Normal;
                return;
            }
            CurrApp.MainWindow.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Коамнда свернуть приложение
        /// </summary>
        public ICommand CmdMinimized { get; }
        private bool CanCmdMinimizedExecute(object p) => true;

        private void OnCmdMinimizedExecuted(object p)
        {
            CurrApp.MainWindow.WindowState = WindowState.Minimized;
        }


        //private bool CanCmdCloseAppExecute(object p) => true;

        //public ContentControl CmdCloseApp  { get; }

        //private void Window_Closing(object Sender, CancelEventArgs E)
        //{
        //    if (MessageBox.Show("Вы действительно хотите выйти?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
        //    {
        //        E.Cancel = true;
        //        return;
        //    }
        //    Application.Current.Shutdown();
        //}
        #endregion

        #region Набор данных для DataGrid
        public ObservableCollection<MapSheets> MyDataGridItems { get; }
        public Sheets MySheetsConfig { get; }

        /// <summary>
        /// Данные выбранного листа
        /// </summary>
        private Rack _SelectedSheets = new();
        public Rack SelectedSheets
        {
            get => _SelectedSheets;
            set => Set(ref _SelectedSheets, value);
        }

        /// <summary>
        /// Сохраняем данные
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dt"></param>
        private void WriteMappingFileGridSheets(string fileName, Sheets dt)
        {
            

            //Sheets _Sheets = new();
            //List<MapSheets> _MapSheets = new();
            //List<MapColumns> _MapColumns = new();

            

            //foreach (var _Sheet in dt.Sheet)
            //{

            //    SelectedSheets.DataTables.WriteXml("people.xml");



            //    //dataGrid.ItemsSource = IList<_Sheet.DataTables>;

            //    //dataTable.

            //    //using FileStream fs = new("people.xml", FileMode.OpenOrCreate);

            //    //    dataTable.WriteXml(fs);




            //    //var ItemMapColumns = new MapColumns
            //    //{
            //    //    Col = item.DataTables.Locale.Name,
            //    //    Row = item.DataTables.Locale.ToString()
            //    //};

            //    //_MapColumns.Add(ItemMapColumns);

            //    var ItemMapSheets = new MapSheets
            //    {
            //        Columns = _MapColumns,
            //        CountRow = _Sheet.CountRow,
            //        Name = _Sheet.Name,
            //        NameMsg = _Sheet.NameMsg,
            //    };
            //    _MapSheets.Add(ItemMapSheets);
            //}

            //_Sheets.Sheet = _MapSheets;
            //_Sheets.LastSelectIntex = dt.LastSelectIntex;

            //Sheets _Sheets;
            //List<MapSheets> _MapSheets;
            //_MapSheets = new List<MapSheets>();
            //_Sheets = new();

            //foreach (var item in dt.Sheet)
            //{
            //    var io = item.DataTables.Rows[0].ItemArray;


            //    //foreach (var io in item.DataTables.ImportRow())
            //    //{
            //    //    io.
            //    //}
            //}



            //foreach (var item in dt.Sheet)
            //{
            //    var ItemMapSheets = new MapSheets
            //    {
            //        CountRow = item.CountRow,
            //        Name = item.Name,
            //        NameMsg = item.NameMsg,
            //        Columns = item.Columns,

            //    };





            //    _MapSheets.Add(ItemMapSheets);
            //}
            //var groups = new Sheets
            //{
            //    LastSelectIntex = JsonData.LastSelectIntex,
            //    Sheet = _MapSheets
            //};






            //DataGrid _DataGrid = new();
            ////MapSheets _MapSheets = new;
            //Sheets _Sheets;
            //_Sheets = new Sheets();
            //MapSheets _MapSheets = new List<MapSheets>();


            //foreach (var item in dt.Sheet)
            //{
            //    var _MapSheets = new MapSheets
            //    {
            //        CountRow = item.CountRow,
            //        DataTables = item.DataTables,
            //        Name = item.Name,
            //        NameMsg = item.NameMsg,
            //    };
            //    _Sheets.Sheet.Add(_MapSheets);


            //    ////_DataGrid.ItemsSource = item.DataTables.Rows;
            //    //_MapSheets.Name = item.Name;
            //    //_MapSheets.NameMsg = item.NameMsg;
            //    //_MapSheets.CountRow = item.CountRow;



            //    //item.Columns.Add();



            //}

            //_Sheets.LastSelectIntex = dt.LastSelectIntex;


            //foreach (var item in _DataGrid.ItemsSource)
            //{

            //}


            //Sheets _Sheets = new();
            //
            //MapColumns _MapColumns = new();

            //_Sheets.LastSelectIntex = dt.LastSelectIntex;

            //foreach (var item in dt.Sheet)
            //{
            //    for (int i = 0; i < item.DataTables.Rows.Count; i++)
            //    {
            //        _MapColumns.Row = item.DataTables.Rows[i].ToString();

            //    }




            //    //foreach (var it in )
            //    //{

            //    //}
            //}




            //_Sheets.Sheet




            //Sheets _Sheets = new();
            //MapSheets _MapSheets = new();
            //MapColumns _MapColumns = new();

            //_Sheets.LastSelectIntex = dt.LastSelectIntex;
            //foreach (var item in dt.Sheet)
            //{
            //    _MapSheets.NameMsg = item.NameMsg;
            //    _MapSheets.Name = item.Name;
            //    foreach (var Rows in item.DataTables.Columns)
            //    {
            //        _MapColumns.Col = Rows.ToString();
            //    }
            //}


            //var options = new JsonSerializerOptions
            //{
            //    AllowTrailingCommas = true,
            //    WriteIndented = true
            //};
            //byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(_Sheets, options);
            //File.WriteAllBytes(fileName, jsonUtf8Bytes);
        }

        /// <summary>
        /// Получаем данные
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static Sheets ReadMappingFileGridSheets(string fileName)
        {
            byte[] jsonUtf8Bytes = File.ReadAllBytes(fileName);
            var readOnlySpan = new ReadOnlySpan<byte>(jsonUtf8Bytes);
            Sheets? mapping = JsonSerializer.Deserialize<Sheets>(readOnlySpan);
            return mapping;
        }
        #endregion

        #region Инициализация данных
        public MainWindowsViewModel()
        {
            MyDataGridItems = new();
            MySheetsConfig = new();
            flWhiteTheames = true;
            CmdSetBlackTheames = new RelayCommand(OnCmdSetBlackTheamesExecuted, CanCmdSetBlackTheamesExecute);
            CmdSetWhiteTheames = new RelayCommand(OnCmdSetWhiteTheamesExecuted, CanCmdSetWhiteTheamesExecute);
            CmdCloseApp = new RelayCommand(OnCmdCloseAppExecuted, CanCmdCloseAppExecute);
            CmdMaximized = new RelayCommand(OnCmdMaximizedExecuted, CanCmdMaximizedExecute);
            CmdMinimized = new RelayCommand(OnCmdMinimizedExecuted, CanCmdMinimizedExecute);
            CmdSaveProject = new RelayCommand(OnCmdSaveProjectExecuted, CanCmdSaveProjectExecute);
            CmdAddRow = new RelayCommand(OnCmdAddRowExecuted, CanCmdAddRowExecute);

            ChangeTheames();




            //var JsonData = ReadMappingFileGridSheets(MyPath);
            //List<MapSheets> _MapSheets;
            //_MapSheets = new List<MapSheets>();
            //foreach (var item in JsonData.Sheet)
            //{
            //    var ItemMapSheets = new MapSheets
            //    {
            //        CountRow = item.CountRow,
            //        DataTables = item.DataTables,
            //        Name = item.Name,
            //        NameMsg = item.NameMsg,
            //        Columns = item.Columns
            //    };
            //    _MapSheets.Add(ItemMapSheets);
            //}
            //var groups = new Sheets
            //{
            //    LastSelectIntex = JsonData.LastSelectIntex,
            //    Sheet = _MapSheets
            //};
            //MySheetsConfig = groups;
            //MyDataGridItems = new ObservableCollection<MapSheets>(_MapSheets);
            //SelectedSheets = MyDataGridItems[MySheetsConfig.LastSelectIntex];
            #endregion



            ////Groups = new ObservableCollection<Group>(groups);

            ////MyDataGridItems.Add(JsonData);

            //





            //GridItems.NameMsg = "";
            //GridItems.Name = "";
            //GridItems.Columns = new string[] { "Name" };
            //GridItems.DataTables = new();
            //GridItems.CountRow




            //MyDataTable = new();
            //MyDataTable.ItemsSource = GridItems.Columns.ToList();
            //MyDataTable.AutoGenerateColumns = false;
            //MyDataTable.ColumnWidth = 70;
            //MyDataTable.RowHeaderWidth = 30;
            //MyDataTable.VerticalGridLinesBrush = Brushes.DarkGray;
            //MyDataTable.AlternatingRowBackground = Brushes.LightGray;
            //foreach (var io in GridItems.Columns)
            //{
            //    MyDataTable.Columns.Add(io.Col);

            //    MyDataTable.Rows.Add(io.Col);


            //    //MyDataTable.Columns.Add(new DataGridTextColumn
            //    //{
            //    //    Header = io.Col.ToString()

            //    //});

            //    //MyDataTable.Items.Add(new DataGridRow
            //    //{
            //    //    Header = index_row++
            //    //});
            //}

            //MyDataGridItems = new ObservableCollection<Sheets>(JsonData);

            //GridItems.DataTables = new(MyDataTable);

            //foreach (MapColumn Items in GridItems.Columns)
            //{
            //    //dt.Columns.Add(Items.Col);
            //    dt.Columns.Add(Items.Col);
            //    dt.Rows.Add(index_row);
            //    index_row++;
            //}

            //foreach (var DGridItems in MyDataGridItems)
            //{
            //    DGridItems.DataTables = (IList<DataTable>)MyDataTable.ItemsSource;
            //}



            //MyDataTable.ItemsSource = dt.Columns.to;
            //MyDataGridItems = JsonData;



            //var groups = Enumerable.Range(1, JsonData.Sheet.Count).Select(i => new MapSheet
            //{
            //    DataTables = new ObservableCollection<DataTable>((IEnumerable<DataTable>)MyDataTable)
            //});


            //MyDataGridItems.Sheet.Add((MapSheet)groups);





            //MyDataGridItems.Sheet= (IList<DataGrid>)MyDataGrid;

            //int index = 1;
            //foreach (var ioMap in ioMapData.Sheet)
            //{
            //    foreach (var io in ioMap.Column)
            //    {
            //        var _MapColumn = Enumerable.Range(1, index).Select(i => new MapColumn
            //        {
            //            Item = io.Item

            //        });

            //        var _MapSheet = Enumerable.Range(1, index).Select(i => new MapSheet
            //        {
            //            Column = new ObservableCollection<MapColumn>(_MapColumn),
            //            CountRow = ioMap.CountRow,
            //            NameMsg = ioMap.NameMsg,
            //            Name = ioMap.Name
            //        });


            //    }

            //    index++;
            //}

            //MyDataGridItems = new ObservableCollection<MapSheet>(ioMapData.Sheet);


            //IEnumerable<MapSheet> _MapColumn;
            //var _MapColumn = new List<MapColumn>();
            //var _MapSheet = new List<MapSheet>();
            //foreach (var ioMap in ioMapData.Sheet)
            //{
            //    foreach (var io in ioMap.Column)
            //    {
            //        var _MapColumn = Enumerable.Range(1, ioMap.Column.Count).Select(i => new MapColumn
            //        {
            //            Item = io.Item
            //        });

            //        var _MapSheet = Enumerable.Range(1, ioMapData.Sheet.Count).Select(i => new MapSheet
            //        {
            //            Column = new ObservableCollection<MapColumn>(_MapColumn),
            //            CountRow = ioMap.CountRow,
            //            NameMsg = ioMap.NameMsg,
            //            Name = ioMap.Name
            //        });

            //        MyDataGridItems = new ObservableCollection<MapSheet>(_MapSheet);
            //    }
            //}


            //
            //{
            //    Name = $"Name {student_index}",
            //    Surname = $"Surname {student_index}",
            //    Patronymic = $"Patronymic {student_index++}",
            //    Birthday = DateTime.Now,
            //    Rating = 0
            //});



            //var _TreeViewItemSource = new List<object>();
            //var _TreeViewItemMsgSource = new List<object>();

            //foreach (var io in ioMapData.Sheet)
            //{
            //    _TreeViewItemSource.Add(io.Name);
            //}
            //TreeViewItemSource = _TreeViewItemSource.ToArray();

            //foreach (var io in ioMapData.Sheet)
            //{
            //    if (!string.IsNullOrEmpty(io.NameMsg))
            //    {
            //        _TreeViewItemMsgSource.Add(io.NameMsg);
            //    }
            //}
            //TreeViewItemMsgSource = _TreeViewItemMsgSource.ToArray();

            // ------------------------------------------------------------------------------------------------------------- //
            //var data_list = new List<object>();
            //foreach (var ioMap in ioMapData.Sheet)
            //{
            //    foreach (var io in ioMap.Column)
            //    {
            //        data_list.Add(io.Item);
            //    }
            //}




            //MyDataGridItems = data_list.ToArray();

            //var students = Enumerable.Range(1, 10).Select(i => new Student
            //{
            //    Name = $"Name {student_index}",
            //    Surname = $"Surname {student_index}",
            //    Patronymic = $"Patronymic {student_index++}",
            //    Birthday = DateTime.Now,
            //    Rating = 0
            //});






            //DataGrid _DataGridItems = new();
            //DataGrid _DataGridItems = new();
            //foreach (var ioMap in ioMapDataGrid.Columns)
            //{

            //    var col = new DataGridTextColumn
            //    {
            //        Header = ioMap.Item
            //    };

            //    _DataGridItems.Columns.Add(col);

            //    //MyDataGridItems = new ObservableCollection<MapDataGrid>(groups);
            //}

            ////var rowData = Enumerable.Range(0, ioMapDataGrid.Columns.Count).ToList();
            //MyDataGridItems = _DataGridItems.ItemsSource;

            //for (int i = 0; i < N; i++)
            //{
            //    var col = new DataGridTextColumn
            //    {
            //        Header = i.ToString(),
            //        Binding = new Binding("[" + i + "]"),
            //        IsReadOnly = true,
            //        Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            //    };

            //    dataGrid.Columns.Add(col);
            //}

            //var rowData = Enumerable.Range(0, N).ToList();
            //dataGrid.Items.Add(rowData);

            ////





            //MyDataGridItems = _DataGridItems.ToArray();

            //var student_index = 1;
            //var students = Enumerable.Range(1, 10).Select(i => new Student
            //{
            //    Name = $"Name {student_index}",
            //    Surname = $"Surname {student_index}",
            //    Patronymic = $"Patronymic {student_index++}",
            //    Birthday = DateTime.Now,
            //    Rating = 0
            //});



            // DataGrid DataList = new();





            //DataGridTextColumn textColumn = new DataGridTextColumn();
            //textColumn.Header = "First Name";
            //textColumn.Binding = new Binding("FirstName");
            //DataList.Columns.Add(textColumn);


            //DataGrid data_list = new();
            //foreach (var ioMap in ioMapDataGrid.Columns)
            //{
            //    DataGridTextColumn textColumn = new DataGridTextColumn();
            //    textColumn.Header = ioMap.Item;
            //    data_list.Columns.Add(textColumn);
            //}

            //MyDataGridItems = data_list.Columns.ToList();
            //data_list.Add(ioMapDataGrid);


            //var _DataGridItems = new List<MapDataGrid>();

            //var dgItems = new MapDataGrid
            //{
            //    Columns = ioMapDataGrid.Columns
            //};

            //_DataGridItems.Add(dgItems);

            //DataList.Items.Add(_TreeViewItemSource);


            //DataList.ItemsSource = data_list.ItemsSource;

            //DataList = new ()data_list

            //DataList.ItemsSource = data_list.ItemsSource;

            //int numCol = 0;
            //foreach (var ioMap in ioMapDataGrid.Columns)
            //{
            //    //data_list.Add(numCol.ToString());


            //    //data_list.Add(numCol);
            //    //numCol++;

            //    //var groups = Enumerable.Range(1, 20).Select(i => new MapDataGrid
            //    //{
            //    //    Columns = (IList<ConfigDataGrid>)ioMap
            //    //});



            //    //data_list.Columns.Add();

            //    //var groups = new DataGrid
            //    //{
            //    //    Column = ObservableCollection<DataGridColumn>(ioMap.Item)
            //    //    //Columns = (IList<ConfigDataGrid>)ioMap
            //    //};

            //    //

            //    //    var groups = new MapDataGrid
            //    //{
            //    //    Columns = ioMap.Item
            //    //    });

            //    //    data_list.Add(ioMap.Item);


            //    //data_list.Add(io.Value);

            //    //var dgItems = new ConfigDataGrid
            //    //{
            //    //    Value = io.Value,
            //    //    Item = io.Item,
            //    //    ColumnSpawn = io.ColumnSpawn
            //    //};
            //    //data_list.Add(dgItems);
            //}
            //Groups = new ObservableCollection<DataGrid>(groups);
            //MyDataGridItems.Add
            //MyDataGridItems.Add(numCol);

            //DataGrid data_list = new();

            //MyDataGridItems = new ObservableCollection<DataGrid>((IEnumerable<DataGrid>)data_list);


            //_DataGridItems.Add(ioMapDataGrid);

            //var _DataGridItems = new List<GridSheets>();


            //_DataGridItems.Add(ioMapDataGrid);
            //MyDataGridItems = new ObservableCollection<GridSheets>(_DataGridItems);


            //_DataGridItems.Add(ioMapDataGrid.Sheet);







            //var groups => new GridSheets
            //{
            //    Sheet = new ObservableCollection<GridSheets>(ioMapDataGrid.Sheet)
            //});

            //MyDataGridItems = new ObservableCollection<GridSheets>();


            //_DataGridItems.Add((GridSheets)ioMapDataGrid.Sheet);
            //MyDataGridItems = new ObservableCollection<GridSheets>(_DataGridItems);

        }
    }
}
