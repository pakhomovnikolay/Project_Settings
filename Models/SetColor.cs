using Project_Settings.Infrastructure.Commands;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_Settings.Models
{
    public class SetColor : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new SetColor();
        }

        public SetColor()
        {
            CmdSelectColor = new RelayCommand(OnCmdSelectColorExecuted, CanCmdSelectColorExecute);
        }

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(
            nameof(SelectedColor), typeof(SolidColorBrush), typeof(SetColor), new PropertyMetadata(default(SolidColorBrush)));

        public SolidColorBrush SelectedColor
        {
            get => (SolidColorBrush)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        /// <summary>
        /// Команда удаления выделынных строк
        /// </summary>
        public ICommand CmdSelectColor { get; }
        private bool CanCmdSelectColorExecute(object p) => true;
        private void OnCmdSelectColorExecuted(object p)
        {
            string ColorName = p as string;
            if (string.IsNullOrEmpty(ColorName)) return;
            System.Drawing.Color wfColor = System.Drawing.Color.FromName(ColorName);
            System.Windows.Media.Color Color = System.Windows.Media.Color.FromRgb(wfColor.R, wfColor.G, wfColor.B);
            SelectedColor = new SolidColorBrush(Color);
        }
    }
}
