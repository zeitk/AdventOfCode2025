using AdventOfCode2025.Model;

if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run <daynumber> <partnumber>");
    return;
}

int Day = int.Parse(args[0]);
int Part = args.Length > 1 ? int.Parse(args[1]) : 1;

Console.WriteLine($"------- Day {Day} Part {Part} -------");
var DaySolver = DayFactory.Create(Day);
DaySolver.Solve(Part);