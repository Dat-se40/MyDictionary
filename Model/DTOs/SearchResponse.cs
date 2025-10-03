using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.Model.DTOs
{
  
    public  class SearchResponse : EventArgs
    {
        public bool _isSuccess { get; set;  }   
        public List<Word> _words { get; set; }       
        public Word GetFirstWord() 
        { 
            return _words[0]; 
        }
        public List<string> GetWordsList() 
        {
            List<string> strings = new List<string>();  
            foreach (Word word in _words) strings.Add(word.word);
            return strings;             
        }
        static public SearchResponse BuildReponseFormWordsList(List<string> words) 
        {
            SearchResponse newRespone = new SearchResponse();
            newRespone._words = new List<Word>();
            words.ForEach(w => 
            {
                newRespone._words.Add(new Word() { word = w});  
            });
            return newRespone;
        } 
    }
}
