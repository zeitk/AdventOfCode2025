using AdventOfCode2025.Days;

namespace AdventOfCode2025.Model
{
    public static class DayFactory
    {
        public static Day Create(int Day)
        {
            switch (Day)
            {
                case 1:
                    return new Day1();
                case 2: 
                    return new Day2();
                case 3:
                    return new Day3();
                case 4:
                    return new Day4();
                case 5:
                    return new Day5();
                case 6:
                    return new Day6();
                default:
                    throw new Exception($"Day {Day} not implement yet!");
            }
        }
    }
}
