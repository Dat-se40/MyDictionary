using MyDictionary.Data;
using MyDictionary.Model;
using MyDictionary.Views.UserControls;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MyDictionary.Views.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        List<Quote> listQuotes;
        public Home()
        {
            listQuotes = new List<Quote>(); 
            InitializeComponent();
            _ = InitializeAsync(); // "fire and forget": đăng kí chạy nền ko quan tâm kết quả trả về
            this.Loaded += Home_Loaded1;
        }

        private void Home_Loaded1(object sender, RoutedEventArgs e)
        {
            stpWordItems.Children.Clear();
            LoadRandomContent();
        }

        private async Task InitializeAsync()
        {
            await LoadAllQuotes();
        }
        void AddWordItem(Word mainWord) 
        {
            WordItem wordItem = new WordItem();
            wordItem.SetUpDisplay(mainWord);
            wordItem.HorizontalAlignment = HorizontalAlignment.Stretch;
            stpWordItems.Children.Add(wordItem);    
        }
        void AddWordItem(List<ShortenWord> words)
        {
            foreach (var word in words)
            {
                WordItem wordItem = new WordItem();
                wordItem.SetUpDisplay(word);
                wordItem.HorizontalAlignment = HorizontalAlignment.Stretch;
                stpWordItems.Children.Add(wordItem);
            }
        }
        private async Task LoadAllQuotes()
        {
            var quotePaths =  Directory.GetFiles(FileStorage._storedQuotePath);
            foreach (var quotePath in quotePaths)
            {
                var task = FileStorage.LoadQuoteAsync(quotePath);
                Quote quote = await task;   
                if (quote != null) listQuotes.Add(quote);       
            }
        }
        private void LoadRandomContent()
        {
            Random ran = new Random();
            int ID = ran.Next(0,listQuotes.Count) + 1 ;
            LoadContent(ID); 
        }
        private void LoadContent(int ID)
        {
            int index = ID - 1; 
            if (index >= 0 && index < listQuotes.Count)
            {
                Quote mainQuote = listQuotes[ID - 1];
                QuoteText.Text = mainQuote.content;
                QuoteAuthor.Text = mainQuote.author;
                QuoteImage.ImageSource = new BitmapImage(new Uri(mainQuote.imageUrl));
                AddWordItem(mainQuote.relativeWords);
            }
        }
    }
}
