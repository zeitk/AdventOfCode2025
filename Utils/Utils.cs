namespace AdventOfCode2025.Utils
{
    public static class Utils
    {
        public static string[] GetInput(int day)
        {
            string folderName = "AdventOfCode2025";
            string curDirectory = Directory.GetCurrentDirectory().Split($@"\{folderName}")[0];
            string dayName = $"Day{day.ToString()}";
            string filePath = @$"{curDirectory}\{folderName}\InputFiles\{dayName}.txt";
            string[] lines = File.ReadAllLines(filePath);
            return lines;
        }
    }
}
