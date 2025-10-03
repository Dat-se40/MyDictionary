using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary.Model.DTOs
{
    internal class SearchRequest : EventArgs
    {
        private string _searchTermBacking;  
        public string _searchTerm
        {
            get { return _searchTermBacking; }
            set { _searchTermBacking = value.ToLower().Trim(); }
        }

        public int _maxResults { get; set; }   = 5;
        
        public SearchRequest()
        {

        }
    }
}
