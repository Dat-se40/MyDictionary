using MyDictionary.Data;
using MyDictionary.Model;
using MyDictionary.Model.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
namespace MyDictionary.Services
{
    public class WordSearchService
    {
        private readonly WordCacheManager  _cacheManager = new WordCacheManager();
        private List<string> _dictionary = FileStorage.BuildDictionary();

        public SearchResponse GetSuggestions(SearchRequest request)
        {
            string term = request._searchTerm;
            var suggestions = _dictionary.Select(word => new
            {
                Word = word,
                Dictance = CalcLevenshteinDistance(term, word),
                LengthDiff = Math.Abs(word.Length - term.Length),
                IsPrefix = word.StartsWith(term) || term.StartsWith(word)
            }).Where(suggestion => suggestion.IsPrefix || suggestion.Dictance <= Math.Max(2, term.Length / 2))
            .OrderBy(suggestion => suggestion.IsPrefix ? 0 : 1 )
            .ThenBy(suggestion => suggestion.Dictance)
            .ThenBy(suggestion => suggestion.LengthDiff)
            .Take(request._maxResults).Select(suggestion => suggestion.Word).ToList();
           
            
            return SearchResponse.BuildReponseFormWordsList(suggestions); 
            
        }
            
        public async Task<SearchResponse> SearchExact(SearchRequest request)
        {
            SearchResponse response = new SearchResponse() { _isSuccess = false };
            string term = request._searchTerm;
            string path = FileStorage.GetWordFilePath(term);

            
            var cachedWords = _cacheManager.GetWordsFormCache(term);
            if (cachedWords != null)
            {
                response._words = cachedWords;
            }else if (File.Exists(path))
            {
                response._words = await FileStorage.LoadWordAsync(path) ?? new List<Word>();
            }else
            {
                response._words = await ApiClient.FetchWordAsync(term);
            }
                

            if (response._words != null && response._words.Count > 0)
            {
                response._isSuccess = true;
                _cacheManager.AddToCache(term, response._words);
            }

            return response;
        }
        public int CalcLevenshteinDistance(string s1, string s2)
        {
            int m = s1.Length, n = s2.Length;
            int[,] lenvenshtein = new int[m + 1, n + 1];

            for (int i = 0; i <= m; i++) lenvenshtein[i, 0] = i;
            for (int j = 0; j <= n; j++) lenvenshtein[0, j] = j;

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1]) lenvenshtein[i, j] = lenvenshtein[i - 1, j - 1];
                    else lenvenshtein[i, j] = 1 + Math.Min(lenvenshtein[i, j - 1], Math.Min(lenvenshtein[i - 1, j], lenvenshtein[i - 1, j - 1]));
                }
            }
            return lenvenshtein[m, n];
        }
    }
}
