using AdventOfCode2025.Days;

namespace AdventOfCode2025.Model
{
    public static class DayFactory
    {
        public static Day Create(int Day, int Part)
        {
            switch (Day)
            {
                case 1:
                    return new Day1(Part);
                default:
                    throw new Exception($"Day {Day} not implement yet!");
            }
        }
    }
}
