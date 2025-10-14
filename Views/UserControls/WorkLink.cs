using MyDictionary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            this.Background = Brushes.Transparent;
            this.BorderBrush = Brushes.Transparent;
            this.BorderThickness = new Thickness(0); 
            VerticalAlignment = VerticalAlignment.Bottom;
            HorizontalAlignment = HorizontalAlignment.Left;
            this.Click += WorkLink_Click; // đăng ký event
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
