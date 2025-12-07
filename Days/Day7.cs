using AdventOfCode2025.Model;

namespace AdventOfCode2025.Days
{
    public class Day7 : Day
    {
        public Day7()
            : base(7)
        {}

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
            var total = 0;
            var tachyons = new bool[this.Input[0].Length];

            // 1. Find the starting point and initialize
            for (int i = 0; i < this.Input[0].Length; i++) 
            {
                if (this.Input[0][i] == 'S')
                {
                    tachyons[i] = true;
                }
            }

            // 2. Descend the input and split into two separate beams if a carat appears where a tachyon is
            for (int i = 1; i < this.Input.Length; i++)
            {
                for (int j = 0; j < this.Input[i].Length; j++)
                {
                    if (this.Input[i][j] == '^' && tachyons[j])
                    {
                        total++;
                        tachyons[j] = false;

                        if (j != 0)
                        {
                            tachyons[j - 1] = true;
                        }

                        if (j != this.Input[i].Length - 1)
                        {
                            tachyons[j + 1] = true;
                        }
                    }
                }
            }

            Console.WriteLine($"Number of splits: {total}");
        }

        private void Part2()
        {
            double total = 0;
            var tachyons = new double[this.Input[0].Length];

            // 1. Find the starting point and initialize
            for (int i = 0; i < this.Input[0].Length; i++)
            {
                if (this.Input[0][i] == 'S')
                {
                    tachyons[i] = 1;
                }
            }

            // 2. Descend the input and double into two separate paths if a carat appears where a tachyon is
            for (int i = 1; i < this.Input.Length; i++)
            {
                for (int j = 0; j < this.Input[i].Length; j++)
                {
                    if (this.Input[i][j] == '^' && tachyons[j] > 0)
                    {
                        if (j != 0)
                        {
                            tachyons[j - 1] += tachyons[j];
                        }

                        if (j != this.Input[i].Length - 1)
                        {
                            tachyons[j + 1] += tachyons[j];
                        }

                        tachyons[j] = 0;
                    }
                }
            }

            // 3. Count the total paths at the bottom
            for (int i = 0; i < tachyons.Length; i++) 
            {
                total += tachyons[i];
            }

            Console.WriteLine($"Total paths: {total}");
        }
    }
}
