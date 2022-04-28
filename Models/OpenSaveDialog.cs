using Microsoft.Win32;
using Project_Settings.Infrastructure.Commands;
using Project_Settings.Pages;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
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

        #region Свойства
        public static readonly DependencyProperty JsonDataProperty = DependencyProperty.Register(
            nameof(JsonData), typeof(DataProject), typeof(OpenSaveDialog), new PropertyMetadata(default(DataProject)));

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

        public DataProject JsonData { get => (DataProject)GetValue(JsonDataProperty); set => SetValue(JsonDataProperty, value); }
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

        public bool flNeedOpenFileAfterSave { get; set; }
        #endregion

        #region Команды
        public ICommand CmdCreateNewProject { get; }
        private bool CanCmdCreateNewProjectExecute(object p) => true;
        private async void OnCmdCreateNewProjectExecuted(object p)
        {
            SelectedFile = Environment.CurrentDirectory + "/MyResource/Jsons/GridDefualt.json";
            if (!File.Exists(SelectedFile)) return;
            using (FileStream fs = new FileStream(SelectedFile, FileMode.OpenOrCreate))
            {
                JsonData = await JsonSerializer.DeserializeAsync<DataProject>(fs).ConfigureAwait(true);
            };
            string[] subs = SelectedFile.Split('\\');
            TitleWindowsProject = subs[subs.Length - 1];
            CreateNewConfig();
        }

        public ICommand CmdOpenFileDialog { get; }
        private bool CanCmdOpenFileDialogExecute(object p) => true;
        private async void OnCmdOpenFileDialogExecuted(object p)
        {
            string flAfterSave = p as string;
            if (string.IsNullOrEmpty(flAfterSave))
            {
                var dialog = new OpenFileDialog
                {
                    Title = TitleOpen,
                    Filter = Filter,
                    RestoreDirectory = true,
                    InitialDirectory = Environment.CurrentDirectory
                };

                if ((dialog.ShowDialog() != true) || (!File.Exists(dialog.FileName))) return;
                flAfterSave = dialog.FileName;
            }

            using (FileStream fs = new FileStream(flAfterSave, FileMode.OpenOrCreate))
            {
                JsonData = await JsonSerializer.DeserializeAsync<DataProject>(fs).ConfigureAwait(true);
            };
            SelectedFile = flAfterSave;
            flNeedOpenFileAfterSave = false;
            ReadMappingFileGridSheets();
        }

        public ICommand CmdSaveFileDialog { get; }
        private bool CanCmdSaveFileDialogExecute(object p) => !string.IsNullOrEmpty(SelectedFile);
        private void OnCmdSaveFileDialogExecuted(object p)
        {
            SelectedFile = p as string;
            flNeedOpenFileAfterSave = (SelectedFile == Environment.CurrentDirectory + "/MyResource/Jsons/GridDefualt.json") || string.IsNullOrEmpty(SelectedFile);
            if (string.IsNullOrEmpty(SelectedFile) || flNeedOpenFileAfterSave)
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
            WriteMappingFileGridSheets();
        }
        #endregion

        #region Чтение\Сохранение\Создание конфига

        #region Считываем конфиг
        /// <summary>
        /// Получаем данные
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private void ReadMappingFileGridSheets()
        {
            MyMapSheets = new();
            SelectedSheets = new();
            flBlackTheames = new();
            flWhiteTheames = new();

            string[] subs = SelectedFile.Split('\\');
            TitleWindowsProject = subs[subs.Length - 1];

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
                    for (int i = 0; i < Sheet.Columns.Count; i++)
                    {
                        if (column != Sheet.Columns[i].Name)
                        {
                            j = 0;
                            column = Sheet.Columns[i].Name;
                            _DataTable.Columns.Add(column);
                        }
                        _row = _DataTable.Rows[j];
                        _row[column] = Sheet.Columns[i].Value;
                        j++;
                    }

                    var _MapSheets = new MapSheets
                    {
                        Columns = Sheet.Columns,
                        CountRow = Sheet.CountRow,
                        DataTables = _DataTable,
                        Name = Sheet.Name,
                        NameMsg = Sheet.NameMsg,
                    };
                    MyMapSheets.Add(_MapSheets);
                }
            }

            SelectedSheets = MyMapSheets[JsonData.SheetLastSelectedIntex];
            flWhiteTheames = JsonData.flWhiteTheames;
            flBlackTheames = JsonData.flBlackTheames;
        }
        #endregion

        #region Сохраняем конфиг
        /// <summary>
        /// Сохраняем данные
        /// </summary>
        /// <param name = "fileName" ></ param >
        /// < param name="dt"></param>
        private async void WriteMappingFileGridSheets()
        {
            DataProject _DataProject = new();
            ObservableCollection<MapData> myMapData = new();
            ObservableCollection<MapSheets> myMapSheets = new();
            ObservableCollection<MapColumns> myMapColumns = new();

            foreach (var Sheets in MyMapSheets)
            {
                for (int i = 0; i < Sheets.DataTables.Columns.Count; i++)
                {
                    for (int j = 0; j < Sheets.DataTables.Rows.Count; j++)
                    {
                        var _MapColumns = new MapColumns
                        {
                            Name = Sheets.DataTables.Columns[i].ColumnName,
                            Value = Sheets.DataTables.Rows[j].ItemArray[i].ToString()
                        };
                        myMapColumns.Add(_MapColumns);
                    }
                }

                var _MapData = new MapSheets
                {
                    Name = Sheets.Name,
                    NameMsg = Sheets.NameMsg,
                    DataTables = null,
                    CountRow = Sheets.DataTables.Rows.Count,
                    Columns = new ObservableCollection<MapColumns>(myMapColumns)
                };
                myMapSheets.Add(_MapData);
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
            _DataProject.Project = _myDataProject.Project;
            _DataProject.SheetLastSelectedIntex = _DataProject.SheetLastSelectedIntex;
            _DataProject.flWhiteTheames = flWhiteTheames;
            _DataProject.flBlackTheames = flBlackTheames;

            var option = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            bool _flNeedOpenFileAfterSave = flNeedOpenFileAfterSave;
            string _SelectedFile = SelectedFile;
            using (FileStream fs = new FileStream(SelectedFile, FileMode.Create, FileAccess.Write))
            {
                byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes<DataProject>(_DataProject, option);
                await fs.WriteAsync(jsonUtf8Bytes).ConfigureAwait(flNeedOpenFileAfterSave);
            }

            if (_flNeedOpenFileAfterSave) OnCmdOpenFileDialogExecuted(_SelectedFile);
        }
        #endregion

        #region Создаем конфиг
        /// <summary>
        /// Создаем новую конфигурацию
        /// </summary>
        private void CreateNewConfig()
        {
            MyMapSheets = new();
            SelectedSheets = new();
            flBlackTheames = new();
            flWhiteTheames = new();

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
                        if (column != Column.Name) { _DataTable.Columns.Add(Column.Name); j = 0; }
                        column = Column.Name;
                        _row = _DataTable.Rows[j];
                        _row[column] = Column.Value.Trim();

                    }

                    Page _MyPage = new Page1();
                    _MyPage.Title = "Страница " + (MyMapSheets.Count + 1).ToString();

                    var _MapSheets = new MapSheets
                    {
                        Columns = Sheet.Columns,
                        CountRow = Sheet.CountRow,
                        DataTables = _DataTable,
                        Name = Sheet.Name + (MyMapSheets.Count + 1).ToString(),
                        NameMsg = Sheet.NameMsg,
                        MyPage = _MyPage
                    };
                    MyMapSheets.Add(_MapSheets);
                }
            }

            SelectedSheets = MyMapSheets[JsonData.SheetLastSelectedIntex];
            flWhiteTheames = JsonData.flWhiteTheames;
            flBlackTheames = JsonData.flBlackTheames;
        }
        #endregion

        #endregion
    }
}
