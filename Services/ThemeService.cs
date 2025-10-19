using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;


namespace MyDictionary.Services
{
    /// <summary>
    /// Service quản lý theme của ứng dụng
    /// </summary>
    public class ThemeService : INotifyPropertyChanged
    {
        private static ThemeService _instance;
        private Theme _currentTheme;
        private ThemeMode _currentMode = ThemeMode.Light;

        public event PropertyChangedEventHandler PropertyChanged;

        public static ThemeService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ThemeService();
                }
                return _instance;
            }
        }

        public Theme CurrentTheme
        {
            get { return _currentTheme; }
            private set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    OnPropertyChanged();
                }
            }
        }

        public ThemeMode CurrentMode
        {
            get { return _currentMode; }
            private set
            {
                if (_currentMode != value)
                {
                    _currentMode = value;
                    OnPropertyChanged();
                }
            }
        }

        public ThemeService()
        {
            // Khởi tạo với Light Theme
            CurrentTheme = Theme.CreateLightTheme();
            CurrentMode = ThemeMode.Light;
        }

        /// <summary>
        /// Toggle giữa Light và Dark theme
        /// </summary>
        public void ToggleTheme()
        {
            if (CurrentMode == ThemeMode.Light)
            {
                SetTheme(ThemeMode.Dark);
            }
            else
            {
                SetTheme(ThemeMode.Light);
            }
        }

        /// <summary>
        /// Set theme dựa trên ThemeMode
        /// </summary>
        public void SetTheme(ThemeMode mode)
        {
            Theme newTheme = mode == ThemeMode.Light
                ? Theme.CreateLightTheme()
                : Theme.CreateDarkTheme();

            CurrentTheme = newTheme;
            CurrentMode = mode;

            ApplyThemeToApplication();
        }

        /// <summary>
        /// Áp dụng theme vào Application Resources
        /// </summary>
        private void ApplyThemeToApplication()
        {
            var res = Application.Current.Resources;

            res["MainBackground"] = CurrentTheme.MainBackground;
            res["NavbarBackground"] = CurrentTheme.NavbarBackground;
            res["TextColor"] = CurrentTheme.TextColor;
            res["ButtonColor"] = CurrentTheme.ButtonColor;
            res["CardBackground"] = CurrentTheme.CardBackground;
            res["BorderColor"] = CurrentTheme.BorderColor;
            res["WordItemBackground"] = CurrentTheme.WordItemBackground;
            res["WordItemHoverBackground"] = CurrentTheme.WordItemHoverBackground;
            res["WordItemBorder"] = CurrentTheme.WordItemBorder;
            res["ToolbarBackground"] = CurrentTheme.ToolbarBackground;
            res["SidebarBackground"] = CurrentTheme.SidebarBackground;
            res["ThemeSliderBackground"] = CurrentTheme.ThemeSliderBackground;
            res["ThemeToggleBackground"] = CurrentTheme.ThemeToggleBackground;
        }

        /// <summary>
        /// Lấy color của theme hiện tại
        /// </summary>
        public Color GetColor(string colorName)
        {
            var property = typeof(Theme).GetProperty(colorName);
            if (property != null)
            {
                return (Color)property.GetValue(CurrentTheme);
            }
            return Colors.Black;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public enum ThemeMode
    {
        Light,
        Dark
    }

    /// <summary>
    /// Class đại diện cho một theme
    /// </summary>
    public class Theme
    {
        public ThemeMode Mode { get; set; }

        public Brush MainBackground { get; set; }
        public Brush NavbarBackground { get; set; }
        public Brush TextColor { get; set; }
        public Brush ButtonColor { get; set; }
        public Brush CardBackground { get; set; }
        public Brush BorderColor { get; set; }
        public Brush WordItemBackground { get; set; }
        public Brush WordItemHoverBackground { get; set; }
        public Brush WordItemBorder { get; set; }
        public Brush ToolbarBackground { get; set; }
        public Brush SidebarBackground { get; set; }
        public Brush ThemeSliderBackground { get; set; }
        public Brush ThemeToggleBackground { get; set; }

        public Theme() { }

        public Theme(ThemeMode mode)
        {
            Mode = mode;
        }

        public static Theme CreateLightTheme()
        {
            var res = Application.Current.Resources;
            return new Theme(ThemeMode.Light)
            {
                MainBackground = (Brush)res["LightMainBackground"],
                NavbarBackground = (Brush)res["LightNavbarBackground"],
                TextColor = (Brush)res["LightTextColor"],
                ButtonColor = (Brush)res["LightButtonColor"],
                CardBackground = (Brush)res["LightCardBackground"],
                BorderColor = (Brush)res["LightBorderColor"],
                WordItemBackground = (Brush)res["LightWordItemBackground"],
                WordItemHoverBackground = (Brush)res["LightWordItemHoverBackground"],
                WordItemBorder = (Brush)res["LightWordItemBorder"],
                ToolbarBackground = (Brush)res["LightToolbarBackground"],
                SidebarBackground = (Brush)res["LightSidebarBackground"],
                ThemeSliderBackground = (Brush)res["LightThemeSliderBackground"],
                ThemeToggleBackground = (Brush)res["LightThemeToggleBackground"]
            };
        }

        public static Theme CreateDarkTheme()
        {
            var res = Application.Current.Resources;
            return new Theme(ThemeMode.Dark)
            {
                MainBackground = (Brush)res["DarkMainBackground"],
                NavbarBackground = (Brush)res["DarkNavbarBackground"],
                TextColor = (Brush)res["DarkTextColor"],
                ButtonColor = (Brush)res["DarkButtonColor"],
                CardBackground = (Brush)res["DarkCardBackground"],
                BorderColor = (Brush)res["DarkBorderColor"],
                WordItemBackground = (Brush)res["DarkWordItemBackground"],
                WordItemHoverBackground = (Brush)res["DarkWordItemHoverBackground"],
                WordItemBorder = (Brush)res["DarkWordItemBorder"],
                ToolbarBackground = (Brush)res["DarkToolbarBackground"],
                SidebarBackground = (Brush)res["DarkSidebarBackground"],
                ThemeSliderBackground = (Brush)res["DarkThemeSliderBackground"],
                ThemeToggleBackground = new LinearGradientBrush(
                    Color.FromRgb(255, 255, 255),
                    Color.FromRgb(232, 241, 255),
                    new Point(0, 0),
                    new Point(0, 1)
                )
            };
        }
    }


}
