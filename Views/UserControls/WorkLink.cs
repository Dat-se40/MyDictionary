using MyDictionary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MyDictionary.Views.UserControls
{
    public class WorkLink : Button
    {

        public WorkLink()
        {
            // Cấu hình cơ bản
            Background = Brushes.Transparent;
            BorderBrush = Brushes.Transparent;
            BorderThickness = new Thickness(0);
            Cursor = Cursors.Hand;
            Padding = new Thickness(4, 2, 4, 2);
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Bottom;

            // Áp dụng màu động từ ResourceDictionary
            SetDynamicColors();
            Click += WorkLink_Click;  
        }
        private void SetDynamicColors()
        {
            var res = Application.Current.Resources;

            if (res["TextColor"] is SolidColorBrush textBrush)
                Foreground = textBrush;
            else
                Foreground = new SolidColorBrush(Color.FromRgb(26, 45, 109)); 
            Background = Brushes.Transparent;
        }
        private void WorkLink_Click(object sender, RoutedEventArgs e)
        {
            // Tìm cửa sổ cha (MainWindow)
            var mainWindow = Application.Current.MainWindow as MyDictionary.MainWindow;
            if (mainWindow != null)
            {

                // Gán Content của WorkLink vào ExpWord trong MainWindow
                mainWindow.ExpWord = this.Content?.ToString() ?? string.Empty;
            }
        }
    }
}
