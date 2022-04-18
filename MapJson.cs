using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project_Settings
{
    public class Sheets
    {
        public int LastSelectIntex { get; set; }
        public IList<MapSheets> Sheet { get; set; }




        //public IList<MapSheets> Sheet { get; set; }
    }
    //public class MapSheets
    //{
    //    public string Name { get; set; }
    //    public string NameMsg { get; set; }
    //    public string CountRow { get; set; }
    //    public DataTable DataTables { get; set; }
    //    public IList<MapColumns> Columns { get; set; }

    //    //public IList<MapColumns> Columns { get; set; }

    //}
    //public class MapColumns
    //{
    //    public string Col { get; set; }
    //    public string Row { get; set; }
    //}

    //public class MapColumn
    //{
    //    public string A { get; set; }
    //    public string B { get; set; }
    //    public string C { get; set; }
    //    public string D { get; set; }
    //    public string E { get; set; }
    //    public string F { get; set; }
    //    public string G { get; set; }
    //    public string H { get; set; }
    //    public string I { get; set; }
    //    public string J { get; set; }
    //    public string K { get; set; }
    //    public string L { get; set; }
    //    public string M { get; set; }
    //    public string N { get; set; }
    //    public string O { get; set; }
    //    public string P { get; set; }
    //    public string Q { get; set; }
    //    public string R { get; set; }
    //    public string S { get; set; }
    //    public string T { get; set; }
    //    public string U { get; set; }
    //    public string V { get; set; }
    //    public string W { get; set; }
    //    public string X { get; set; }
    //    public string Y { get; set; }
    //    public string Z { get; set; }
    //}
}
