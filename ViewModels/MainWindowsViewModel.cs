using Project_Settings.Infrastructure.Commands;
using Project_Settings.Models;
using Project_Settings.ViewModels.Default;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_Settings.ViewModels
{
    public class MainWindowsViewModel : ViewModel
    {
        private readonly Application CurrApp = Application.Current;

        public MainWindowsViewModel()
        {
            // Команды
            CmdSetBlackTheames = new RelayCommand(OnCmdSetBlackTheamesExecuted, CanCmdSetBlackTheamesExecute);
            CmdSetWhiteTheames = new RelayCommand(OnCmdSetWhiteTheamesExecuted, CanCmdSetWhiteTheamesExecute);
            CmdCloseApp = new RelayCommand(OnCmdCloseAppExecuted, CanCmdCloseAppExecute);
            CmdCreateNewList = new RelayCommand(OnCmdCreateNewListExecuted, CanCmdCreateNewListExecute);
        }

        #region Параметры
        private MapSheets _SelectedSheets = new();
        public MapSheets SelectedSheets
        {
            get => _SelectedSheets;
            set => Set(ref _SelectedSheets, value);
        }

        private ObservableCollection<MapSheets> _MyMapSheets = new();
        public ObservableCollection<MapSheets> MyMapSheets
        {

            get => _MyMapSheets;
            set => Set(ref _MyMapSheets, value);
        }

        private DataProject _MyDataProject = new();
        public DataProject MyDataProject
        {
            get => _MyDataProject;
            set => Set(ref _MyDataProject, value);
        }

        private DataProject _JsonData = new();
        public DataProject JsonData
        {
            get => _JsonData;
            set => Set(ref _JsonData, value);
        }


        private string _myPath = Environment.CurrentDirectory;
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

        #region Смена темы
        private bool _flBlackTheames = false;

        /// <summary>Установить темную тему для приложения</summary>
        public bool flBlackTheames
        {
            get => _flBlackTheames;
            set
            {
                if (Set(ref _flBlackTheames, value))
                {
                    ChangeTheames();
                }
            }
        }

        private bool _flWhiteTheames;
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
        #endregion
        #endregion

        #region Команды


        public ICommand CmdCreateNewList { get; }
        private bool CanCmdCreateNewListExecute(object p) => true;
        private async void OnCmdCreateNewListExecuted(object p)
        {
            if (!File.Exists(MyPath)) return;

            using (FileStream fs = new FileStream(MyPath, FileMode.OpenOrCreate))
            {
                JsonData = await JsonSerializer.DeserializeAsync<DataProject>(fs).ConfigureAwait(true);
            };
            CreateNewList();
        }



        /// <summary>
        /// Команда добавить строку
        /// </summary>
        //public ICommand CmdChangeSelectedSheetName { get; }

        //private bool CanCmdChangeSelectedSheetNameExecute(object p) => true;

        //private void OnCmdChangeSelectedSheetNameExecuted(object p)
        //{
        //    //SelectedSheets.Name = NewSelectedSheetName;
        //}



        /// <summary>
        /// Команда добавить строку
        /// </summary>
        //public ICommand CmdAddRow { get; }

        //private bool CanCmdAddRowExecute(object p) => true;

        //private void OnCmdAddRowExecuted(object p)
        //{

        //    //var index = MyDataGridItems.IndexOf(SelectedSheets);
        //    //if (SelectedSheets.DataTables == null)
        //    //{
        //    //    SelectedSheets.DataTables = new();
        //    //}

        //    //SelectedSheets.DataTables.Columns.Add("Номер\nкоризны");
        //    //SelectedSheets.DataTables.Columns.Add("Название шкафа\nНомер шкафа");
        //    //SelectedSheets.DataTables.Columns.Add("Номер\nкорзины\nв шкафу");
        //    //SelectedSheets.DataTables.Columns.Add("Наименование модуля. Выбирайте модуль из списка, чтобы наименование было верным.");

        //    //DataRow row = SelectedSheets.DataTables.NewRow();
        //    //row["Номер\nкоризны"] = "A";
        //    //row["Название шкафа\nНомер шкафа"] = "A";
        //    //row["Номер\nкорзины\nв шкафу"] = "A";
        //    //row["Наименование модуля. Выбирайте модуль из списка, чтобы наименование было верным."] = "B";

        //    //SelectedSheets.DataTables.Rows.Add(row);


        //    //var index = MyDataGridItems.IndexOf(SelectedSheets);


        //    //if (SelectedSheets.DataTables == null)
        //    //{
        //    //    SelectedSheets.DataTables = new();
        //    //}
        //    //SelectedSheets.DataTables.Columns.Add("Номер\nкоризны");
        //    //SelectedSheets.DataTables.Columns.Add("Название шкафа\nНомер шкафа");
        //    //SelectedSheets.DataTables.Columns.Add("Номер\nкорзины\nв шкафу");
        //    //SelectedSheets.DataTables.Columns.Add("Наименование модуля. Выбирайте модуль из списка, чтобы наименование было верным.");
        //    //SelectedSheets.DataTables.Rows.Add();


        //    ////for (int i = 0; i < 3; i++)
        //    ////{
        //    ////    if (SelectedSheets.DataTables == null)
        //    ////    {
        //    ////        SelectedSheets.DataTables = new();
        //    ////        SelectedSheets.DataTables.Columns.Add();
        //    ////        SelectedSheets.DataTables.NewRow();
        //    ////    }
        //    ////    else
        //    ////    {
        //    ////        SelectedSheets.DataTables.Columns.Add();
        //    ////        SelectedSheets.DataTables.Rows.Add();
        //    ////    }
        //    ////}
        //    //MyDataGridItems[index] = SelectedSheets;
        //    //SelectedSheets = MyDataGridItems[index];
        //    //MySheetsConfig.Sheet[index] = SelectedSheets;
        //}

        /// <summary>
        /// Команда сохранить проект
        /// </summary>
        //public ICommand CmdSaveProject { get; }

        //private bool CanCmdSaveProjectExecute(object p) => !string.IsNullOrEmpty(MyPath);

        //private void OnCmdSaveProjectExecuted(object p)
        //{
        //    WriteMappingFileGridSheets(MyPath, MyDataProject);
        //}

        /// <summary>
        /// Команда октрыть проект
        /// </summary>
        //public ICommand CmdOpenFile { get; }

        //private bool CanCmdOpenFileExecute(object p) => true;

        //private void OnCmdOpenFileExecuted(object p)
        //{
        //    if (MyDataProject.Project != null)
        //    {
        //        MyDataProject.Project.Clear();
        //        MyMapSheets.Clear();
        //        MyMapData.Clear();
        //    }
        //    ReadMappingFileGridSheets(MyPath);
        //}


        /// <summary>
        /// Команда на смену светлой темы
        /// </summary>
        public ICommand CmdSetBlackTheames { get; }

        private bool CanCmdSetBlackTheamesExecute(object p) => true;

        private void OnCmdSetBlackTheamesExecuted(object p)
        {
            flBlackTheames = true;
            flWhiteTheames = false;
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
        //public ICommand CmdMaximized { get; }
        //private bool CanCmdMaximizedExecute(object p) => true;

        //private void OnCmdMaximizedExecuted(object p)
        //{
        //    if (CurrApp.MainWindow.WindowState == WindowState.Maximized)
        //    {
        //        CurrApp.MainWindow.WindowState = WindowState.Normal;
        //        return;
        //    }
        //    CurrApp.MainWindow.WindowState = WindowState.Maximized;
        //}

        /// <summary>
        /// Коамнда свернуть приложение
        /// </summary>
        //public ICommand CmdMinimized { get; }
        //private bool CanCmdMinimizedExecute(object p) => true;

        //private void OnCmdMinimizedExecuted(object p)
        //{
        //    CurrApp.MainWindow.WindowState = WindowState.Minimized;
        //}
        #endregion

        #region События
        private void CreateNewList()
        {
            foreach (var Project in JsonData.Project)
            {
                foreach (var Sheet in Project.Sheet)
                {
                    DataTable _DataTable = new();
                    DataRow _row;
                    foreach (var Column in Sheet.Columns)
                    {
                        _DataTable.Columns.Add(Column.Item);
                    }

                    for (int i = 0; i < Sheet.CountRow; i++)
                    {
                        _row = _DataTable.NewRow();
                        _DataTable.Rows.Add(_row);
                    }

                    int j = 0;
                    string column = "";
                    foreach (var Row in Sheet.Rows)
                    {
                        if (column != Row.Column) { j = 0; }
                        column = Row.Column;
                        _row = _DataTable.Rows[j];
                        _row[column] = Row.Value;
                        j++;
                    }

                    var _MapSheets = new MapSheets
                    {
                        Columns = Sheet.Columns,
                        CountRow = Sheet.CountRow,
                        DataTables = _DataTable,
                        Name = Sheet.Name + (MyMapSheets.Count + 1).ToString(),
                        NameMsg = Sheet.NameMsg,
                        Rows = Sheet.Rows
                    };
                    MyMapSheets.Add(_MapSheets);
                }
            }
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
    }
}
