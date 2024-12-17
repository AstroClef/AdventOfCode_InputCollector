using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_InputCollector.UI
{
    public enum DialogueID
    {
        START,
        YEAR, 
        DAY, 
        SESSIONTOKEN,
        ALLDAYS,
        DIRECTORY,
        DVTFILES
    }

    internal class Dialogue
    {
        private static readonly Dictionary<DialogueID, string> dialogue = new Dictionary<DialogueID, string>()
        {
            { 
                DialogueID.START, 
                $"Advent of Code - Project Input Collector - v{Program.VERSION}\n" +
                $"-------------------------------------------------------------\n" 
            },
            { 
                DialogueID.YEAR, 
                "Which project YEAR do you want?"
            },
            { 
                DialogueID.DAY, 
                "Which project DAY do you want?"
            },
            { 
                DialogueID.SESSIONTOKEN, 
                "Please enter the cookie session token used for Advent of Code? [Session Tokens change frequently]" 
            },
            { 
                DialogueID.ALLDAYS, 
                "Do you only need a specific day? (Y/N)" 
            },
            { 
                DialogueID.DIRECTORY,
                "Directory your Puzzle Input files be saved to?"
            },
            {
                DialogueID.DVTFILES,
                "Do you want to create empty DVT files for each day? (Y/N)"
            }
        };

        public static void WriteDialogue(DialogueID id)
        {
            Console.WriteLine(dialogue[id]);
        }
    }
}
