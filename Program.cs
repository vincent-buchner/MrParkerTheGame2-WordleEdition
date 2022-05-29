using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
        /// All this function does is reads from a 'word.txt' file inside the the bin folder and generates a random
        /// number to index in that array.
        /// </summary>
        /// <returns>A random word as a string</returns>
        static string fiveLetterWord()
        {
            // Reads Text File
            string rawText = File.ReadAllText(@"words.txt", Encoding.UTF8);

            // Converts it to an array
            List<string> listOfStrings = rawText.Split(",").ToList();

            // TEST
            Console.WriteLine(listOfStrings);

            // Generates a random number
            Random random = new Random();
            int randomNumber = random.Next(0, listOfStrings.Count);

            return listOfStrings[randomNumber];
        }

        static void gameIntro()
        {
            Console.WriteLine("===================== W E L C O M E   T O =====================");
            // ASCII Art
            Console.WriteLine("");

            Console.WriteLine("The name of the game is, well, Mr. Parker: The Game II, but it all actuality is just Wordle." +
                "You will have six tries to guess the mystery five letter word. Best of luck!");
        }

        /// <summary>
        /// This function will compare the differnces between two words, character by character. If the character is in the exact position and is the same, it will append a "!"
        /// If the character is in the word but not in the correct position, it will append a "?"
        /// </summary>
        /// <param name="guessedWord">The user guessed word</param>
        /// <param name="actualWord">The correct Wordle word</param>
        /// <returns>A tuple: (the word after modification => string, if the word was guessed correctly => bool)</returns>
        static (string, bool) wordChecker(string guessedWord, string actualWord)
        {
            // Values will get appended to this string
            string returnWord = "";
            bool correctWordBool = false;

            // NOTE: Add this value to the return value of the function
            if (guessedWord == actualWord)
            {
                correctWordBool = true;
            }

            // Goes through each char in the string
            if (!correctWordBool)
            {
                for (int i = 0; i < guessedWord.Length; i++)
                {
                    // If it matches in the exact position, return "!"
                    if (guessedWord[i] == actualWord[i])
                    {
                        returnWord += $"!{guessedWord[i]} ";
                    }
                    // If the char is in the string, return "?"
                    else if (actualWord.Contains(guessedWord[i]))
                    {
                        returnWord += $"?{guessedWord[i]} ";
                    }
                    // Otherwise, just add the letter with no mods
                    else
                    {
                        returnWord += $"{guessedWord[i]} ";
                    }
                }
            }

            // C:\Users\Vincent\source\repos\TestApp\TestApp\bin\Debug\net5.0\words.json


            // After the operation is done, it will return that modified string
            return (returnWord, correctWordBool);
        }

        static (string[], bool) gameFunction(string word, int lives, string[] guessed)
        {
            // Reads Text File
            string rawText = File.ReadAllText(@"words.txt", Encoding.UTF8);

            // Converts it to an array
            List<string> listOfStrings = rawText.Split(",").ToList();

            // TEST
            Console.WriteLine(word);

            // Number of lives left
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

            // Player guess to lower
            playerGuess = playerGuess.ToLower();

            // Not worth checking the list if the word isn't even five characters long
            if(playerGuess.Length != 5)
            {
                Console.WriteLine("Word is too short or too long, please try again");
                gameFunction(word, lives, guessed);
            }


            // Checks JSON file for word
            // If not, rerun game loop
            // Try and avoid this for loop
            bool wordFound = false;
            for(int i = 0; i < listOfStrings.Count; i++ )
            {
                if (listOfStrings[i] == playerGuess)
                {
                    wordFound = true;
                    break;
                }
            }

            if (!wordFound)
            {
                Console.WriteLine("Word was not found in our data base, please try again...");
                gameFunction(word, lives, guessed);
            }

            // Word checker function
            (string, bool) checkedWord = wordChecker(playerGuess, word);

            // Adds guessed string to array
            guessed = new List<string>(guessed) { checkedWord.Item1 }.ToArray();

            // What the Main function needs to know
            // - If the player won
            // - What words have been guessed

            return (guessed, checkedWord.Item2);

        }

        /// <summary>
        /// THE MAIN FUNCTION == This is where everything happens
        /// </summary>
        /// <param name="args"> no clue</param>
        static void Main(string[] args)
        {
            string wordleWord = fiveLetterWord();
            int numberOfLives = 6;
            string[] alreadyGuessedWords = { };

            gameIntro();

            // Put all of this if while loop that subtracts from lives
            (string[], bool) resultsFromGuess = gameFunction(wordleWord, numberOfLives, alreadyGuessedWords);

            // Assignment to results: if the game is over
            bool gameIsOver = resultsFromGuess.Item2;

            while (!gameIsOver)
            {
                if (numberOfLives <= 0)
                {
                    gameIsOver = true;
                    Console.WriteLine("You lost!");
                    Console.WriteLine($"The word was: {wordleWord}");
                }
                else
                {
                    // Subtract from lives
                    numberOfLives--;

                    // New guessed words
                    alreadyGuessedWords = resultsFromGuess.Item1;

                    // Put all of this if while loop that subtracts from lives
                    resultsFromGuess = gameFunction(wordleWord, numberOfLives, alreadyGuessedWords);

                    // Assignment to results: if the game is over
                    gameIsOver = resultsFromGuess.Item2;
                }

            }

        if (gameIsOver && numberOfLives > 0)
            {
                Console.WriteLine("You won!");
                Console.WriteLine($"The word was: {wordleWord}");
            }
        }
    }
}