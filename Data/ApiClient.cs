using System.Net;
using System.Net.Http;
using MyDictionary.Model;
using Newtonsoft.Json;



namespace MyDictionary.Data
{
    internal class ApiClient
    {
        readonly static string _baseApiUrl = "https://api.dictionaryapi.dev/api/v2/entries/en/";
        private static readonly HttpClient _client = new HttpClient();
        public ApiClient() {}


        public static string BuildApiUrl(string word)
        {
            return _baseApiUrl + word;   
        }


        public static async Task<List<Word>> FetchWordAsync(string word)
        {
            string path = BuildApiUrl(word);
            string content = await _client.GetStringAsync(path);
            var result = JsonConvert.DeserializeObject<List<Word>>(content);
            return result ?? new List<Word>();
        }
    }
}