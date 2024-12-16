using AOC_InputCollector.UI;

namespace AOC_InputCollector
{
    internal class Program
    {
        public const string VERSION = "0.1.0";

        static void Main(string[] args)
        {
            (int, bool) responseYear;
            (int, bool) responseDay;
            (string?, bool) responseKey;

            Dialogue.WriteDialogue(DialogueID.START);
            do
            {
                responseYear = Responses.GetResponse<int>(DialogueID.YEAR);
                Console.Clear();
            } while(!responseYear.Item2);

            //TODO: Check if user wants a specific day or all available days

            do
            {
                responseDay = Responses.GetResponse<int>(DialogueID.DAY);
                Console.Clear();
            }
            while (!responseDay.Item2);

            do
            {
                responseKey = Responses.GetResponse<string>(DialogueID.SESSIONTOKEN);
                Console.Clear();
            } while (!responseKey.Item2);

            //Example of how the link should look like: https://adventofcode.com/2024/day/4/input
            Console.WriteLine($"https://adventofcode.com/{responseYear.Item1}/day/{responseDay.Item1}/input");
            
            //TODO: Build the web link into the FetchPuzzleInput method.
        }
        private static void FetchPuzzleInput(string link, string key)
        {
            throw new NotImplementedException();

            //TODO: Navigate to link and download the puzzle input to file ("Day<#>.txt")
        }
    }
}
