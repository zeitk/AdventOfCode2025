using AdventOfCode2025.Model;

namespace AdventOfCode2025.Days
{
    public class Day2 : Day
    {
        public Day2()
            : base(2)
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
            double sum = 0;
            var ids = this.Lines[0].Split(',');
            foreach (var id in ids)
            {
                var digits = id.Split('-');
                var start = double.Parse(digits[0]);
                var end = double.Parse(digits[1]);

                var startLen = start.ToString().Length % 2 == 1 
                    ? start.ToString().Length / 2 + 1
                    : start.ToString().Length / 2;
                var endLen = end.ToString().Length % 2 == 1
                    ? end.ToString().Length / 2 + 1
                    : end.ToString().Length / 2;

                for (int i = startLen; i <= endLen; i++)
                {
                    double baseVal = Math.Pow(10, i - 1);
                    while (baseVal.ToString().Length == startLen)
                    {
                        var doubledVal = double.Parse($"{baseVal}{baseVal}");
                        if (doubledVal >= start && doubledVal <= end)
                        {
                            sum += doubledVal;
                        }
                        else if (doubledVal > end)
                        {
                            break;
                        }

                        baseVal++;
                    }
                }
            }

            Console.WriteLine($"Answer: {sum}");
        }

        private void Part2()
        {
            double sum = 0;
            double largest = 0;
            var ranges = new Dictionary<double, double>();
            var found = new HashSet<double>();
            var ids = this.Lines[0].Split(',');

            foreach (var id in ids)
            {
                var digits = id.Split('-');
                var start = double.Parse(digits[0]);
                var end = double.Parse(digits[1]);
                ranges[start] = end;

                if (end > largest)
                {
                    largest = end;
                }
            }

            var finalLen = largest.ToString().Length / 2;
            for (int i = 1; i.ToString().Length <= finalLen; i++)
            {
                var baseString = $"{i}{i}";
                var baseVal = double.Parse(baseString);

                while (baseVal <= largest)
                {
                    if (found.Contains(baseVal))
                    {
                        break;
                    }

                    foreach (var key in ranges.Keys)
                    {
                        if (baseVal >= key && baseVal <= ranges[key])
                        {
                            found.Add(baseVal);
                            sum += baseVal;
                        }
                    }

                    baseString = $"{baseString}{i}";
                    baseVal = double.Parse(baseString);
                }
            }

            Console.WriteLine($"Answer: {sum}");
        }
    }
}
