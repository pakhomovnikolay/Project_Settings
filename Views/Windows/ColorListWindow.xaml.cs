using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project_Settings.ViewModels;
using Project_Settings.Views;

namespace Project_Settings.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ColorListWindow.xaml
    /// </summary>
    public partial class ColorListWindow
    {
        public ColorListWindow()
        {
            InitializeComponent();
            //ColorList.ItemsSource = typeof(Brushes).GetProperties();
        }

        //private void ColorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Brush SelectedColor = (Brush)(e.AddedItems[0] as PropertyInfo).GetValue(null, null);
        //    MainWindowsViewModel mainWindowsViewModel = new();
        //    mainWindowsViewModel.SetColor(SelectedColor);
        //}
    }
}
