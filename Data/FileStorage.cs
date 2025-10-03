using Microsoft.VisualBasic;
using MyDictionary.Model;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MyDictionary.Data
{
    internal class FileStorage
    {
        static private string _storedFolder = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            @"..\..\..\Data\PersistentStorage\StoredWord"
        );
        static private string _listFile = Path.Combine(
           AppDomain.CurrentDomain.BaseDirectory,
           @"..\..\..\Data\PersistentStorage\AvailableWordList.txt"
        );

        public static string GetWordFilePath(string word)
        {
            return Path.Combine(_storedFolder, word + ".json");
        }

        public static List<string> GetStoredWordList()
        {
            if (!Directory.Exists(_storedFolder)) return new List<string>();

            var result = Directory.GetFiles(_storedFolder, "*.json");
            List<string> words = new List<string>();

            foreach (var r in result)
            {
                string word = Path.GetFileNameWithoutExtension(r);
                words.Add(word);
            }
            return words;
        }
        
        public static List<string> GetAvailableWordList()
        {
            List<string> results = File.ReadAllLines(_listFile).Where(line => !string.IsNullOrWhiteSpace(line)).
                                    Select(line => line.Trim()).ToList().ToList();   
            return results; 
        }
        
        public static async Task<List<Word>?> LoadWordAsync(string word)
        {
            string path = GetWordFilePath(word);
            if (!File.Exists(path)) return null;
            
            var content = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<List<Word>>(content);
        }

        public static bool LoadWordAsync(List<Word>? words)
        {
            if (words == null || words.Count == 0) return false;

            string path = GetWordFilePath(words[0].word);

            if (File.Exists(path)) return false; 

            string content = JsonConvert.SerializeObject(words, Formatting.Indented);
            File.WriteAllTextAsync(path, content);
            return true;
        }

        public static List<string> BuildDictionary()
        {

            var list1 = GetAvailableWordList();
            var list2 = GetStoredWordList();
            return  list1.Concat(list2).Distinct().ToList();
        }
    
    }
}
