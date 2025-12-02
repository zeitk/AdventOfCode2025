namespace AdventOfCode2025.Model
{
    public abstract class Day
    {
        protected string[] Lines;

        public Day(int Day)
        {
            this.Lines = Utils.Utils.GetInput(Day);
        }

        public abstract void Solve(int Part);
    }
}
