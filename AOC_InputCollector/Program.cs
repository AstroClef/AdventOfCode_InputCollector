using AOC_InputCollector.UI;

namespace AOC_InputCollector
{
    internal class Program
    {
        public const string VERSION = "0.1.0";
        private static string weblink = "https://adventofcode.com/";
        private static (int, bool) responseYear;
        private static (int, bool) responseDay;
        private static (string?, bool) responseKey;
        private static (string?, bool) responseAllDays;
        private static (string?, bool) responseDVTFiles;
        private static (string?, bool) responseDirectory;
        private static bool needFullInventory = false;
        private static bool createEmptyDVTFiles = false;

        static void Main(string[] args)
        {
            CollectInformation();

            if (needFullInventory)
            {
                InventoryPuzzleInput();
                //TODO: For each available day, download the puzzle input.
            }
            else
            {
                //Example of how the link should look like: https://adventofcode.com/2024/day/4/input
                weblink = weblink + responseYear.Item1 + "/day/" + responseDay.Item1 + "/input";
                FetchPuzzleInput(weblink, responseKey.Item1!, responseDay.Item1).Wait();
            }
        }

        private static void CollectInformation()
        {
            Dialogue.WriteDialogue(DialogueID.START);
            do
            {
                responseYear = Responses.GetResponse<int>(DialogueID.YEAR);
                Console.Clear();
            } while (!responseYear.Item2);

            do
            {
                responseAllDays = Responses.GetResponse<string>(DialogueID.ALLDAYS);
                Console.Clear();
            }
            while (!responseAllDays.Item2 || (!responseAllDays.Item1!.Equals("Y", StringComparison.CurrentCultureIgnoreCase) && !responseAllDays.Item1.Equals("N", StringComparison.CurrentCultureIgnoreCase)));

            if (responseAllDays.Item1!.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
            {
                do
                {
                    responseDay = Responses.GetResponse<int>(DialogueID.DAY);
                    Console.Clear();
                }
                while (!responseDay.Item2);
            }
            else
            {
                needFullInventory = true;
            }

            do
            {
                responseDVTFiles = Responses.GetResponse<string>(DialogueID.DVTFILES);
                Console.Clear();
            } while (!responseDVTFiles.Item2 || (!responseDVTFiles.Item1!.Equals("Y", StringComparison.CurrentCultureIgnoreCase) && !responseDVTFiles.Item1!.Equals("N", StringComparison.CurrentCultureIgnoreCase)));

            if (responseDVTFiles.Item1!.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
            {
                createEmptyDVTFiles = true;
            }

            do
            {
                responseDirectory = Responses.GetResponse<string>(DialogueID.DIRECTORY);
                Console.Clear();
            } while (!responseDirectory.Item2 || !Path.Exists(responseDirectory.Item1));

            do
            {
                responseKey = Responses.GetResponse<string>(DialogueID.SESSIONTOKEN);
                Console.Clear();
            } while (!responseKey.Item2);
        }

        private static async Task FetchPuzzleInput(string link, string key, int day)
        {
            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.Add("Cookie", $"session={key}");
                try
                {
                    HttpResponseMessage response = await client.GetAsync(link);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    string fileName = $"Day{day}.txt";
                    string filePath = Path.Combine(responseDirectory.Item1!, fileName);

                    if (createEmptyDVTFiles)
                    {
                        string dvtFilePath = Path.Combine(responseDirectory.Item1!, $"Day{day}_DVT.txt");
                        if (!File.Exists(dvtFilePath))
                        {
                            File.Create(dvtFilePath).Close();
                            Console.WriteLine($"DVT file for \"{fileName}\" created.");
                        }
                        else
                        {
                            Console.WriteLine($"DVT file for \"{fileName}\" already exists.");
                        }
                        if (!File.Exists(filePath))
                        {
                            await File.WriteAllTextAsync(filePath, responseBody);
                            Console.WriteLine($"File \"{fileName}\" saved.");
                        }
                        else
                        {
                            Console.WriteLine($"File \"{fileName}\" already exists.");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Failed to download the puzzle input.");
                }
            }
        }

        private static void InventoryPuzzleInput()
        {
            throw new NotImplementedException();

            //TODO: Check if the file exists, if not, download it.
        }
    }
}
