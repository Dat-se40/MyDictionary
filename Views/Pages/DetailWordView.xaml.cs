using MyDictionary.Data;
using MyDictionary.Model;
using MyDictionary.Model.DTOs;
using System.Net;
using System.Windows.Controls;

namespace MyDictionary.Views.Pages
{
    /// <summary>
    /// Interaction logic for DetailWordView.xaml
    /// </summary>
    public partial class DetailWordView : Page
    {
        Word mainWord; 
        public string main { get ; set { 
                lbTitle.Content = value;
            } } = string.Empty; 
        public DetailWordView()
        {
            InitializeComponent();
            
        }
        public void TakenRespone(SearchResponse response ) 
        {
            if (!response._isSuccess) main = "Failed to load ";
            else 
            {
                mainWord = response._words[0];
                main = mainWord.word;
                lbUS.Content = " US: " + mainWord.phonetic; 
                lbUK.Content = " UK: " + mainWord.phonetic; 
            }
        }

        private void btnDownload_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FileStorage.Download(new List<Word>() {mainWord});
        }
    }
}
