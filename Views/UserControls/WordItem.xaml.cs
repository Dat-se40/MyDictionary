﻿using MyDictionary.Model;
using MyDictionary.Services;
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

namespace MyDictionary.Views.UserControls
{
    /// <summary>
    /// Interaction logic for WordItem.xaml
    /// </summary>
    public partial class WordItem : UserControl
    {

        public WordItem()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += WordItem_MouseLeftButtonDown;
        }

        public void SetUpDisplay(Word mainWord)
        {
            tbMainWord.Text = mainWord.word;
            tbPhonetic.Text = mainWord.phonetic;
            tbMeaningText.Text = mainWord.meanings[0].definitions[0].definition ?? string.Empty;
        }
        public void SetUpDisplay(ShortenWord mainWord)
        {
            tbMainWord.Text = mainWord.word;
            tbPhonetic.Text = mainWord.phonetic;
            tbMeaningText.Text = mainWord.meaning; 
        }

        private void WordItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MyDictionary.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.ExpWord = tbMainWord.Text.ToString()?? string.Empty ;
            }
        }
    }
}
