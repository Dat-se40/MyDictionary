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
using MyDictionary.Views.UserControls;
using MyDictionary.Model.DTOs;
using MyDictionary.Model; 
using MyDictionary.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            suggestionsView = new Views.Pages.SuggestionsView(); 
            detailWordView  = new Views.Pages.DetailWordView() { }; 
            home = new Views.Pages.Home();  
            InitializeComponent();
            MainFrame.Navigate(home); 
            
        }
        private string expWord;

        public string ExpWord
        {
            get { return expWord; }
            set { expWord = value;
                OnPropertyChanged(); 
            }
        }

        async Task OnPropertyChanged([CallerMemberName] string sender = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(sender)); 
            if(sender == nameof(ExpWord))
            {
                var request = wordSearchService.SearchExact(new SearchRequest()
                                            { _searchTerm = ExpWord, _maxResults = 5 });
                await request; 
                detailWordView.TakenRespone(request.Result);
                MainFrame.Navigate(detailWordView);  
            }else
            {
                tbSearch.Text = sender + " ne dm!";
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var reponse = wordSearchService.GetSuggestions( new SearchRequest() { _searchTerm =  tbSearch.Text   , _maxResults = 5 });

            if (!reponse._isSuccess) 
            {
                tbSearch.Text = "clm thất bại";
            }
            suggestionsView.Reply( reponse ); 
            MainFrame.Navigate(suggestionsView); 
        }

        private void btnHomeNavg_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(home); 
        }

        private void btnHistoryNavg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCollectionsNavg_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack) MainFrame.GoBack();    
        }

        private void btnGoForward_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoForward) MainFrame.GoForward();
        }
    }
}