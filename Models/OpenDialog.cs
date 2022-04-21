using Microsoft.Win32;
using Project_Settings.Infrastructure.Commands;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace Project_Settings.Models
{
    public class OpenDialog : Freezable
    {
        public static readonly DependencyProperty TitleOpenProperty = DependencyProperty.Register(
            nameof(TitleOpen), typeof(string), typeof(OpenDialog), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty FilterOpenProperty = DependencyProperty.Register(
            nameof(FilterOpen), typeof(string), typeof(OpenDialog), new PropertyMetadata("Текстовые файлы (*.json)|*.json|Все файлы (*.*)|*.*"));

        public static readonly DependencyProperty SelectedFileOpenProperty = DependencyProperty.Register(
            nameof(SelectedFileOpen), typeof(string), typeof(OpenDialog), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty TitleWindowsProjectProperty = DependencyProperty.Register(
            nameof(TitleWindowsProject), typeof(string), typeof(OpenDialog), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty MyDataProjectOpenProperty = DependencyProperty.Register(
            nameof(MyDataProjectOpen), typeof(DataProject), typeof(OpenDialog), new PropertyMetadata(default(DataProject)));

        public static readonly DependencyProperty MyMapSheetsProperty = DependencyProperty.Register(
            nameof(MyMapSheets), typeof(ObservableCollection<MapSheets>), typeof(OpenDialog), new PropertyMetadata(default(ObservableCollection<MapSheets>)));

        public static readonly DependencyProperty SelectedSheetsProperty = DependencyProperty.Register(
            nameof(SelectedSheets), typeof(MapSheets), typeof(OpenDialog), new PropertyMetadata(default(MapSheets)));

        public static readonly DependencyProperty flBlackTheamesOpenProperty = DependencyProperty.Register(
           nameof(flBlackTheamesOpen), typeof(bool), typeof(OpenDialog), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty flWhiteTheamesOpenProperty = DependencyProperty.Register(
           nameof(flWhiteTheamesOpen), typeof(bool), typeof(OpenDialog), new PropertyMetadata(default(bool)));

        public string TitleOpen { get => (string)GetValue(TitleOpenProperty); set => SetValue(TitleOpenProperty, value); }
        public string FilterOpen { get => (string)GetValue(FilterOpenProperty); set => SetValue(FilterOpenProperty, value); }
        public string SelectedFileOpen { get => (string)GetValue(SelectedFileOpenProperty); set => SetValue(SelectedFileOpenProperty, value); }
        public string TitleWindowsProject { get => (string)GetValue(TitleWindowsProjectProperty); set => SetValue(TitleWindowsProjectProperty, value); }
        public DataProject MyDataProjectOpen { get => (DataProject)GetValue(MyDataProjectOpenProperty); set => SetValue(MyDataProjectOpenProperty, value); }

        public ObservableCollection<MapSheets> MyMapSheets
        {
            get => (ObservableCollection<MapSheets>)GetValue(MyMapSheetsProperty);
            set => SetValue(MyMapSheetsProperty, value);
        }

        public MapSheets SelectedSheets { get => (MapSheets)GetValue(SelectedSheetsProperty); set => SetValue(SelectedSheetsProperty, value); }
        public bool flBlackTheamesOpen { get => (bool)GetValue(flBlackTheamesOpenProperty); set => SetValue(flBlackTheamesOpenProperty, value); }
        public bool flWhiteTheamesOpen { get => (bool)GetValue(flWhiteTheamesOpenProperty); set => SetValue(flWhiteTheamesOpenProperty, value); }

        public ICommand CmdOpenFileDialog { get; }
        private bool CanCmdOpenFileDialogExecute(object p) => true;
        private void OnCmdOpenFileDialogExecuted(object p)
        {
            var dialog = new OpenFileDialog
            {
                Title = TitleOpen,
                Filter = FilterOpen,
                RestoreDirectory = true,
                InitialDirectory = Environment.CurrentDirectory
            };
            if (dialog.ShowDialog() != true) return;
            SelectedFileOpen = dialog.FileName;
            ReadMappingFileGridSheets(SelectedFileOpen);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new OpenDialog();
        }

        public OpenDialog()
        {
            CmdOpenFileDialog = new RelayCommand(OnCmdOpenFileDialogExecuted, CanCmdOpenFileDialogExecute);
        }

        /// <summary>
        /// Получаем данные
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async void ReadMappingFileGridSheets(string FilePath)
        {
            MyMapSheets = new();
            MyDataProjectOpen = new();
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
            MyDataProjectOpen.Project = _DataProject.Project;
            MyDataProjectOpen.SheetLastSelectedIntex = _DataProject.SheetLastSelectedIntex;
            MyDataProjectOpen.flWhiteTheames = _DataProject.flWhiteTheames;
            MyDataProjectOpen.flBlackTheames = _DataProject.flBlackTheames;
            SelectedSheets = MyMapSheets[MyDataProjectOpen.SheetLastSelectedIntex];
            flWhiteTheamesOpen = MyDataProjectOpen.flWhiteTheames;
            flBlackTheamesOpen = MyDataProjectOpen.flBlackTheames;
        }
    }

}
