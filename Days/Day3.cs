using AdventOfCode2025.Model;
using System.Text;

namespace AdventOfCode2025.Days
{
    public class Day3 : Day
    {
        public Day3()
            : base(3)
        { }

        public override void Solve(int Part)
        {
            if (Part == 1)
            {
                this.Part1();
            }
            else
            {
                this.Part2();
            }
        }

        private void Part1()
        {
            var sum = 0;
            foreach (var batteryPack in this.Input)
            {
                // Store positions of battery joltages
                var earliest = new Dictionary<int, int>();
                var latest = new Dictionary<int, int>();
                for (int i = 0; i < batteryPack.Length; i++)
                {
                    var battery = int.Parse(batteryPack[i].ToString());
                    if (!earliest.ContainsKey(battery))
                    {
                        earliest[battery] = i;
                    }

                    latest[battery] = i;
                }

                // Find the largest possible number
                var largest = this.FindLargestPair(earliest, latest);
                sum += largest;
            }

            Console.WriteLine($"Sum: {sum}");
        }

        private void Part2()
        {
            double sum = 0;
            foreach (var batteryPack in this.Input)
            {
                // Store positions of battery joltages
                var positions = new Dictionary<int, List<int>>();
                for (int i = 0; i < batteryPack.Length; i++)
                {
                    var battery = int.Parse(batteryPack[i].ToString());
                    if (positions.TryGetValue(battery, out var list))
                    {
                        list.Add(i);
                    }
                    else
                    {
                        positions[battery] = new List<int>() { i };
                    }
                }

                // Find the largest possible number
                var largest = this.FindLargestViaDFS(positions, new StringBuilder(), -1);
                sum += largest;
            }

            Console.WriteLine($"Sum: {sum}");
        }

        private int FindLargestPair(Dictionary<int,int> earliest, Dictionary<int, int> latest)
        {
            for (int i = 9; i >= 0; i--)
            {
                if (earliest.TryGetValue(i, out var first))
                {
                    for (int j = 9; j >= 0; j--)
                    {
                        if (latest.TryGetValue(j, out var last) && last > first)
                        {
                            return int.Parse($"{i}{j}");
                        }
                    }
                }
            }

            return 0;
        }

        private double FindLargestViaDFS(Dictionary<int, List<int>> positions, StringBuilder sb, int previousPosition)
        {
            if (sb.Length == 12)
            {
                return double.Parse(sb.ToString());
            }

            for (int i = 9; i >= 0; i--)
            {
                if (positions.TryGetValue(i, out var positionsList) && positionsList.Last() > previousPosition)
                {
                    for (int j = 0; j < positionsList.Count; j++)
                    {
                        var currentPosition = positionsList[j];
                        if (currentPosition > previousPosition)
                        {
                            sb.Append(i);
                            var value = this.FindLargestViaDFS(positions, sb, currentPosition);

                            if (value == -1)
                            {
                                sb.Remove(sb.Length - 1, 1);
                                break;
                            }
                            else
                            {
                                return value;
                            }
                        }
                    }
                }
            }

            return -1;
        }
    }
}
