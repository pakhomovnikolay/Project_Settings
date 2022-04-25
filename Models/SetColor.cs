using Project_Settings.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_Settings.Models
{
    public class SetColor : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new OpenSaveDialog();
        }

        public SetColor()
        {
            CmdSelectColor = new RelayCommand(OnCmdSelectColorExecuted, CanCmdSelectColorExecute);
        }

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(
            nameof(SelectedColor), typeof(SolidColorBrush), typeof(SetColor), new PropertyMetadata(default(SolidColorBrush)));

        public SolidColorBrush SelectedColor { get => (SolidColorBrush)GetValue(SelectedColorProperty); set => SetValue(SelectedColorProperty, value); }

        /// <summary>
        /// Команда удаления выделынных строк
        /// </summary>
        public ICommand CmdSelectColor { get; }
        private bool CanCmdSelectColorExecute(object p) => true;
        private void OnCmdSelectColorExecuted(object p)
        {
            string ColorName = p as string;
            System.Drawing.Color wfColor = System.Drawing.Color.FromName(ColorName);
            System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(wfColor.A, wfColor.R, wfColor.G, wfColor.B);
            SelectedColor = new SolidColorBrush(color);
        }
    }
}
