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

            BorderBrush = Brushes.Transparent;
            BorderThickness = new Thickness(0);
            Cursor = Cursors.Hand;
            Padding = new Thickness(4, 2, 4, 2);
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Bottom;

            SetDynamicColors();
            Click += WorkLink_Click;  
        }
        private void SetDynamicColors()
        {
            var res = Application.Current.Resources;
            this.Style =  (Style)res["ToolButtonStyle"]; 
            
        }
        private void WorkLink_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MyDictionary.MainWindow;
            if (mainWindow != null)
            {

                // Gán Content của WorkLink vào ExpWord trong MainWindow
                mainWindow.ExpWord = this.Content?.ToString() ?? string.Empty;
            }
        }
    }
}
