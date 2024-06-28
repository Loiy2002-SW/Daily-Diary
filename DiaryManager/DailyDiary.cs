using System.Text.RegularExpressions;

namespace DiaryManager
{

    public class DailyDiary
    {
        private const string DiaryFilePath = "../../../diarydata.txt";
        private const string DateFormat = @"^\d{4}-\d{2}-\d{2}$";

        public void RunApp()
        {
            string choice = string.Empty;
            while (choice != "E")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("============================");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("R. Read Diary");
                Console.WriteLine("A. Add New Entry");
                Console.WriteLine("D. Delete Entry");
                Console.WriteLine("C. Count All Lines");
                Console.WriteLine("S. Search Entries");
                Console.WriteLine("E. Exit");
                Console.WriteLine("============================");

                choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "R":
                        ReadDiaryFile();
                        break;
                    case "A":
                        AddEntry();
                        break;
                    case "D":
                        DeleteEntry();
                        break;
                    case "C":
                        CountLines();
                        break;
                    case "S":
                        SearchEntries();
                        break;
                    case "E":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("============================");
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.WriteLine("============================");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                }
            }
        }


        private bool IsValidDate(string date) => Regex.IsMatch(date, DateFormat);

        public void ReadDiaryFile()
        {
            try
            {
                if (File.Exists(DiaryFilePath))
                {
                    string[] lines = File.ReadAllLines(DiaryFilePath);
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("============================");
                    Console.WriteLine("Diary file not found.");
                    Console.WriteLine("============================");
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================");
                Console.WriteLine($"Error reading diary file: {ex.Message}");
                Console.WriteLine("============================");
                Console.ForegroundColor = ConsoleColor.Blue;
            }
        }

        public void AddEntry()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("============================");
                Console.WriteLine("Enter the date (YYYY-MM-DD):");
                Console.WriteLine("============================");
                string date = Console.ReadLine();

                while (!IsValidDate(date))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("============================");
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    Console.WriteLine("============================");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    date = Console.ReadLine();
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("============================");
                Console.WriteLine("Enter the content:");
                Console.WriteLine("============================");
                string content = Console.ReadLine();

                string formattedEntry = $"{Environment.NewLine}{date}{Environment.NewLine}{content}{Environment.NewLine}";
                File.AppendAllText(DiaryFilePath, formattedEntry);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("============================");
                Console.WriteLine("Entry added successfully.");
                Console.WriteLine("============================");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================");
                Console.WriteLine($"Error adding entry: {ex.Message}");
                Console.WriteLine("============================");
                Console.ForegroundColor = ConsoleColor.Blue;
            }
        }

        public void DeleteEntry()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("============================");
                Console.WriteLine("Enter the date (YYYY-MM-DD) of the entry to delete:");
                Console.WriteLine("============================");
                string date = Console.ReadLine();

                while (!IsValidDate(date))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("============================");
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    Console.WriteLine("============================");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    date = Console.ReadLine();
                }

                if (File.Exists(DiaryFilePath))
                {
                    var lines = File.ReadAllLines(DiaryFilePath).ToList();
                    bool entryFound = false;

                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].StartsWith(date))
                        {
                            entryFound = true;
                            lines.RemoveAt(i); // Remove the date line
                            if (i < lines.Count && !IsValidDate(lines[i]))
                            {
                                lines.RemoveAt(i); // Remove the content line
                            }
                            break;
                        }
                    }

                    if (!entryFound)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("============================");
                        Console.WriteLine("No entry found for the specified date.");
                        Console.WriteLine("============================");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        return;
                    }

                    File.WriteAllLines(DiaryFilePath, lines);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("============================");
                    Console.WriteLine("Entry deleted successfully.\n");
                    Console.WriteLine("============================");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("============================");
                    Console.WriteLine("Diary file not found.");
                    Console.WriteLine("============================");
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================");
                Console.WriteLine($"Error deleting entry: {ex.Message}");
                Console.WriteLine("============================");
                Console.ForegroundColor = ConsoleColor.Blue;
            }
        }

        public void CountLines()
        {
            try
            {
                if (File.Exists(DiaryFilePath))
                {
                    int lineCount = File.ReadAllLines(DiaryFilePath).Length;

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("============================");
                    Console.WriteLine($"Total number of lines: {lineCount}\n");
                    Console.WriteLine("============================");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("============================");
                    Console.WriteLine("Diary file not found.");
                    Console.WriteLine("============================");
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================");
                Console.WriteLine($"Error counting lines: {ex.Message}");
                Console.WriteLine("============================");
                Console.ForegroundColor = ConsoleColor.Blue;
            }
        }

        public void SearchEntries()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("============================");
                Console.WriteLine("Enter the date (YYYY-MM-DD) to retrieve data:");
                Console.WriteLine("============================");
                string date = Console.ReadLine();

                while (!IsValidDate(date))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("============================");
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    Console.WriteLine("============================");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    date = Console.ReadLine();
                }

                if (File.Exists(DiaryFilePath))
                {
                    var lines = File.ReadAllLines(DiaryFilePath);
                    bool entryFound = false;

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Equals(date))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("============================");
                            Console.WriteLine("Data retrieved: ");
                            Console.WriteLine("============================");
                            entryFound = true;
                            Console.WriteLine(lines[i]); // Print the date
                            if (i + 1 < lines.Length)
                            {
                                Console.WriteLine(lines[i + 1]); // Print the content
                            }
                            Console.WriteLine(); // Add a blank line between entries
                        }
                    }

                    if (!entryFound)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("============================");
                        Console.WriteLine("No entries found for the specified date.");
                        Console.WriteLine("============================");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("============================");
                    Console.WriteLine("Diary file not found.");
                    Console.WriteLine("============================");
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================");
                Console.WriteLine($"Error searching entries: {ex.Message}");
                Console.WriteLine("============================");
                Console.ForegroundColor = ConsoleColor.Blue;
            }
        }
    }

}