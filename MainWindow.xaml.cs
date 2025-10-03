using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyDictionary;
using MyDictionary.Model.DTOs;
using MyDictionary.Services;
namespace MyDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WordSearchService wordSearchService = new WordSearchService() ;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchRequest request = new SearchRequest() { _searchTerm = tbSearch.Text, _maxResults = 5 };
            var response = wordSearchService.GetSuggestions(request);
            Pages.SuggestionsList suggestions = new Pages.SuggestionsList();
            suggestions.Dislay(response); 
            MainFrame.Content = suggestions; 
        }

        private void btnHomeNavg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHistoryNavg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCollectionsNavg_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}