using Project_Settings.Infrastructure.Commands;
using Project_Settings.ViewModels.Default;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_Settings.ViewModels
{
    internal class ColorListWindowViewModel : ViewModel
    {
        public ColorListWindowViewModel()
        {
            MyListViewColor = new();
            MyListViewColor.ItemsSource = typeof(Brushes).GetProperties();

            
        }

        private SolidColorBrush _SelectedColor;
        public SolidColorBrush SelectedColor
        {
            get => _SelectedColor;
            set => Set(ref _SelectedColor, value);
        }

        private ListView _MyListViewColor;
        public ListView MyListViewColor
        {
            get => _MyListViewColor;
            set => Set(ref _MyListViewColor, value);
        }
    }
}
