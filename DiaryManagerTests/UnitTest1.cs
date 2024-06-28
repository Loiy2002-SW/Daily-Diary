using DiaryManager;

namespace DiaryManagerTests
{
    public class UnitTest1
    {
        private const string TestDiaryFilePath = "test_diary.txt";

        [Fact]
        public void TestReadDiaryFile()
        {
            // Arrange
            File.WriteAllText(TestDiaryFilePath, "2024-06-27\nTest entry");

            // Act
            DailyDiary diary = new DailyDiary();
            diary.ReadDiaryFile();

            // Assert
            // Verify that the content is read correctly -->
            // (checking console output in a real test).
        }

        [Fact]
        public void TestAddEntry()
        {
            // Arrange
            if (File.Exists(TestDiaryFilePath))
            {
                File.Delete(TestDiaryFilePath);
            }

            DailyDiary diary = new DailyDiary();

            // Act
            diary.AddEntry();

            // Assert
            Assert.True(File.Exists(TestDiaryFilePath));
            string[] lines = File.ReadAllLines(TestDiaryFilePath);
            Assert.Single(lines);
        }
    }
}