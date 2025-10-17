using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace MyDictionary.Views.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private DispatcherTimer quoteTimer;

        // Index của quote hiện tại
        private int currentQuoteIndex = 0;

        // Danh sách các câu trích dẫn
        private List<Quote> quotes = new List<Quote>
        {
            new Quote
            {
                Text = "The limits of my language mean the limits of my world.",
                Author = "Ludwig Wittgenstein",
                ImageUrl = "https://picsum.photos/200/200?random=1"
            },
            new Quote
            {
                Text = "Language is the road map of a culture.",
                Author = "Rita Mae Brown",
                ImageUrl = "https://picsum.photos/200/200?random=2"
            },
            new Quote
            {
                Text = "One language sets you in a corridor for life.",
                Author = "Frank Smith",
                ImageUrl = "https://picsum.photos/200/200?random=3"
            }
        };

        // Danh sách từ vựng mẫu
        private List<WordItem> words = new List<WordItem>
        {
            new WordItem { Word = "Beautiful", Phonetic = "/ˈbjuː.tɪ.fəl/", Meaning = "adj. Đẹp, xinh đẹp, tuyệt đẹp" },
            new WordItem { Word = "Dictionary", Phonetic = "/ˈdɪk.ʃən.er.i/", Meaning = "n. Từ điển" },
            new WordItem { Word = "Knowledge", Phonetic = "/ˈnɒl.ɪdʒ/", Meaning = "n. Kiến thức, sự hiểu biết" },
            new WordItem { Word = "Learn", Phonetic = "/lɜːrn/", Meaning = "v. Học, học hỏi" },
            new WordItem { Word = "Language", Phonetic = "/ˈlæŋ.ɡwɪdʒ/", Meaning = "n. Ngôn ngữ" },
            new WordItem { Word = "Practice", Phonetic = "/ˈpræk.tɪs/", Meaning = "v. Thực hành, luyện tập" },
            new WordItem { Word = "Study", Phonetic = "/ˈstʌd.i/", Meaning = "v. Học tập, nghiên cứu" }
        };

        /// <summary>
        /// Constructor - Khởi tạo MainWindow
        /// </summary>
        public Home()
        {
            InitializeComponent();
            InitializeQuoteTimer();
            LoadWordCollection();
        }

        /// <summary>
        /// Khởi tạo Timer để tự động thay đổi quote mỗi 10 giây
        /// </summary>
        private void InitializeQuoteTimer()
        {
            quoteTimer = new DispatcherTimer();
            quoteTimer.Interval = TimeSpan.FromSeconds(10);
            quoteTimer.Tick += (s, e) => ChangeQuote();
            quoteTimer.Start();
        }

        /// <summary>
        /// Thay đổi quote hiển thị
        /// </summary>
        private void ChangeQuote()
        {
            currentQuoteIndex = (currentQuoteIndex + 1) % quotes.Count;
            var quote = quotes[currentQuoteIndex];

            QuoteText.Text = $"\"{quote.Text}\"";
            QuoteAuthor.Text = $"— {quote.Author}";
        }

        /// <summary>
        /// Load danh sách từ vựng vào Word Collection
        /// Tạo động các Border cho mỗi từ với màu sắc phù hợp với theme
        /// </summary>
        private void LoadWordCollection()
        {
            WordCollection.Children.Clear();

            // Lấy màu từ Resources dựa theo theme hiện tại
            var wordItemBackground = (Brush)this.Resources["WordItemBackground"];
            var wordItemHoverBackground = (Brush)this.Resources["WordItemHoverBackground"];
            var wordItemBorder = (SolidColorBrush)this.Resources["WordItemBorder"];
            var textColor = (SolidColorBrush)this.Resources["TextColor"];

            foreach (var word in words)
            {
                // Tạo Border cho mỗi word item
                var border = new Border
                {
                    Background = wordItemBackground,
                    BorderBrush = wordItemBorder,
                    BorderThickness = new Thickness(3, 0, 0, 0),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(20, 15, 20, 15),
                    Margin = new Thickness(0, 0, 0, 12),
                    Cursor = System.Windows.Input.Cursors.Hand
                };

                var stackPanel = new StackPanel();

                // Word text
                var wordText = new TextBlock
                {
                    Text = word.Word,
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = textColor
                };

                // Phonetic text
                var phoneticText = new TextBlock
                {
                    Text = word.Phonetic,
                    FontSize = 13,
                    Foreground = textColor,
                    Opacity = 0.8,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                // Meaning text
                var meaningText = new TextBlock
                {
                    Text = word.Meaning,
                    FontSize = 14,
                    Foreground = textColor,
                    Opacity = 0.9,
                    Margin = new Thickness(0, 8, 0, 0),
                    TextWrapping = TextWrapping.Wrap
                };

                stackPanel.Children.Add(wordText);
                stackPanel.Children.Add(phoneticText);
                stackPanel.Children.Add(meaningText);

                border.Child = stackPanel;

                // Hover effect
                border.MouseEnter += (s, e) =>
                {
                    border.Background = (Brush)this.Resources["WordItemHoverBackground"];
                };

                border.MouseLeave += (s, e) =>
                {
                    border.Background = (Brush)this.Resources["WordItemBackground"];
                };

                // Click event
                border.MouseDown += (s, e) =>
                {
                    MessageBox.Show($"Bạn đã chọn từ: {word.Word}", "Word Selected",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                };

                WordCollection.Children.Add(border);
            }
        }

        /// <summary>
        /// Xử lý sự kiện click vào Theme Toggle
        /// Chuyển đổi giữa Light Mode và Dark Mode
        /// </summary>

    }

    /// <summary>
    /// Class đại diện cho một Quote (câu trích dẫn)
    /// </summary>
    public class Quote
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
    }

    /// <summary>
    /// Class đại diện cho một Word Item (từ vựng)
    /// </summary>
    public class WordItem
    {
        public string Word { get; set; }
        public string Phonetic { get; set; }
        public string Meaning { get; set; }
    }
}
