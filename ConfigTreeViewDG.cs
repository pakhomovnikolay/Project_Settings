using System.Collections.Generic;

namespace Project_Settings
{
    /// <summary>
    /// Конфиг для заполнения TreeView
    /// </summary>
    public class ConfigTreeView
    {
        public string Item { get; set; }
    }

    public class MapTreeView
    {
        public IList<ConfigTreeView> Lists { get; set; }
        public IList<ConfigTreeView> ListsMsg { get; set; }
    }

    /// <summary>
    /// Конфиг для заполнения DataGrid
    /// </summary>

    public class ConfigDataGrid
    {
        public string Item { get; set; }
        public string Value { get; set; }
        public string ColumnSpawn { get; set; }
    }

    public class MapDataGrid
    {
        public IList<ConfigDataGrid> Columns { get; set; }
    }

    public class GroupSheet
    {

    }

    public class MapColumnItem
    {
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
        public string F { get; set; }
        public string G { get; set; }
        public string H { get; set; }
        public string I { get; set; }
        public string J { get; set; }
        public string K { get; set; }
        public string L { get; set; }
        public string M { get; set; }
        public string N { get; set; }
        public string O { get; set; }
        public string P { get; set; }
        public string Q { get; set; }
        public string R { get; set; }
        public string S { get; set; }
        public string T { get; set; }
        public string U { get; set; }
        public string V { get; set; }
        public string W { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string Z { get; set; }
    }

}
