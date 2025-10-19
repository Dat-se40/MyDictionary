using MyDictionary.Model; 
using MyDictionary.Model.DTOs;
using MyDictionary.Services;
using MyDictionary.Views.UserControls;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        WordSearchService wordSearchService = new WordSearchService();
        Views.Pages.SuggestionsView suggestionsView;
        Views.Pages.DetailWordView detailWordView ;
        Views.Pages.Home home;
        private ThemeService _themeService = ThemeService.Instance;
        public event PropertyChangedEventHandler? PropertyChanged;
        // Biến theo dõi trạng thái Dark Mode
        private bool isDarkMode = false;

        // Biến theo dõi trạng thái Sidebar (mở/đóng)
        private bool isSidebarOpen = false;
        public MainWindow()
        {
            suggestionsView = new Views.Pages.SuggestionsView(); 
            detailWordView  = new Views.Pages.DetailWordView() { }; 
            home = new Views.Pages.Home();  
            InitializeComponent();
            MainFrame.Navigate(home); 
        }
        async Task OnPropertyChanged([CallerMemberName] string? sender = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(sender));
            if (sender == nameof(ExpWord))
            {
                var request = wordSearchService.SearchExact(new SearchRequest()
                { _searchTerm = ExpWord, _maxResults = 5 });
                await request;
                detailWordView.TakenRespone(request.Result);
                MainFrame.Navigate(detailWordView);
            }
            else
            {
                SearchInput.Text = sender + " ne dm!";
            }
        }
        private void ThemeToggle_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _themeService.ToggleTheme();

            var slider = ThemeSlider;
            var themeIcon = ThemeIcon;

            DoubleAnimation animation = new DoubleAnimation
            {
                Duration = TimeSpan.FromMilliseconds(300)
            };

            if (_themeService.CurrentMode == MyDictionary.Services.ThemeMode.Dark)
            {
                animation.To = 36;
                themeIcon.Text = "☀️";
                ThemeSlider.Background = new SolidColorBrush(Color.FromRgb(59, 130, 246));
                ThemeToggle.Background = new SolidColorBrush(Color.FromRgb(45, 55, 72));
            }
            else
            {
                animation.To = 3;
                themeIcon.Text = "🌙";
                ThemeSlider.Background = new SolidColorBrush(Color.FromRgb(91, 127, 255));
                ThemeToggle.Background = new SolidColorBrush(Colors.White);
            }
            var transform = slider.RenderTransform as TranslateTransform;
            if (transform == null)
            {
                transform = new TranslateTransform();
                slider.RenderTransform = transform;
            }
            transform.BeginAnimation(TranslateTransform.XProperty, animation);
        }
        /// <summary>
        /// Xử lý sự kiện click vào Hamburger Button
        /// Mở/đóng Sidebar
        /// </summary>
        private void HamburgerBtn_Click(object sender, RoutedEventArgs e)
        {
            ToggleSidebar();
        }

        /// <summary>
        /// Toggle Sidebar (mở/đóng)
        /// </summary>
        private void ToggleSidebar()
        {
            isSidebarOpen = !isSidebarOpen;

            var transform = Sidebar.RenderTransform as TranslateTransform;
            if (transform == null)
            {
                transform = new TranslateTransform();
                Sidebar.RenderTransform = transform;
            }

            DoubleAnimation animation = new DoubleAnimation
            {
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            if (isSidebarOpen)
            {
                // Mở sidebar
                animation.To = 0;
                Overlay.Visibility = Visibility.Visible;
            }
            else
            {
                // Đóng sidebar
                animation.To = -280;
                Overlay.Visibility = Visibility.Collapsed;
            }

            transform.BeginAnimation(TranslateTransform.XProperty, animation);
        }

        /// <summary>
        /// Xử lý sự kiện click vào Overlay
        /// Đóng Sidebar khi click vào overlay
        /// </summary>
        private void Overlay_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (isSidebarOpen)
            {
                ToggleSidebar();
            }
        }

        /// <summary>
        /// Xử lý sự kiện click vào Search Button
        /// </summary>
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            var reponse = wordSearchService.GetSuggestions(new SearchRequest() { _searchTerm = SearchInput.Text, _maxResults = 5 });

            if (!reponse._isSuccess)
            {
                SearchInput.Text = "khong load duoc";
            }
            suggestionsView.Reply(reponse);
            MainFrame.Navigate(suggestionsView);
        }

        /// <summary>
        /// Xử lý sự kiện click vào các Tool Button (Home, History, Favourite...)
        /// </summary>
        private void ToolBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string tag = button.Tag?.ToString();
                MessageBox.Show($"Bạn đã chọn: {tag}", "Tool Selected",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            if(MainFrame.CanGoBack) MainFrame.GoBack();
        }
        private void GoForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoForward) MainFrame.GoForward();
        }
        /// <summary>
        /// Xử lý sự kiện click vào các Sidebar Item
        /// </summary>
        private void SidebarItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string tag = button.Tag?.ToString();
                MessageBox.Show($"Đang chuyển đến: {tag}", "Navigation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                ToggleSidebar();
            }
        }
        private string expWord;

        public string ExpWord
        {
            get { return expWord; }
            set
            {
                expWord = value;
                OnPropertyChanged();
            }
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(home);
        }
    }
}