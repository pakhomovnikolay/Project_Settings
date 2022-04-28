using Project_Settings.Models;
using Project_Settings.ViewModels.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Settings.ViewModels.Pages
{
    public class PageViewModel : ViewModel
    {
        public PageViewModel()
        {
            SelectedSheets = new();
        }

        private MapSheets _SelectedSheets = new();
        public MapSheets SelectedSheets
        {
            get => _SelectedSheets;
            set => Set(ref _SelectedSheets, value);
        }
    }
}
