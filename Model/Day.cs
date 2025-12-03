namespace AdventOfCode2025.Model
{
    public abstract class Day
    {
        protected string[] Input;

        public Day(int Day)
        {
            this.Input = Utils.Utils.GetInput(Day);
        }

        public abstract void Solve(int Part);
    }
}
