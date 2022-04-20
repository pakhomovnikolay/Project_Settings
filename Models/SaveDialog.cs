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
        public static readonly DependencyProperty TitleCloseProperty = DependencyProperty.Register(
            nameof(TitleClose), typeof(string), typeof(SaveDialog), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty FilterCloseProperty = DependencyProperty.Register(
            nameof(FilterClose), typeof(string), typeof(SaveDialog), new PropertyMetadata("Текстовые файлы (*.json)|*.json|Все файлы (*.*)|*.*"));

        public static readonly DependencyProperty MyDataProjectCloseProperty = DependencyProperty.Register(
            nameof(MyDataProjectClose), typeof(DataProject), typeof(SaveDialog), new PropertyMetadata(default(DataProject)));

        public static readonly DependencyProperty flBlackTheamesCloseProperty = DependencyProperty.Register(
           nameof(flBlackTheamesClose), typeof(bool), typeof(SaveDialog), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty flWhiteTheamesCloseProperty = DependencyProperty.Register(
           nameof(flWhiteTheamesClose), typeof(bool), typeof(SaveDialog), new PropertyMetadata(default(bool)));

        public string TitleClose { get => (string)GetValue(TitleCloseProperty); set => SetValue(TitleCloseProperty, value); }
        public string FilterClose { get => (string)GetValue(FilterCloseProperty); set => SetValue(FilterCloseProperty, value); }
        public DataProject MyDataProjectClose { get => (DataProject)GetValue(MyDataProjectCloseProperty); set => SetValue(MyDataProjectCloseProperty, value); }
        public bool flBlackTheamesClose { get => (bool)GetValue(flBlackTheamesCloseProperty); set => SetValue(flBlackTheamesCloseProperty, value); }
        public bool flWhiteTheamesClose { get => (bool)GetValue(flWhiteTheamesCloseProperty); set => SetValue(flWhiteTheamesCloseProperty, value); }

        public ICommand CmdSaveFileDialog { get; }
        private bool CanCmdSaveFileDialogExecute(object p) => true;

        private void OnCmdSaveFileDialogExecuted(object p)
        {
            var filePath = p as string;
            if (filePath == null)
            {
                var dialog = new SaveFileDialog
                {
                    Title = TitleClose,
                    Filter = FilterClose,
                    RestoreDirectory = true,
                    InitialDirectory = Environment.CurrentDirectory
                };
                if (dialog.ShowDialog() != true) return;
                filePath = dialog.FileName;
            }
            WriteMappingFileGridSheets(filePath, MyDataProjectClose);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new SaveDialog();
        }

        public SaveDialog()
        {
            MyDataProjectClose = new();
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

            dataProject.flWhiteTheames = flWhiteTheamesClose;
            dataProject.flBlackTheames = flBlackTheamesClose;


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
