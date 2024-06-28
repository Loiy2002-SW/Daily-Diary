namespace DiaryManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Welcome to Diary Manager app");
            DailyDiary diary = new DailyDiary();
            diary.RunApp();
        }
    }
}
