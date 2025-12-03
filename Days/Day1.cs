using AdventOfCode2025.Model;

namespace AdventOfCode2025.Days
{
    public class Day1 : Day
    {
        public Day1()
            : base(1) 
        {}

        public override void Solve(int Part)
        {
            if (Part == 1)
            {
                Part1();
            }
            else
            {
                Part2();
            }
        }

        private void Part1()
        {
            var numZeros = 0;
            var position = 50;
            foreach (var line in Input)
            {
                var dir = line[0] == 'L' ? -1 : 1;
                var dist = int.Parse(line.Substring(1));

                position += dist * dir;
                position %= 100;

                if (position == 0)
                {
                    numZeros++;
                }
            }

            Console.WriteLine($"Password: {numZeros}");
        }

        private void Part2()
        {
            var numZeros = 0;
            var position = 50;
            foreach (var line in Input)
            {
                var dir = line[0] == 'L' ? -1 : 1;
                var dist = int.Parse(line.Substring(1));

                var startIsZero = position == 0 ? 1 : 0;
                position += dir * dist;
                if (dir == -1)
                {
                    if (position <= 0)
                    {
                        var zerosToAdd = Math.Abs(position) / 100 + 1 - startIsZero;
                        numZeros += zerosToAdd;
                        position %= 100;
                        if (position != 0)
                        {
                            position += 100;
                        }
                    }
                }
                else
                {
                    var zerosToAdd = position / 100;
                    numZeros += zerosToAdd;
                    position %= 100;
                }
            }

            Console.WriteLine($"Password: {numZeros}");
        }
    }
}
