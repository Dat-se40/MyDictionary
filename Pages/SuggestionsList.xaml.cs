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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyDictionary.Model.DTOs; 
namespace MyDictionary.Pages
{
    /// <summary>
    /// Interaction logic for SuggestionsList.xaml
    /// </summary>
    public partial class SuggestionsList : Page
    {
        List<string> suggestions;

        
        public SuggestionsList()
        {
            InitializeComponent();
        }
        public void Dislay(SearchResponse response)
        {
            lvSuggestions.Items.Clear();    
            suggestions = response.GetWordsList();
            suggestions.ForEach(sug => lvSuggestions.Items.Add(sug)); 
        }
        
    }
}
