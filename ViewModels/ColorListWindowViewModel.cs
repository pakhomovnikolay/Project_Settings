using Project_Settings.Infrastructure.Commands;
using Project_Settings.ViewModels.Default;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project_Settings.ViewModels
{
    internal class ColorListWindowViewModel : ViewModel
    {
        public ColorListWindowViewModel()
        {
            MyListViewColor = new();
            MyListViewColor.ItemsSource = typeof(Brushes).GetProperties();

            CmdSelectColor = new RelayCommand(OnCmdSelectColorExecuted, CanCmdSelectColorExecute);
        }

        private Rectangle _SelectRectangle;
        public Rectangle SelectRectangle
        {
            get => _SelectRectangle;
            set => Set(ref _SelectRectangle, value);
        }

        private ListView _MyListViewColor;
        public ListView MyListViewColor
        {
            get => _MyListViewColor;
            set => Set(ref _MyListViewColor, value);
        }

        /// <summary>
        /// Команда удаления выделынных строк
        /// </summary>
        public ICommand CmdSelectColor { get; }
        private bool CanCmdSelectColorExecute(object p) => true;
        private void OnCmdSelectColorExecuted(object p)
        {


            //Color color = p as Color;
            Brush SelectedColor = p as Brush;
            //Brush SelectedColor = Color.GetValue(Color)

            MainWindowsViewModel mainWindowsViewModel = new();
            mainWindowsViewModel.SetColor(SelectedColor);

            //Brush SelectedColor = (Brush)(p.AddedItems[0] as PropertyInfo).GetValue(null, null);
            //MainWindowsViewModel mainWindowsViewModel = new();
            //mainWindowsViewModel.SetColor(SelectedColor);
        }
    }
}
