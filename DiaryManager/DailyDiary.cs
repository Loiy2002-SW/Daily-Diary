
using System.Text.RegularExpressions;

namespace DiaryManager
{
    public class DailyDiary
    {
        private const string DiaryFilePath = "../../../diarydata.txt";
        private const string DateFormat = @"^\d{4}-\d{2}-\d{2}$";

        public void RunApp()
        {
            var menuOptions = new Dictionary<string, Action>
            {
                { "R", ReadDiaryFile },
                { "A", AddEntry },
                { "D", DeleteEntry },
                { "C", CountLines },
                { "S", SearchEntries },
                { "E", () => Environment.Exit(0) }
            };

            string choice = string.Empty;
            while (choice != "E")
            {
                DisplayMenu();
                choice = Console.ReadLine().ToUpper();

                if (menuOptions.ContainsKey(choice))
                {
                    menuOptions[choice].Invoke();
                }
                else
                {
                    DisplayMessage("Invalid choice. Please try again.", ConsoleColor.Red);
                }
            }
        }

        private bool IsValidDate(string date) => Regex.IsMatch(date, DateFormat);

        private void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=================================");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("R. Read Diary");
            Console.WriteLine("A. Add New Entry");
            Console.WriteLine("D. Delete Entry");
            Console.WriteLine("C. Count All Lines");
            Console.WriteLine("S. Search Entries");
            Console.WriteLine("E. Exit");
            Console.WriteLine("=================================");
            Console.ResetColor();
        }

        private void DisplayMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("=================================");
            Console.WriteLine(message);
            Console.WriteLine("=================================");
            Console.ResetColor();
        }

        public void ReadDiaryFile()
        {
            if (File.Exists(DiaryFilePath))
            {
                var lines = File.ReadAllLines(DiaryFilePath);
                if (lines.Length > 0)
                {
                    foreach (var line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
                else
                {
                    DisplayMessage("Diary file is empty.", ConsoleColor.Yellow);
                }
            }
            else
            {
                DisplayMessage("Diary file not found.", ConsoleColor.Red);
            }
        }

        public void AddEntry()
        {
            Console.WriteLine("Enter the date (YYYY-MM-DD):");
            string date = ReadValidDate();
            Console.WriteLine("Enter the content:");
            string content = Console.ReadLine();

            string formattedEntry = $"{date}{Environment.NewLine}{content}{Environment.NewLine}";

            try
            {
                File.AppendAllText(DiaryFilePath, formattedEntry);
                DisplayMessage("Entry added successfully.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error adding entry: {ex.Message}", ConsoleColor.Red);
            }
        }

        public void DeleteEntry()
        {
            Console.WriteLine("Enter the date (YYYY-MM-DD) of the entry to delete:");
            string date = ReadValidDate();

            if (File.Exists(DiaryFilePath))
            {
                var lines = File.ReadAllLines(DiaryFilePath).ToList();
                bool entryFound = false;

                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Equals(date))
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

                if (entryFound)
                {
                    File.WriteAllLines(DiaryFilePath, lines);
                    DisplayMessage("Entry deleted successfully.", ConsoleColor.Green);
                }
                else
                {
                    DisplayMessage("No entry found for the specified date.", ConsoleColor.Red);
                }
            }
            else
            {
                DisplayMessage("Diary file not found.", ConsoleColor.Red);
            }
        }

        public void CountLines()
        {
            if (File.Exists(DiaryFilePath))
            {
                int lineCount = File.ReadAllLines(DiaryFilePath).Length;
                DisplayMessage($"Total number of lines: {lineCount}", ConsoleColor.DarkYellow);
            }
            else
            {
                DisplayMessage("Diary file not found.", ConsoleColor.Red);
            }
        }

        public void SearchEntries()
        {
            Console.WriteLine("Enter the date (YYYY-MM-DD) to retrieve data:");
            string date = ReadValidDate();

            if (File.Exists(DiaryFilePath))
            {
                var lines = File.ReadAllLines(DiaryFilePath);
                bool entryFound = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Equals(date))
                    {
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
                    DisplayMessage("No entries found for the specified date.", ConsoleColor.Red);
                }
            }
            else
            {
                DisplayMessage("Diary file not found.", ConsoleColor.Red);
            }
        }

        private string ReadValidDate()
        {
            string date = Console.ReadLine();
            while (!IsValidDate(date))
            {
                DisplayMessage("Invalid date format. Please use YYYY-MM-DD.", ConsoleColor.Red);
                date = Console.ReadLine();
            }
            return date;
        }
    }
}
