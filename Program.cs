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

TODO: Import five letter words and format it into JSON
TODO: Set up a random function that Index through the JSON list(or maybe just a list in general?)
TODO: Set a turn counter system
TODO: Set up a system that tells the user if the letter is in the right position or not, or if it's in the word
TODO: I'm thinking about apending '?' if that letter is in the word, "!" if it's in the correct spot
TODO: Life Counter and already used words
TODO: Add some cool ASCII art
TODO: Functions: Main Function, Game Loop Function, Art Function(s), fiveLetterWord

*/

namespace Program
{
    class Program
    {
        static string fiveLetterWord()
        {
            const string jsonFileIn = "words.json";
            dynamic jsonFile = JsonConvert.DeserializeObject(File.ReadAllText(jsonFileIn));

            Random random = new Random();
            int randNumber = random.Next(0, jsonFile.Count);

            return jsonFile[randNumber];
        }
        static void Main(string[] args)
        {
            Console.WriteLine(fiveLetterWord());
            Console.WriteLine("Hello World");
            
        }
    }
}