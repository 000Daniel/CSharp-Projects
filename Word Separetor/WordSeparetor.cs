using System.Collections.Generic;
using System;

namespace Word_Separetor
{
    class WordSeparetor
    {
        static List<string> Words = new List<string>();
        static String userInput;
        
        static void Main() {
            userInput = "Hello New World Again! :)";
            ProcessWords(userInput);
        }

        static void ProcessWords(string userInput)
        {
            string Temp_userInput = userInput;
            while (Temp_userInput.Length > 0) {
                if (Temp_userInput.Contains(" ")) {
                    int firstSpace = Temp_userInput.IndexOf(" ");
                    while (firstSpace == 0) {
                        Temp_userInput = Temp_userInput.Substring(1);
                        firstSpace = Temp_userInput.IndexOf(" ");
                    }
                    if (firstSpace != 0) {
                        Words.Add(Temp_userInput.Substring(0,firstSpace));
                        Temp_userInput = Temp_userInput.Substring(firstSpace + 1);
                    }
                } else {
                    Words.Add(Temp_userInput);
                    Temp_userInput = null;
                    break;
                }
            }
            PrintAllWords();
        }

        static void PrintAllWords() {
            for (int i = 0; i < Words.Count; i++) {
                Console.WriteLine("[" + (i + 1) + "]" + Words[i]);
            }
        }
    }
}
