using System.Collections.Generic;

namespace Project_Settings
{
    public class SignalMapTreeView
    {
        public string Item { get; set; }
    }

    public class MappingConfigTreeView
    {
        public IList<SignalMapTreeView> Lists { get; set; }
        public IList<SignalMapTreeView> ListsMsg { get; set; }
    }

    
}
