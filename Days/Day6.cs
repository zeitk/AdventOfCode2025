using AdventOfCode2025.Model;
using System.Text;

namespace AdventOfCode2025.Days
{
    public class Day6 : Day
    {
        public Day6()
            : base(6)
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
            var table = new List<List<double>>();
            var operators = new List<string>();

            // 1. Sort the input into a table
            for (int i = 0; i < this.Input.Length; i++)
            {
                var line = this.Input[i];
                var sb = new StringBuilder();

                // Eliminate extra white space
                for (int j = 0; j < line.Length; j++)
                {
                    if (j != 0 && line[j] == ' ' && line[j - 1] == ' ') 
                    {
                        continue;
                    }

                    sb.Append(line[j]);
                }

                var values = sb.ToString().Trim().Split(' ');
                if (i == this.Input.Length - 1)
                {
                    operators = values.ToList();
                }
                else
                {
                    var row = new List<double>();
                    foreach (var val in values)
                    {
                        var numericVal = double.Parse(val.ToString());
                        row.Add(numericVal);
                    }

                    table.Add(row);
                }
            }

            // 2. Calculate by going down each column then add to the sum
            for (int i = 0; i < table[0].Count; i++)
            {
                double current = operators[i] == "*" ? 1 : 0;
                for (int j = 0; j < table.Count; j++)
                {
                    if (operators[i] == "*")
                    {
                        current *= table[j][i];
                    }
                    else
                    {
                        current += table[j][i];
                    }
                }

                sum += current;
            }

            Console.WriteLine($"Final sum: {sum}");
        }

        private void Part2()
        {
            double sum = 0;
            var operators = new List<char>();
            var indices = new List<int>();

            // 1. Find indices of operators
            var lastRow = this.Input[this.Input.Length - 1];
            for (int i = 0; i < lastRow.Length; i++)
            {
                if (lastRow[i] != ' ')
                {
                    operators.Add(lastRow[i]);
                    indices.Add(i);
                }
            }

            // 2. Build out each column's numbers and calculate
            for (int i = 0; i < indices.Count; i++)
            {
                var groupNumbers = new List<double>();
                var finalIndex = i != indices.Count - 1 
                    ? indices[i + 1] - 1 
                    : this.Input[0].Length;

                // Find the numbers from each column in this operator group
                for (int j = indices[i]; j < finalIndex; j++)
                {
                    var sb = new StringBuilder();
                    for (int k = 0; k < this.Input.Length - 1; k++)
                    {
                        if (this.Input[k][j] != ' ')
                        {
                            sb.Append(this.Input[k][j]);
                        }
                    }

                    var numericVal = double.Parse(sb.ToString());
                    groupNumbers.Add(numericVal);
                }

                // Calculate the total of this operator group
                double current = operators[i] == '*' ? 1 : 0;
                foreach (var number in groupNumbers)
                {
                    if (operators[i] == '*')
                    {
                        current *= number;
                    }
                    else
                    {
                        current += number;
                    }
                }

                sum += current;
            }

            Console.WriteLine($"Final sum: {sum}");
        }
    }
}
