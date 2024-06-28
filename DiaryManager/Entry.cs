
namespace DiaryManager
{
    public class Entry
    {
        public string? Date { get; set; }
        public string? Content { get; set; }

        public override string ToString()
        {
            return $"{Date}: {Content}";
        }
    }

}