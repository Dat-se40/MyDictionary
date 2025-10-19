using MyDictionary.Model.DTOs;
using MyDictionary.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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

namespace MyDictionary.Views.Pages
{
    /// <summary>
    /// Interaction logic for SuggestionsView.xaml
    /// </summary>
    public partial class SuggestionsView : Page,INotifyPropertyChanged
    {
        private ObservableCollection<WorkLink> collection;

        public ObservableCollection<WorkLink> Collection
        {
            get { return collection; }
            set { collection = value; }
        }

        public SuggestionsView()
        {
            collection = new ObservableCollection<WorkLink>();
            InitializeComponent();

        }
        public void Reply(SearchResponse response) 
        {
            // Có vấn đề với binding
            Container.Children.Clear(); 
            var suggestions = response.GetWordsList(); 
            if (suggestions != null && suggestions.Count !=0)
            {
                suggestions.ForEach(sug => {
                    Container.Children.Add(new WorkLink() 
                    {FontSize = 30 , Content = sug});  
                });
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        
    }
}
