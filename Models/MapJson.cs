using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project_Settings.Models
{
    public class DataProject
    {
        public bool flWhiteTheames { get; set; }
        public bool flBlackTheames { get; set; }
        public int SheetLastSelectedIntex { get; set; }
        public IList<MapData> Project { get; set; }
    }

    public class MapData
    {
        public IList<MapSheets> Sheet { get; set; }
    }

    public class MapSheets
    {
        public string Name { get; set; }
        public string NameMsg { get; set; }
        public int CountRow { get; set; }
        public DataTable DataTables { get; set; }
        public IList<MapColumns> Columns { get; set; }
        public IList<MapRow> Rows { get; set; }

    }
    public class MapColumns
    {
        public string Item { get; set; }
    }

    public class MapRow
    {
        public string Column { get; set; }
        public string Value { get; set; }
    }
}
