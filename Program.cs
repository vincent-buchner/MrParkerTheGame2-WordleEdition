using System;
using System.IO;

// NEWTON
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/* 

--- STRINGBUILDER AND PARSING --- 
NOTE: StringBuilder inside of C# essentially makes strings mutable. Watch Joe Marini's LinkedIn Learning video(3.4, Learning C#) or visit 
Microsoft's Documentation on the StringBuilder class

Link To Docs about StringBuilder:
https://lnkd.in/gspyQD5

Link to Docs about Parsing Strings:
https://docs.microsoft.com/en-us/dotnet/standard/base-types/parsing-strings

///////////////////////////////////////////////////////////////////////////////////////////
// EXPLANATION AND TODO'S
///////////////////////////////////////////////////////////////////////////////////////////

/* 
MR. PARKER: THE GAME 2
WORDLE EDITION

This game is essential Wordle, but in a command prompt and with a Mr. Parker theme to it.
List of TODO's can be found below

[✔] TODO: Import five letter words and format it into JSON
[✔] TODO: Set up a random function that Index through the JSON list(or maybe just a list in general?)
[x] TODO: Set a turn counter system
[x] TODO: Set up a system that tells the user if the letter is in the right position or not, or if it's in the word
[x] TODO: I'm thinking about apending '?' if that letter is in the word, "!" if it's in the correct spot
[x] TODO: Life Counter and already used words
[x] TODO: Add some cool ASCII art
[x] TODO: Functions: Main Function, Game Loop Function, Art Function(s), fiveLetterWord, gameIntro
*/

namespace Program
{
    class Program
    {
        /// <summary>
        /// <h1>Random Five Letter Word Returner</h1>
        /// 
        /// All this function does is reads from a 'word.json' file inside the the bin folder and generates a random
        /// number to index in that array.
        /// </summary>
        /// <returns>A random word as a string</returns>
        static string fiveLetterWord()
        {
            // Const string that reads from the 'words.json' file inside of the bin folder
            const string jsonFileIn = "words.json";

            // Complies thatc JSON into an object C# can undertsand
            dynamic jsonFile = JsonConvert.DeserializeObject(File.ReadAllText(jsonFileIn));

            // Pulls a random number
            Random random = new Random();
            int randNumber = random.Next(0, jsonFile.Count);

            // Returns a random word from the array of words using that random number
            return jsonFile[randNumber];
        }

        static void gameIntro()
        {
            Console.WriteLine("===================== W E L C O M E   T O =====================");
            // ASCII Art
            Console.WriteLine("");

            Console.WriteLine("The name of the game is, well, Mr. Parker: The Game II, but it all actuality is just Wordle." +
                "You will have six tries to guess the mystery five letter word. Best of luck!");
        }

        static object gameLoop(string word,int lives,string[] guessed)
        {
            // Const string that reads from the 'words.json' file inside of the bin folder
            const string jsonFileIn = "words.json";

            // This variable catches if a word was not in word bank or is too long/short
            bool inputErrorWasCaught = false;

            // Complies thatc JSON into an object C# can undertsand
            dynamic jsonFile = JsonConvert.DeserializeObject(File.ReadAllText(jsonFileIn));

            // Number of lives quessed
            Console.WriteLine($"You have {lives} live(s) left.");

            // The words already guessed(with mods)
            Console.WriteLine($"You have already guessed: {System.Environment.NewLine}");
            for (int i = 0; i < guessed.Length; i++)
            {
                Console.WriteLine($"    ~ {guessed[i]}{System.Environment.NewLine}");
            }

            // Player enters their guess
            Console.WriteLine("Please enter your word: ");
            string playerGuess = Console.ReadLine();

            // Checks if that word is in the list
            bool stringIsInJSON = jsonFile.Contains(playerGuess);

            // Checks JSON file for word
            if (!stringIsInJSON)
            {
                Console.WriteLine("Word was not found in word bank, please try again.");
                inputErrorWasCaught = true;
            }


            



        }
        static void Main(string[] args)
        {
            string wordleWord = fiveLetterWord();
            int numberOfLives = 6;
            string[] alreadyGuessedWords = { };

            gameIntro();

            object[] resultsFromQuess = gameLoop(wordleWord, numberOfLives, alreadyGuessedWords);
            
        }
    }
}