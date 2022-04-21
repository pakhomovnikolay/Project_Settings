using Microsoft.Win32;
using Project_Settings.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace Project_Settings.Models
{
    public class SaveDialog : Freezable
    {
        public static readonly DependencyProperty TitleSaveProperty = DependencyProperty.Register(
            nameof(TitleSave), typeof(string), typeof(SaveDialog), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty FilterSaveProperty = DependencyProperty.Register(
            nameof(FilterSave), typeof(string), typeof(SaveDialog), new PropertyMetadata("Текстовые файлы (*.json)|*.json|Все файлы (*.*)|*.*"));

        public static readonly DependencyProperty MyDataProjectSaveProperty = DependencyProperty.Register(
            nameof(MyDataProjectSave), typeof(DataProject), typeof(SaveDialog), new PropertyMetadata(default(DataProject)));

        public static readonly DependencyProperty flBlackTheamesSaveProperty = DependencyProperty.Register(
           nameof(flBlackTheamesSave), typeof(bool), typeof(SaveDialog), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty flWhiteTheamesSaveProperty = DependencyProperty.Register(
           nameof(flWhiteTheamesSave), typeof(bool), typeof(SaveDialog), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty SelectedFileSaveProperty = DependencyProperty.Register(
            nameof(SelectedFileSave), typeof(string), typeof(SaveDialog), new PropertyMetadata(default(string)));

        public string TitleSave { get => (string)GetValue(TitleSaveProperty); set => SetValue(TitleSaveProperty, value); }
        public string FilterSave { get => (string)GetValue(FilterSaveProperty); set => SetValue(FilterSaveProperty, value); }
        public DataProject MyDataProjectSave { get => (DataProject)GetValue(MyDataProjectSaveProperty); set => SetValue(MyDataProjectSaveProperty, value); }
        public bool flBlackTheamesSave { get => (bool)GetValue(flBlackTheamesSaveProperty); set => SetValue(flBlackTheamesSaveProperty, value); }
        public bool flWhiteTheamesSave { get => (bool)GetValue(flWhiteTheamesSaveProperty); set => SetValue(flWhiteTheamesSaveProperty, value); }
        public string SelectedFileSave { get => (string)GetValue(SelectedFileSaveProperty); set => SetValue(SelectedFileSaveProperty, value); }

        public ICommand CmdSaveFileDialog { get; }
        private bool CanCmdSaveFileDialogExecute(object p) => !string.IsNullOrEmpty(SelectedFileSave);

        private void OnCmdSaveFileDialogExecuted(object p)
        {
            var filePath = p as string;
            if (filePath == null)
            {
                var dialog = new SaveFileDialog
                {
                    Title = TitleSave,
                    Filter = FilterSave,
                    RestoreDirectory = true,
                    InitialDirectory = Environment.CurrentDirectory
                };
                if (dialog.ShowDialog() != true) return;
                filePath = dialog.FileName;
            }
            WriteMappingFileGridSheets(filePath, MyDataProjectSave);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new SaveDialog();
        }

        public SaveDialog()
        {
            MyDataProjectSave = new();
            CmdSaveFileDialog = new RelayCommand(OnCmdSaveFileDialogExecuted, CanCmdSaveFileDialogExecute);
        }

        /// <summary>
        /// Сохраняем данные
        /// </summary>
        /// <param name = "fileName" ></ param >
        /// < param name="dt"></param>
        private async void WriteMappingFileGridSheets(string FilePath, DataProject _DataProject)
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
            dataProject.flWhiteTheames = flWhiteTheamesSave;
            dataProject.flBlackTheames = flBlackTheamesSave;

            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(dataProject);
                await fs.WriteAsync(jsonUtf8Bytes).ConfigureAwait(false);
            }
        }
    }
}
