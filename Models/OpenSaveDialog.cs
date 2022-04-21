using Microsoft.Win32;
using Project_Settings.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace Project_Settings.Models
{
    public class OpenSaveDialog : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new OpenSaveDialog();
        }

        public OpenSaveDialog()
        {
            CmdOpenFileDialog = new RelayCommand(OnCmdOpenFileDialogExecuted, CanCmdOpenFileDialogExecute);
            CmdSaveFileDialog = new RelayCommand(OnCmdSaveFileDialogExecuted, CanCmdSaveFileDialogExecute);
            CmdCreateNewProject = new RelayCommand(OnCmdCreateNewProjectExecuted, CanCmdCreateNewProjectExecute);
        }

        public static readonly DependencyProperty TitleOpenProperty = DependencyProperty.Register(
            nameof(TitleOpen), typeof(string), typeof(OpenSaveDialog), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty TitleSaveProperty = DependencyProperty.Register(
            nameof(TitleSave), typeof(string), typeof(OpenSaveDialog), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(
            nameof(Filter), typeof(string), typeof(OpenSaveDialog), new PropertyMetadata("Текстовые файлы (*.json)|*.json|Все файлы (*.*)|*.*"));

        public static readonly DependencyProperty SelectedFileProperty = DependencyProperty.Register(
            nameof(SelectedFile), typeof(string), typeof(OpenSaveDialog), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty TitleWindowsProjectProperty = DependencyProperty.Register(
            nameof(TitleWindowsProject), typeof(string), typeof(OpenSaveDialog), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty MyDataProjectProperty = DependencyProperty.Register(
            nameof(MyDataProject), typeof(DataProject), typeof(OpenSaveDialog), new PropertyMetadata(default(DataProject)));

        public static readonly DependencyProperty MyMapSheetsProperty = DependencyProperty.Register(
            nameof(MyMapSheets), typeof(ObservableCollection<MapSheets>), typeof(OpenSaveDialog), new PropertyMetadata(default(ObservableCollection<MapSheets>)));

        public static readonly DependencyProperty SelectedSheetsProperty = DependencyProperty.Register(
            nameof(SelectedSheets), typeof(MapSheets), typeof(OpenSaveDialog), new PropertyMetadata(default(MapSheets)));

        public static readonly DependencyProperty flBlackTheamesProperty = DependencyProperty.Register(
           nameof(flBlackTheames), typeof(bool), typeof(OpenSaveDialog), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty flWhiteTheamesProperty = DependencyProperty.Register(
           nameof(flWhiteTheames), typeof(bool), typeof(OpenSaveDialog), new PropertyMetadata(default(bool)));

        public string TitleOpen { get => (string)GetValue(TitleOpenProperty); set => SetValue(TitleOpenProperty, value); }
        public string TitleSave { get => (string)GetValue(TitleSaveProperty); set => SetValue(TitleSaveProperty, value); }
        public string Filter { get => (string)GetValue(FilterProperty); set => SetValue(FilterProperty, value); }
        public string SelectedFile { get => (string)GetValue(SelectedFileProperty); set => SetValue(SelectedFileProperty, value); }
        public string TitleWindowsProject { get => (string)GetValue(TitleWindowsProjectProperty); set => SetValue(TitleWindowsProjectProperty, value); }
        public DataProject MyDataProject { get => (DataProject)GetValue(MyDataProjectProperty); set => SetValue(MyDataProjectProperty, value); }
        public ObservableCollection<MapSheets> MyMapSheets
        {
            get => (ObservableCollection<MapSheets>)GetValue(MyMapSheetsProperty);
            set => SetValue(MyMapSheetsProperty, value);
        }
        public MapSheets SelectedSheets { get => (MapSheets)GetValue(SelectedSheetsProperty); set => SetValue(SelectedSheetsProperty, value); }
        public bool flBlackTheames { get => (bool)GetValue(flBlackTheamesProperty); set => SetValue(flBlackTheamesProperty, value); }
        public bool flWhiteTheames { get => (bool)GetValue(flWhiteTheamesProperty); set => SetValue(flWhiteTheamesProperty, value); }


        public ICommand CmdCreateNewProject { get; }
        private bool CanCmdCreateNewProjectExecute(object p) => true;
        private void OnCmdCreateNewProjectExecuted(object p)
        {
            CreateNewConfig();
        }

        public ICommand CmdOpenFileDialog { get; }
        private bool CanCmdOpenFileDialogExecute(object p) => true;
        private void OnCmdOpenFileDialogExecuted(object p)
        {
            var dialog = new OpenFileDialog
            {
                Title = TitleOpen,
                Filter = Filter,
                RestoreDirectory = true,
                InitialDirectory = Environment.CurrentDirectory
            };
            if (dialog.ShowDialog() != true) return;
            SelectedFile = dialog.FileName;
            ReadMappingFileGridSheets(SelectedFile, false);
        }

        public ICommand CmdSaveFileDialog { get; }
        private bool CanCmdSaveFileDialogExecute(object p) => true;
        private void OnCmdSaveFileDialogExecuted(object p)
        {
            SelectedFile = p as string;
            bool NeedOpenFileAfterSave = (SelectedFile == null);
            if (string.IsNullOrEmpty(SelectedFile))
            {
                var dialog = new SaveFileDialog
                {
                    Title = TitleSave,
                    Filter = Filter,
                    RestoreDirectory = true,
                    InitialDirectory = Environment.CurrentDirectory
                };
                if (dialog.ShowDialog() != true) return;
                SelectedFile = dialog.FileName;
            }
            WriteMappingFileGridSheets(SelectedFile, MyDataProject, NeedOpenFileAfterSave);
        }


        /// <summary>
        /// Получаем данные
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async void ReadMappingFileGridSheets(string FilePath, bool NeedOpenFileAfterSave)
        {
            MyMapSheets = new();
            MyDataProject = new();
            DataProject JsonData = new();
            string file_path;
            if (string.IsNullOrEmpty(FilePath)) return;

            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                JsonData = await JsonSerializer.DeserializeAsync<DataProject>(fs).ConfigureAwait(true);
                file_path = fs.Name;
            };

            string[] subs = file_path.Split('\\');
            TitleWindowsProject = subs[subs.Length - 1];

            DataRow _row;
            ObservableCollection<MapData> MyMapData = new();

            foreach (var Project in JsonData.Project)
            {
                foreach (var Sheet in Project.Sheet)
                {
                    DataTable _DataTable = new();
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
                        Name = Sheet.Name,
                        NameMsg = Sheet.NameMsg,
                        Rows = Sheet.Rows
                    };
                    MyMapSheets.Add(_MapSheets);
                }
            }

            var _MapData = new MapData
            {
                Sheet = new ObservableCollection<MapSheets>(MyMapSheets)
            };
            MyMapData.Add(_MapData);

            var _DataProject = new DataProject
            {
                Project = new ObservableCollection<MapData>(MyMapData),
                SheetLastSelectedIntex = JsonData.SheetLastSelectedIntex,
                flWhiteTheames = JsonData.flWhiteTheames,
                flBlackTheames = JsonData.flBlackTheames
            };
            MyDataProject.Project = _DataProject.Project;
            MyDataProject.SheetLastSelectedIntex = _DataProject.SheetLastSelectedIntex;
            MyDataProject.flWhiteTheames = _DataProject.flWhiteTheames;
            MyDataProject.flBlackTheames = _DataProject.flBlackTheames;
            SelectedSheets = MyMapSheets[MyDataProject.SheetLastSelectedIntex];
            flWhiteTheames = MyDataProject.flWhiteTheames;
            flBlackTheames = MyDataProject.flBlackTheames;
        }


        /// <summary>
        /// Сохраняем данные
        /// </summary>
        /// <param name = "fileName" ></ param >
        /// < param name="dt"></param>
        private async void WriteMappingFileGridSheets(string FilePath, DataProject _DataProject, bool NeedOpenFileAfterSave)
        {
            DataProject dataProject = new();
            ObservableCollection<MapData> myMapData = new();
            ObservableCollection<MapSheets> myMapSheets = new();
            foreach (var _Project in _DataProject.Project)
            {
                foreach (var Sheet in _Project.Sheet)
                {
                    List<MapColumns> myMapColumn = new();
                    List<MapRow> myMapRow = new();
                    int ColumnCount = Sheet.DataTables.Columns.Count;
                    int RowCount = Sheet.DataTables.Rows.Count;

                    for (int i = 0; i < ColumnCount; i++)
                    {
                        string column = Sheet.DataTables.Columns[i].ColumnName;
                        var mapColumns = new MapColumns
                        {
                            Item = column
                        };
                        myMapColumn.Add(mapColumns);
                        for (int j = 0; j < RowCount; j++)
                        {
                            string value = Sheet.DataTables.Rows[j].ItemArray[i].ToString();
                            var mapRow = new MapRow
                            {
                                Column = column,
                                Value = value
                            };
                            myMapRow.Add(mapRow);
                        }
                    }

                    var _MapData = new MapSheets
                    {
                        Name = Sheet.Name,
                        NameMsg = Sheet.NameMsg,
                        DataTables = null,
                        CountRow = Sheet.DataTables.Rows.Count,
                        Columns = myMapColumn,
                        Rows = myMapRow
                    };
                    myMapSheets.Add(_MapData);
                }
            }

            var _myMapData = new MapData
            {
                Sheet = new ObservableCollection<MapSheets>(myMapSheets)
            };
            myMapData.Add(_myMapData);

            var _myDataProject = new DataProject
            {
                Project = new ObservableCollection<MapData>(myMapData)
            };
            dataProject.Project = _myDataProject.Project;
            dataProject.SheetLastSelectedIntex = _DataProject.SheetLastSelectedIntex;
            dataProject.flWhiteTheames = flWhiteTheames;
            dataProject.flBlackTheames = flBlackTheames;

            var option = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes<DataProject>(dataProject, option);
                await fs.WriteAsync(jsonUtf8Bytes).ConfigureAwait(NeedOpenFileAfterSave);
            }

            if (NeedOpenFileAfterSave) ReadMappingFileGridSheets(SelectedFile, NeedOpenFileAfterSave);
        }


        /// <summary>
        /// Создаем новую конфигурацию
        /// </summary>
        private async void CreateNewConfig()
        {
            SelectedSheets = new();
            MyMapSheets = new();
            MyDataProject = new();
            SelectedFile = "";
            TitleWindowsProject = "";
            flBlackTheames = false;
            flWhiteTheames = false;

            DataProject JsonData = new();
            string FilePath = Environment.CurrentDirectory + "/MyResource/Jsons/GridDefualt.json";

            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                JsonData = await JsonSerializer.DeserializeAsync<DataProject>(fs).ConfigureAwait(true);
            };
            string[] subs = FilePath.Split('\\');
            TitleWindowsProject = subs[subs.Length - 1];

            DataRow _row;
            ObservableCollection<MapData> MyMapData = new();

            foreach (var Project in JsonData.Project)
            {
                foreach (var Sheet in Project.Sheet)
                {
                    DataTable _DataTable = new();
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
                        Name = Sheet.Name,
                        NameMsg = Sheet.NameMsg,
                        Rows = Sheet.Rows
                    };
                    MyMapSheets.Add(_MapSheets);
                }
            }

            var _MapData = new MapData
            {
                Sheet = new ObservableCollection<MapSheets>(MyMapSheets)
            };
            MyMapData.Add(_MapData);

            var _DataProject = new DataProject
            {
                Project = new ObservableCollection<MapData>(MyMapData),
                SheetLastSelectedIntex = JsonData.SheetLastSelectedIntex,
                flWhiteTheames = JsonData.flWhiteTheames,
                flBlackTheames = JsonData.flBlackTheames
            };
            MyDataProject.Project = _DataProject.Project;
            MyDataProject.SheetLastSelectedIntex = _DataProject.SheetLastSelectedIntex;
            MyDataProject.flWhiteTheames = _DataProject.flWhiteTheames;
            MyDataProject.flBlackTheames = _DataProject.flBlackTheames;
            SelectedSheets = MyMapSheets[MyDataProject.SheetLastSelectedIntex];
            flWhiteTheames = MyDataProject.flWhiteTheames;
            flBlackTheames = MyDataProject.flBlackTheames;
        }

    }
}
