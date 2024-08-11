using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countdown
{
    internal class WordList
    {
        private static List<string> words;

        static WordList()
        {
            LoadWordsAsync();
        }

        //Open words.txt file 
        public static async Task LoadWordsAsync()
        {
            List<string> loadedWords = new List<string>();

            try
            {
                string path = "words.txt"; //Change path if needed

                using (Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(path))
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        Console.WriteLine(line);
                        loadedWords.Add(line.Trim().ToLower());
                        line = reader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                //Handle exception
                Console.WriteLine($"Error loading words: {ex.Message}");
            }

            words = loadedWords;
            //Debug.WriteLine($"Total words loaded: {words.Count}"); //Check words loaded in debug output
        }

        //Return word if it exists
        public static bool ContainsWord(string word)
        {
            //Debug.WriteLine($"Checking if word list contains '{word}'");//Debugging
            return words != null && words.Contains(word);
        }
    }
}
