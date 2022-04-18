using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Settings.Models.LayotRack
{
    public class Rack
    {
        public int RackId { get; set; }
        public string Name { get; set; }
        public int IndexUSO { get; set; }
        public string IndexRackInUSO { get; set; }
        public List<Module> Modules { get; set; } = new List<Module>();
    }
}
