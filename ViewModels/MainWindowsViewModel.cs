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


        private string _NewSelectedSheetName;
        public string NewSelectedSheetName
        {
            get => _NewSelectedSheetName;
            set => Set(ref _NewSelectedSheetName, value);
        }


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
        public ICommand CmdChangeSelectedSheetName { get; }

        private bool CanCmdChangeSelectedSheetNameExecute(object p) => true;

        private void OnCmdChangeSelectedSheetNameExecuted(object p)
        {
            //SelectedSheets.Name = NewSelectedSheetName;
        }



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

            //DataRow row = SelectedSheets.DataTables.NewRow();
            //row["Номер\nкоризны"] = "A";
            //row["Название шкафа\nНомер шкафа"] = "A";
            //row["Номер\nкорзины\nв шкафу"] = "A";
            //row["Наименование модуля. Выбирайте модуль из списка, чтобы наименование было верным."] = "B";

            //SelectedSheets.DataTables.Rows.Add(row);


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
            //WriteMappingFileGridSheets(MyPath, MySheetsConfig);
        }

        /// <summary>
        /// Команда октрыть проект
        /// </summary>
        public ICommand CmdOpenFile { get; }

        private bool CanCmdOpenFileExecute(object p) => true;

        private void OnCmdOpenFileExecuted(object p)
        {
            ReadMappingFileGridSheets(MyPath);
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
        #endregion

        #region Набор данных для DataGrid
        public ObservableCollection<MapSheets> MyDataGridItems { get; }
        public Sheets MySheetsConfig { get; }

        Sheets groups = new();
        List<MapSheets> _MapSheets = new();

        /// <summary>
        /// Данные выбранного листа
        /// </summary>
        private MapSheets _SelectedSheets = new();
        public MapSheets SelectedSheets
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

            List<MapSheets> _MapSheets = new();
            List<MapColumns> _MapColumn = new();
            List<MapRow> _MapRow = new();
            string column = "";

            foreach (MapSheets _Sheet in dt.Sheet)
            {
                for (int i = 0; i < _Sheet.DataTables.Columns.Count; i++)
                {
                    column = _Sheet.DataTables.Columns[i].ColumnName;
                    var mapColumns = new MapColumns
                    {
                        Item = column
                    };
                    _MapColumn.Add(mapColumns);
                }
                for (int i = 0; i < _Sheet.DataTables.Rows.Count; i++)
                {
                    int countRow = 0;
                    foreach (var ItemArray in _Sheet.DataTables.Rows[i].ItemArray)
                    {
                        string value = "";
                        if (!string.IsNullOrEmpty(ItemArray.ToString())) value = ItemArray.ToString();
                        var mapRow = new MapRow
                        {
                            Column = _Sheet.DataTables.Columns[countRow].ColumnName,
                            Value = value
                        };
                        countRow++;
                        _MapRow.Add(mapRow);
                    }
                }

                var mapSheets = new MapSheets
                {
                    Columns = _MapColumn,
                    CountRow = _Sheet.DataTables.Rows.Count,
                    DataTables = null,
                    Rows = _MapRow,
                    Name = _Sheet.Name,
                    NameMsg = _Sheet.NameMsg
                };
                _MapSheets.Add(mapSheets);
            }

            var groups = new Sheets
            {
                LastSelectIntex = dt.LastSelectIntex,
                Sheet = _MapSheets
            };
            Sheets sheets = groups;

            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                WriteIndented = true
            };
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(sheets, options);
            File.WriteAllBytes(fileName, jsonUtf8Bytes);
        }

        /// <summary>
        /// Получаем данные
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        //private static Sheets ReadMappingFileGridSheets(string fileName)
        //{
        //    byte[] jsonUtf8Bytes = File.ReadAllBytes(fileName);
        //    var readOnlySpan = new ReadOnlySpan<byte>(jsonUtf8Bytes);
        //    Sheets mapping = JsonSerializer.Deserialize<Sheets>(readOnlySpan);
        //    return mapping;
        //}
        private void ReadMappingFileGridSheets(string fileName)
        {
            byte[] jsonUtf8Bytes = File.ReadAllBytes(fileName);
            var readOnlySpan = new ReadOnlySpan<byte>(jsonUtf8Bytes);
            Sheets JsonData = JsonSerializer.Deserialize<Sheets>(readOnlySpan);

            DataRow _row;
            foreach (var item in JsonData.Sheet)
            {
                DataTable _DataTable = new();

                foreach (var Column in item.Columns)
                {
                    _DataTable.Columns.Add(Column.Item);
                }

                for (int i = 0; i < item.CountRow; i++)
                {
                    _row = _DataTable.NewRow();
                    _DataTable.Rows.Add(_row);
                    foreach (var Row in item.Rows)
                    {
                        _row = _DataTable.Rows[i];
                        _row[Row.Column] = Row.Value;
                    }
                }

                var ItemMapSheets = new MapSheets
                {
                    CountRow = item.CountRow,
                    DataTables = _DataTable,
                    Name = item.Name,
                    NameMsg = item.NameMsg,
                    Columns = item.Columns,
                    Rows = item.Rows
                };
                _MapSheets.Add(ItemMapSheets);
            }
            groups = new Sheets
            {
                LastSelectIntex = JsonData.LastSelectIntex,
                Sheet = _MapSheets
            };

        }


        #endregion

        #region Инициализация данных
        public MainWindowsViewModel()
        {
            //MyDataGridItems = new();
            //MySheetsConfig = new();
            flWhiteTheames = true;
            CmdSetBlackTheames = new RelayCommand(OnCmdSetBlackTheamesExecuted, CanCmdSetBlackTheamesExecute);
            CmdSetWhiteTheames = new RelayCommand(OnCmdSetWhiteTheamesExecuted, CanCmdSetWhiteTheamesExecute);
            CmdCloseApp = new RelayCommand(OnCmdCloseAppExecuted, CanCmdCloseAppExecute);
            CmdMaximized = new RelayCommand(OnCmdMaximizedExecuted, CanCmdMaximizedExecute);
            CmdMinimized = new RelayCommand(OnCmdMinimizedExecuted, CanCmdMinimizedExecute);
            CmdSaveProject = new RelayCommand(OnCmdSaveProjectExecuted, CanCmdSaveProjectExecute);
            CmdAddRow = new RelayCommand(OnCmdAddRowExecuted, CanCmdAddRowExecute);
            CmdOpenFile = new RelayCommand(OnCmdOpenFileExecuted, CanCmdOpenFileExecute);
            CmdChangeSelectedSheetName = new RelayCommand(OnCmdChangeSelectedSheetNameExecuted, CanCmdChangeSelectedSheetNameExecute);

            ChangeTheames();
            ReadMappingFileGridSheets(MyPath);

            MySheetsConfig = groups;
            MyDataGridItems = new ObservableCollection<MapSheets>(_MapSheets);
            SelectedSheets = MyDataGridItems[MySheetsConfig.LastSelectIntex];

            //var JsonData = ReadMappingFileGridSheets(MyPath);
            //List<MapSheets> _MapSheets = new();
            //int countRow = 0;
            //string column = "";

            //foreach (var item in JsonData.Sheet)
            //{
            //    DataTable _DataTable = new();
            //    DataRow _row;
            //    foreach (var Column in item.Columns)
            //    {
            //        _DataTable.Columns.Add(Column.Item);
            //    }

            //    for (int i = 0; i < item.CountRow; i++)
            //    {
            //        _row = _DataTable.NewRow();
            //        _DataTable.Rows.Add(_row);
            //    }

            //    foreach (var Row in item.Rows)
            //    {
            //        if (Row.Column != column)
            //        {
            //            countRow = 0;
            //            column = Row.Column;
            //        }
            //        _row = _DataTable.Rows[countRow];
            //        _row[Row.Column] = Row.Value;
            //        countRow++;
            //    }

            //    var ItemMapSheets = new MapSheets
            //    {
            //        CountRow = item.CountRow,
            //        DataTables = _DataTable,
            //        Name = item.Name,
            //        NameMsg = item.NameMsg,
            //        Columns = item.Columns,
            //        Rows = item.Rows
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
        }
    }
}
