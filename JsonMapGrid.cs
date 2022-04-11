using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Settings
{
    public class MappingConfigGrid
    {
        public SignalMapGrid[] Columns { get; set; }
    }

    public class SignalMapGrid
    {
        public string Item { get; set; }

        public string Value { get; set; }
    }
}