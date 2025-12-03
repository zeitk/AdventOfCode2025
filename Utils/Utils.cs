namespace AdventOfCode2025.Utils
{
    public static class Utils
    {
        public static string[] GetInput(int day)
        {
            string folderName = "AdventOfCode2025";
            string curDirectory = Directory.GetCurrentDirectory();
            int index = curDirectory.LastIndexOf(folderName);
            string projectRoot = curDirectory.Substring(0, index + folderName.Length);
            string filePath = Path.Combine(projectRoot, "InputFiles", $"Day{day}.txt");
            string[] lines = File.ReadAllLines(filePath);
            return lines;
        }
    }
}
