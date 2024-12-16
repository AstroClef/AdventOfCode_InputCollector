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
        SESSIONTOKEN
    }

    internal class Dialogue
    {
        private static readonly Dictionary<DialogueID, string> dialogue = new Dictionary<DialogueID, string>()
        {
            { DialogueID.START, 
                $"Advent of Code - Project Input Collector - v{Program.VERSION}\n" +
                $"-------------------------------------------------------------\n" },
            { DialogueID.YEAR, "Which project YEAR do you want?" },
            { DialogueID.DAY, "Which project DAY do you want?" },
            { DialogueID.SESSIONTOKEN, "Please enter the cookie session token used for Advent of Code? [Session Tokens change frequently]" }
        };

        public static void WriteDialogue(DialogueID id)
        {
            Console.WriteLine(dialogue[id]);
        }
    }
}
