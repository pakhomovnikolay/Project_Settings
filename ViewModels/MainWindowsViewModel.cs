using Project_Settings.Infrastructure.Commands;
using Project_Settings.Models;
using Project_Settings.ViewModels.Default;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_Settings.ViewModels
{
    public class MainWindowsViewModel : ViewModel
    {
        private readonly Application CurrApp = Application.Current;

        //public Page MyPage = new Page1();

        public MainWindowsViewModel()
        {
            // Команды
            CmdSetBlackTheames = new RelayCommand(OnCmdSetBlackTheamesExecuted, CanCmdSetBlackTheamesExecute);
            CmdSetWhiteTheames = new RelayCommand(OnCmdSetWhiteTheamesExecuted, CanCmdSetWhiteTheamesExecute);
            CmdCloseApp = new RelayCommand(OnCmdCloseAppExecuted, CanCmdCloseAppExecute);
            CmdCreateNewList = new RelayCommand(OnCmdCreateNewListExecuted, CanCmdCreateNewListExecute);
            CmdRemoveSelectedList = new RelayCommand(OnCmdRemoveSelectedListExecuted, CanCmdRemoveSelectedListExecute);
            CmdAddRowList = new RelayCommand(OnCmdAddRowListExecuted, CanCmdAddRowListExecute);
            CmdRemoveSelectedRowList = new RelayCommand(OnCmdRemoveSelectedRowListExecuted, CanCmdRemoveSelectedRowListExecute);
            CmdSetColorRow = new RelayCommand(OnCmdSetColorRowExecuted, CanCmdSetColorRowExecute);
            MyListViewColor = new();
            MyListViewColor.ItemsSource = typeof(Brushes).GetProperties();
        }

        #region Параметры
        private DataGrid _MySelectedDataGrid;
        public DataGrid MySelectedDataGrid
        {
            get => _MySelectedDataGrid;
            set => Set(ref _MySelectedDataGrid, value);
        }


        private ListView _MyListViewColor;
        public ListView MyListViewColor
        {
            get => _MyListViewColor;
            set => Set(ref _MyListViewColor, value);
        }

        private SolidColorBrush _MyBorderBrush = Brushes.LightGray;
        public SolidColorBrush MyBorderBrush
        {
            get => _MyBorderBrush;
            set => Set(ref _MyBorderBrush, value);
        }

        private SolidColorBrush _MySelectedColor = Brushes.Yellow;
        public SolidColorBrush MySelectedColor
        {
            get => _MySelectedColor;
            set => Set(ref _MySelectedColor, value);
        }


        private DataRowView _SelectedItems;
        public DataRowView SelectedItems
        {

            get => _SelectedItems;
            set => Set(ref _SelectedItems, value);
        }

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

        private string _myPath = Environment.CurrentDirectory + "/MyResource/Jsons/GridDefualt.json";
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

        #region Команды
        public ICommand CmdSetColorRow { get; }
        private bool CanCmdSetColorRowExecute(object p) => SelectedSheets != null;
        private void OnCmdSetColorRowExecuted(object p)
        {
            if (p == null) return;
            DataGrid MyDataGrid = p as DataGrid;
            var row_list = GetDataGridRows(MyDataGrid);
            bool fl = false;
            foreach (var single_row in row_list)
            {
                if (single_row.IsSelected)
                {
                    CurrApp.Resources["ColorDataGridItems"] = MySelectedColor;
                    single_row.Background = MySelectedColor;
                    single_row.BorderBrush = MySelectedColor;
                    fl = true;
                }
            }
            if (fl) return;

            var cell_list = GetDataGridCell(MyDataGrid);
            foreach (var single_cell in cell_list)
            {
                if (single_cell.IsSelected)
                {
                    //CurrApp.Resources["ColorDataGridItems"] = MySelectedColor;
                    single_cell.Background = MySelectedColor;
                }
            }

            //foreach (var SelectedCells in MyDataGrid.SelectedCells)
            //{

            //    SelectedCells




            //    //var asd = SelectedCells as DataGridRow;
            //    //int selectedColumn = SelectedCells.Column.DisplayIndex;
            //    //var selectedCell = MyDataGrid.SelectedCells[selectedColumn];
            //    //var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
            //    //if (cellContent is DataGridCell)
            //    //{
            //    //    cellContent.
            //    //}


            //}

            //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[dataGridView1.CurrentCell.ColumnIndex].Value.ToString()


            //int selectedColumn = MyDataGrid.CurrentCell.Column.DisplayIndex;

            //var cell_list = GetDataGridCell(MyDataGrid);
            //foreach (var single_cell in cell_list)
            //{

            //}




            //cell_list.GetEnumerator().Current.Background = MySelectedColor;
            //foreach (var single_cell in cell_list.GetEnumerator().Current.Background)
            //{
            //    if (single_cell.IsSelected)
            //    {
            //        single_cell.Background = MySelectedColor;
            //    }
            //}
        }

        /// <summary>
        /// Команда на содание новой вкладке в текущем проекте
        /// </summary>
        public ICommand CmdCreateNewList { get; }
        private bool CanCmdCreateNewListExecute(object p) => !string.IsNullOrEmpty(MyPath);
        private async void OnCmdCreateNewListExecuted(object p)
        {
            string DefaultPath = Environment.CurrentDirectory + "/MyResource/Jsons/GridDefualt.json";
            using (FileStream fs = new FileStream(DefaultPath, FileMode.OpenOrCreate))
            {
                JsonData = await JsonSerializer.DeserializeAsync<DataProject>(fs).ConfigureAwait(true);
            };
            CreateNewList();
        }

        /// <summary>
        /// Команда на удаление выделеной вкладки текущего проекта
        /// </summary>
        public ICommand CmdRemoveSelectedList { get; }
        private bool CanCmdRemoveSelectedListExecute(object p) => SelectedSheets != null;
        private void OnCmdRemoveSelectedListExecuted(object p)
        {
            int count = MyMapSheets.Count;
            if (count < 2)
            {
                MessageBox.Show("Невозможно удалить последнюю страницу", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int index = MyMapSheets.IndexOf(SelectedSheets);
            MyMapSheets.Remove(SelectedSheets);
            if (index > 0) index--;
            SelectedSheets = MyMapSheets[index];
        }

        /// <summary>
        /// Команда добавления новых строк (3 строки за одну команду)
        /// </summary>
        public ICommand CmdAddRowList { get; }
        private bool CanCmdAddRowListExecute(object p) => SelectedSheets != null;
        private void OnCmdAddRowListExecuted(object p)
        {
            int index = MyMapSheets.IndexOf(SelectedSheets);
            if (index < 0) return;
            DataRow _row;
            for (int i = 0; i < 3; i++)
            {
                _row = SelectedSheets.DataTables.NewRow();
                SelectedSheets.DataTables.Rows.Add(_row);
            }

            for (int i = 0; i < SelectedSheets.DataTables.Columns.Count; i++)
            {
                var _MapColumns = new MapColumns
                {
                    Name = SelectedSheets.DataTables.Columns[i].ColumnName,
                    Value = ""
                };
                SelectedSheets.Columns.Add(_MapColumns);
            }
        }

        /// <summary>
        /// Команда удаления выделынных строк
        /// </summary>
        public ICommand CmdRemoveSelectedRowList { get; }
        private bool CanCmdRemoveSelectedRowListExecute(object p) => SelectedItems != null;
        private void OnCmdRemoveSelectedRowListExecuted(object p)
        {
            DataGrid MyDataGrid = p as DataGrid;
            try
            {
                var row_list = GetDataGridRows(MyDataGrid);
                foreach (var single_row in row_list)
                {
                    if (single_row.IsSelected)
                    {
                        int j = single_row.GetIndex();
                        SelectedSheets.DataTables.Rows.RemoveAt(j);
                    }
                }
                MyDataGrid.Items.Refresh();
            }
            catch { }
        }

        /// <summary>
        /// Команда на смену светлой темы
        /// </summary>
        public ICommand CmdSetBlackTheames { get; }

        private bool CanCmdSetBlackTheamesExecute(object p) => SelectedSheets != null;

        private void OnCmdSetBlackTheamesExecuted(object p)
        {
            flBlackTheames = true;
            flWhiteTheames = false;
        }

        /// <summary>
        /// Команда на смену темной темы
        /// </summary>
        public ICommand CmdSetWhiteTheames { get; }

        private bool CanCmdSetWhiteTheamesExecute(object p) => SelectedSheets != null;

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

        #endregion

        #region События

        private IEnumerable<DataGridCell> GetDataGridCell(DataGrid grid)
        {
            var itemsSource = grid.SelectedCells as IEnumerable;
            if (itemsSource == null) yield return null;
            foreach (var item in itemsSource)
            {
                if (grid.ItemContainerGenerator.ContainerFromItem(item) is DataGridCell row) yield return row;
            }
        }

        private IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (itemsSource == null) yield return null;
            foreach (var item in itemsSource)
            {
                if (grid.ItemContainerGenerator.ContainerFromItem(item) is DataGridRow row) yield return row;
            }
        }

        private void CreateNewList()
        {
            foreach (var Project in JsonData.Project)
            {
                foreach (var Sheet in Project.Sheet)
                {
                    DataTable _DataTable = new();
                    DataRow _row;

                    for (int i = 0; i < Sheet.CountRow; i++)
                    {
                        _row = _DataTable.NewRow();
                        _DataTable.Rows.Add(_row);
                    }

                    string column = "";
                    int j = 0;
                    foreach (var Column in Sheet.Columns)
                    {
                        if (column != Column.Name)
                        {
                            _DataTable.Columns.Add(Column.Name);
                            j = 0;
                        }
                        column = Column.Name;
                        _row = _DataTable.Rows[j];
                        _row[column] = Column.Value;
                        j++;
                    }

                    var _MapSheets = new MapSheets
                    {
                        Columns = Sheet.Columns,
                        CountRow = Sheet.CountRow,
                        DataTables = _DataTable,
                        Name = Sheet.Name + (MyMapSheets.Count + 1).ToString(),
                        NameMsg = Sheet.NameMsg
                    };
                    MyMapSheets.Add(_MapSheets);
                }
            }
            SelectedSheets = MyMapSheets[^1];
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
