using AdventOfCode2025.Model;

namespace AdventOfCode2025.Days
{
    public class Day5 : Day
    {
        public Day5() 
            : base(5)
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
            // 1. Create base ranges and ingredient list
            var ranges = new List<(double, double)>();
            var ingredients = new List<double>();
            var freshIngredients = new List<double>();

            foreach (var line in this.Input)
            {
                if (line.Contains('-'))
                {
                    var numbers = line.Split('-');
                    var start = double.Parse(numbers[0]);
                    var end = double.Parse(numbers[1]);
                    ranges.Add((start, end));
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    var val = double.Parse(line);
                    ingredients.Add(val);
                }
            }

            // 2. Sort inputs
            ingredients.Sort();
            ranges.Sort((a,b) => a.Item1 - b.Item1 > 0 ? 1 : -1);

            // 3. Loop through ranges and compare against ingredients
            var index = 0;
            foreach (var range in ranges)
            {
                while (index < ingredients.Count && ingredients[index] <= range.Item2)
                {
                    if (ingredients[index] >= range.Item1)
                    {
                        freshIngredients.Add(index);
                    }

                    index++;
                }
            }

            Console.WriteLine($"Fresh ingredient count: {freshIngredients.Count}");
        }

        private void Part2()
        {
            // 1. Create base ranges and ingredient
            var ranges = new List<(double, double)>();
            foreach (var line in this.Input)
            {
                if (line.Contains('-'))
                {
                    var numbers = line.Split('-');
                    var start = double.Parse(numbers[0]);
                    var end = double.Parse(numbers[1]);
                    ranges.Add((start, end));
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }
            }

            // 2. Sort ranges
            ranges.Sort((a, b) => a.Item1 - b.Item1 > 0 ? 1 : -1);

            // 3. Combine ranges
            var rangesToIgnore = new HashSet<int>();
            for (int i = 1; i < ranges.Count; i++)
            {
                // Combine ranges if they overlap 
                if (ranges[i].Item1 <= ranges[i - 1].Item2)
                {
                    rangesToIgnore.Add(i - 1);
                    var start = Math.Min(ranges[i].Item1, ranges[i - 1].Item1);
                    var end = Math.Max(ranges[i].Item2, ranges[i - 1].Item2);
                    ranges[i] = (start, end);
                }
            }

            // 4. Sum the total span of each range
            double sum = 0;
            for (int i = 0; i < ranges.Count; i++)
            {
                if (!rangesToIgnore.Contains(i))
                {
                    sum += ranges[i].Item2 - ranges[i].Item1 + 1;
                }
            }

            Console.WriteLine($"Fresh ingredient range: {sum}");
        }
    }
}
