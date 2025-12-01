namespace AdventOfCode2025.Model
{
    public abstract class Day
    {
        protected string[] Lines;
        protected int Part;

        public Day(int Day, int Part)
        {
            this.Lines = Utils.Utils.GetInput(Day);
            this.Part = Part;
        }

        public abstract void Solve();
    }
}
