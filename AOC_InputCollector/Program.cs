using AOC_InputCollector.UI;

namespace AOC_InputCollector
{
    internal class Program
    {
        public const string VERSION = "1.0.0";
        private static string rootlink = "https://adventofcode.com/";
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
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            CollectInformation();
            if (currentDate < DateOnly.Parse($"12/1/{responseYear.Item1}"))
            {
                Console.WriteLine($"The Advent of Code for the year you've selected ({responseYear.Item1}) hasn't started yet.");
                return;
            }
            if (responseYear.Item1 < 2015)
            {
                Console.WriteLine($"The year you selected: {responseYear.Item1}\nAdvent of Code only started in 2015.");
                return;
            }
            if (responseDay.Item1 > 25)
            {
                Console.WriteLine("Advent of Code only has 25 days.");
                return;
            }

            if (needFullInventory)
            {
                InventoryPuzzleInput(currentDate).Wait();
            }
            else
            {
                //Example of how the link should look like: https://adventofcode.com/2024/day/4/input
                FetchPuzzleInput(rootlink + responseYear.Item1 + "/day/" + responseDay.Item1 + "/input", responseDay.Item1!).Wait();
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

        private static async Task FetchPuzzleInput(string link, int day)
        {
            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.Add("Cookie", $"session={responseKey.Item1}");
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

        private static async Task InventoryPuzzleInput(DateOnly date)
        {   
            for (int day = 1;  day <= date.Day && day <= 25; day++)
            {             
                await FetchPuzzleInput(rootlink + responseYear.Item1 + $"/day/{day}/input", day);
            }
        }
    }
}
