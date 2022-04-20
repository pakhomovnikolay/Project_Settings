using Microsoft.Win32;
using Project_Settings.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Settings.Models
{
    internal class SaveDialog : Freezable
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(OpenDialog), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(
            nameof(Filter), typeof(string), typeof(OpenDialog), new PropertyMetadata("Текстовые файлы (*.json)|*.json|Все файлы (*.*)|*.*"));

        public static readonly DependencyProperty MyDataProjectProperty = DependencyProperty.Register(
            nameof(MyDataProject), typeof(DataProject), typeof(OpenDialog), new PropertyMetadata(default(DataProject)));

        public static readonly DependencyProperty flBlackTheamesProperty = DependencyProperty.Register(
           nameof(flBlackTheames), typeof(bool), typeof(OpenDialog), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty flWhiteTheamesProperty = DependencyProperty.Register(
           nameof(flWhiteTheames), typeof(bool), typeof(OpenDialog), new PropertyMetadata(default(bool)));

        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
        public string Filter { get => (string)GetValue(FilterProperty); set => SetValue(FilterProperty, value); }
        public DataProject MyDataProject { get => (DataProject)GetValue(MyDataProjectProperty); set => SetValue(MyDataProjectProperty, value); }
        public bool flBlackTheames { get => (bool)GetValue(flBlackTheamesProperty); set => SetValue(flBlackTheamesProperty, value); }
        public bool flWhiteTheames { get => (bool)GetValue(flWhiteTheamesProperty); set => SetValue(flWhiteTheamesProperty, value); }

        public ICommand CmdSaveFileDialog { get; }
        private bool CanCmdSaveFileDialogExecute(object p) => MyDataProject.Project != null;

        private void OnCmdSaveFileDialogExecuted(object p)
        {
            var filePath = p as string;
            if (filePath == null)
            {
                var dialog = new OpenFileDialog
                {
                    Title = Title,
                    Filter = Filter,
                    RestoreDirectory = true,
                    InitialDirectory = Environment.CurrentDirectory
                };
                if (dialog.ShowDialog() != true) return;                
            }
            WriteMappingFileGridSheets(filePath, MyDataProject);


        }

        protected override Freezable CreateInstanceCore()
        {
            return new SaveDialog();
        }

        public SaveDialog()
        {
            CmdSaveFileDialog = new RelayCommand(OnCmdSaveFileDialogExecuted, CanCmdSaveFileDialogExecute);
        }

        /// <summary>
        /// Сохраняем данные
        /// </summary>
        /// <param name = "fileName" ></ param >
        /// < param name="dt"></param>
        private async void WriteMappingFileGridSheets(string FilePath, DataProject _DataProject)
        {
            //if (string.IsNullOrEmpty(FilePath))
            //{
            //    var dialog = new SaveFileDialog
            //    {
            //        Title = "Сохранение конфигурации",
            //        Filter = "Текстовые файлы (*.json)|*.json|Все файлы (*.*)|*.*",
            //        InitialDirectory = Environment.CurrentDirectory,
            //        RestoreDirectory = true
            //    };
            //    if (dialog.ShowDialog() != true) return;
            //    FilePath = dialog.FileName;
            //}

            //if (!File.Exists(FilePath))
            //{
            //    if (MessageBox.Show("Файл не найдет. Проверьте путь", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            //}


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


            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(dataProject);
                await fs.WriteAsync(jsonUtf8Bytes).ConfigureAwait(false);
            }
            //MyPath = FilePath;

            //var options = new JsonSerializerOptions
            //{
            //    AllowTrailingCommas = true,
            //    WriteIndented = true
            //};
            //byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(dataProject);
            //File.WriteAllBytesAsync(FilePath, jsonUtf8Bytes);

        }
    }
}
