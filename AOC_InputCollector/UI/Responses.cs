using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_InputCollector.UI
{
    internal class Responses
    {
        public static (T?, bool) GetResponse<T>(DialogueID id)
        {
            Dialogue.WriteDialogue(id);
            string userInput = Console.ReadLine() ?? "";
            T? response = default;
            try
            {
                response = (T)Convert.ChangeType(userInput, typeof(T));
            }
            catch
            {
                return (response, false);
            }
            return (response, true);
        }
    }
}
