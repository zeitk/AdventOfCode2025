using AdventOfCode2025.Model;

namespace AdventOfCode2025.Days
{
    public class Day4 : Day
    {
        private char[][] inputSet = new char[0][];

        public Day4()
            : base(4)
        { }

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
            var totalSpots = 0;
            for (int i = 0; i < this.Input.Length; i++)
            {
                for (int j = 0; j < this.Input[i].Length; j++)
                {
                    if (this.Input[i][j] == '@')
                    {
                        var surroundingRolls = 0;
                        surroundingRolls += this.AddOneIfHasARoll(i - 1, j - 1);
                        surroundingRolls += this.AddOneIfHasARoll(i - 1, j);
                        surroundingRolls += this.AddOneIfHasARoll(i - 1, j + 1);
                        surroundingRolls += this.AddOneIfHasARoll(i, j - 1);
                        surroundingRolls += this.AddOneIfHasARoll(i, j + 1);
                        surroundingRolls += this.AddOneIfHasARoll(i + 1, j - 1);
                        surroundingRolls += this.AddOneIfHasARoll(i + 1, j);
                        surroundingRolls += this.AddOneIfHasARoll(i + 1, j + 1);

                        if (surroundingRolls < 4)
                        {
                            totalSpots++;
                        }
                    }
                }
            }

            Console.WriteLine($"Total spots: {totalSpots}");
        }

        private void Part2()
        {
            this.inputSet = this.ConvertInputToEditableTable();
            var totalRolls = 0;

            while (true)
            {
                var pickupSpots = new List<(int, int)>();
                this.AssessRollsToPickup(pickupSpots);
                if (pickupSpots.Count == 0)
                {
                    break;
                }

                this.PickupRolls(pickupSpots);
                totalRolls += pickupSpots.Count;
            }

            Console.WriteLine($"Total rolls removed: {totalRolls}");
        }

        private void PickupRolls(List<(int, int)> spots)
        {
            foreach (var pickupSpot in spots)
            {
                var i = pickupSpot.Item1;
                var j = pickupSpot.Item2;
                this.inputSet[i][j] = '.';
            }
        }

        private void AssessRollsToPickup(List<(int, int)> spots)
        {
            for (int i = 0; i < this.inputSet.Length; i++)
            {
                for (int j = 0; j < this.inputSet[i].Length; j++)
                {
                    if (this.inputSet[i][j] == '@')
                    {
                        var surroundingRolls = 0;
                        surroundingRolls += this.AddOneIfHasARollChar(i - 1, j - 1);
                        surroundingRolls += this.AddOneIfHasARollChar(i - 1, j);
                        surroundingRolls += this.AddOneIfHasARollChar(i - 1, j + 1);
                        surroundingRolls += this.AddOneIfHasARollChar(i, j - 1);
                        surroundingRolls += this.AddOneIfHasARollChar(i, j + 1);
                        surroundingRolls += this.AddOneIfHasARollChar(i + 1, j - 1);
                        surroundingRolls += this.AddOneIfHasARollChar(i + 1, j);
                        surroundingRolls += this.AddOneIfHasARollChar(i + 1, j + 1);

                        if (surroundingRolls < 4)
                        {
                            spots.Add((i, j));
                        }
                    }
                }
            }
        }

        private char[][] ConvertInputToEditableTable()
        {
            var inputSet = new char[this.Input.Length][];
            for (int i = 0; i < this.Input.Length; i++)
            {
                inputSet[i] = new char[this.Input[i].Length];
                for (int j = 0; j < this.Input[i].Length; j++)
                {
                    inputSet[i][j] = this.Input[i][j];
                }
            }

            return inputSet;
        }

        private int AddOneIfHasARollChar(int i, int j)
        {
            return i >= 0 
                    && j >= 0
                    && i < this.inputSet.Length
                    && j < this.inputSet[i].Length
                    && this.inputSet[i][j] == '@'
                ? 1
                : 0;
        }

        private int AddOneIfHasARoll(int i, int j)
        {
            return i >= 0 
                    && j >= 0
                    && i < this.Input.Length 
                    && j < this.Input[i].Length 
                    && this.Input[i][j] == '@' 
                ? 1 
                : 0;
        }
    }
}
