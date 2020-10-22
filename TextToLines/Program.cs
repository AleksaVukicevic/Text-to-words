using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextToLines
{
    class Program // By Aleksa Vukićević
    {
        static void Main(string[] args)
        {
            string readPath;
            string writePath;
            char[] seperators = { ' ', ',', '-', '(', ')', '.', '!', '?', ';', '/', '"', ':', '_' }; // chars that seperate the words
            List<string> words; // words list

        read:
            // ------ Takes the read path from the user, saves it and checks if it Exists ------
            Print("Enter file path to READ from (.txt)", ConsoleColor.White);
            Console.ForegroundColor = ConsoleColor.Yellow;
            readPath = Console.ReadLine();
            if (!File.Exists(readPath))
            {
                Print("File not found", ConsoleColor.Red);
                goto read;
            }

            // ------ Takes the write path from the user, saves it and checks if it Exists ------
            Console.WriteLine();
            Print("Enter file path to SAVE to (.txt)", ConsoleColor.White);
            Console.ForegroundColor = ConsoleColor.Yellow;
            writePath = Console.ReadLine();
            if (!File.Exists(writePath))
            {
                Print("File not found, it will be created.", ConsoleColor.DarkYellow);
            }
            Console.WriteLine();
            Print("WARNING! DATA IN THE WRITE FILE WILL BE REPLACED!", ConsoleColor.Red);
            Print("Enter \"start\" if you want to continue.", ConsoleColor.Yellow);

        start: 
            // ------ After the user enters "start" the program continues ------
            if (Console.ReadLine().ToLower() == "start")
            {
                Print($"\nWORKING... Reading from file {readPath}\n", ConsoleColor.Yellow);
                words = GetWords(readPath); // Read words and save them
                SaveWords(); // Save  words to a file
                Print($"\nDONE. Saved to file {writePath}", ConsoleColor.Yellow); // Finish
            }
            else
            {
                goto start; // If the user didn't enter "start" go back
            }


            // Methods down here |
            //                   V

            List<string> GetWords(string filepath) // Read the path and return a list with all the words
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    string filetext = sr.ReadToEnd(); // Takes the text from the file and puts it into a string
                    filetext = filetext.Replace(Environment.NewLine, seperators[0].ToString()); // Removes all new lines
                    string[] str = filetext.Split(seperators); // Splits the filetext into words and puts every word into a list
                    return str.ToList(); // Returns the words
                }
            }

            void SaveWords() // writes all the words to a file
            {
                using (StreamWriter sw = new StreamWriter(writePath))
                {
                    for (int w = 0; w < words.Count; w++) // Loops over all the words in the array
                    {
                        if (words[w] != "") // If the word isn't empty, save it
                        {
                            sw.WriteLine(words[w]); // Saves each line to a file
                            Console.WriteLine(words[w]);
                        }
                    }
                }
            }

            void Print(string text, ConsoleColor consoleColor) // Print function to make it easier to print colored text;
            {
                Console.ForegroundColor = consoleColor;
                Console.WriteLine(text);
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
