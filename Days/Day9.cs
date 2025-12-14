using AdventOfCode2025.Model;
using System;
using System.Diagnostics.Metrics;
using System.Linq;

namespace AdventOfCode2025.Days
{
    public class Day9 : Day
    {
        Dictionary<double, List<(double, double)>> X_WALLS = new Dictionary<double, List<(double, double)>>();
        Dictionary<double, List<(double, double)>> Y_WALLS = new Dictionary<double, List<(double, double)>>();
        List<double> X_WALL_KEYS = new List<double>();
        List<double> Y_WALL_KEYS = new List<double>();

        public Day9()
            : base(9)
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
            var corners = new List<(double, double)>();
            foreach (var line in this.Input)
            {
                var numbers = line.Split(',');
                var x = double.Parse(numbers[0]);
                var y = double.Parse(numbers[1]);
                corners.Add((x, y));
            }

            double max = 0;
            // lazy n^2 algorithm
            foreach (var corner in corners)
            {
                foreach (var other in corners)
                {
                    var xDiff = Math.Abs(corner.Item1 - other.Item1) + 1;
                    var yDiff = Math.Abs(corner.Item2 - other.Item2) + 1;
                    var size = xDiff * yDiff;
                    max = Math.Max(max, size);
                }
            }

            Console.WriteLine($"Largest: {max}");
        }

        private void Part2()
        {
            var corners = new List<(double, double)>();
            
            // Find the locations of all corners and walls
            var lastLine = this.Input.Last().Split(',');
            var lastX = double.Parse(lastLine[0]);
            var lastY = double.Parse(lastLine[1]);
            (double, double) previous = (lastX, lastY);

            foreach (var line in this.Input)
            {
                var numbers = line.Split(',');
                var x = double.Parse(numbers[0]);
                var y = double.Parse(numbers[1]);
                corners.Add((x, y));

                if (x == previous.Item1)
                {
                    var wall = (Math.Min(y, previous.Item2), Math.Max(y, previous.Item2));
                    if (X_WALLS.TryGetValue(x, out var walls))
                    {
                        walls.Add(wall);
                    }
                    else
                    {
                        X_WALLS.Add(x, new List<(double, double)>() { wall });
                    }
                }
                else
                {
                    var wall = (Math.Min(x, previous.Item1), Math.Max(x, previous.Item1));
                    if (Y_WALLS.TryGetValue(y, out var walls))
                    {
                        walls.Add(wall);
                    }
                    else
                    {
                        Y_WALLS.Add(y, new List<(double, double)>() { wall });
                    }
                }

                previous = (x, y);
            }

            // Sort the walls locations
            X_WALL_KEYS = X_WALLS.Keys.OrderBy(k => k).ToList();
            Y_WALL_KEYS = Y_WALLS.Keys.OrderBy(k => k).ToList();

            double max = 0;
            for (int i = 0; i < corners.Count; i++)
            {
                for (int j = i + 1; j < corners.Count; j++)
                {
                    var corner1 = corners[i];
                    var corner2 = corners[j];

                    var min_x = Math.Min(corner1.Item1, corner2.Item1);
                    var min_y = Math.Min(corner1.Item2, corner2.Item2);
                    var max_x = Math.Max(corner1.Item1, corner2.Item1);
                    var max_y = Math.Max(corner1.Item2, corner2.Item2);

                    var wall_x1 = (corner1.Item1, min_y, max_y);
                    var wall_x2 = (corner2.Item1, min_y, max_y);
                    var wall_y1 = (corner1.Item2, min_x, max_x);
                    var wall_y2 = (corner2.Item2, min_x, max_x);

                    var corner3 = (corner1.Item1, corner2.Item2);
                    var corner4 = (corner2.Item1, corner1.Item2);

                    // check whether all walls do not cross an existing wall, and that the two unknown corners are inbounds
                    if (this.WallIsValid(wall_x1, Y_WALL_KEYS, Y_WALLS) 
                        && this.WallIsValid(wall_x2, Y_WALL_KEYS, Y_WALLS) 
                        && this.WallIsValid(wall_y1, X_WALL_KEYS, X_WALLS) 
                        && this.WallIsValid(wall_y2, X_WALL_KEYS, X_WALLS)
                        && this.CornerIsInbounds(corner3) 
                        && this.CornerIsInbounds(corner4))
                    {
                        var xDiff = Math.Abs(corner1.Item1 - corner2.Item1) + 1;
                        var yDiff = Math.Abs(corner1.Item2 - corner2.Item2) + 1;
                        var size = xDiff * yDiff;
                        max = Math.Max(max, size);
                    }
                }
            }

            Console.WriteLine($"Largest: {max}");
        }

        private bool WallIsValid((double, double, double) wall, List<double> WALL_KEYS, Dictionary<double, List<(double, double)>> WALLS)
        {
            // Wall is valid if it does not cross an existing wall. End points of the wall may touch existing walls
            var crossValue = wall.Item1;
            var wallStart = wall.Item2;
            var wallEnd = wall.Item3;

            foreach (var WALL_KEY in WALL_KEYS)
            {
                if (WALL_KEY > wallStart && WALL_KEY < wallEnd)
                {
                    foreach (var WALL in WALLS[WALL_KEY])
                    {
                        if (crossValue > WALL.Item1 && crossValue < WALL.Item2)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool CornerIsInbounds((double, double) corner)
        {
            // Go the four cardinal directions and make sure we're surrounded by walls
            var isWallFound = false;
            for (int i = 0; i < X_WALL_KEYS.Count; i++)
            {
                var WALL_KEY = X_WALL_KEYS[i];
                if (WALL_KEY <= corner.Item1)
                {
                    if (this.CheckCornerWall(corner.Item2, X_WALLS[WALL_KEY]))
                    {
                        isWallFound = true; break;
                    }
                }
            }
            if (!isWallFound) { return false; }

            isWallFound = false;
            for (int i = X_WALL_KEYS.Count - 1; i >= 0; i--)
            {
                var WALL_KEY = X_WALL_KEYS[i];
                if (WALL_KEY >= corner.Item1)
                {
                    if (this.CheckCornerWall(corner.Item2, X_WALLS[WALL_KEY]))
                    {
                        isWallFound = true; break;
                    }
                }
            }
            if (!isWallFound) { return false; }

            isWallFound = false;
            for (int i = 0; i < Y_WALL_KEYS.Count; i++)
            {
                var WALL_KEY = Y_WALL_KEYS[i];
                if (WALL_KEY <= corner.Item2)
                {
                    if (this.CheckCornerWall(corner.Item1, Y_WALLS[WALL_KEY]))
                    {
                        isWallFound = true; break;
                    }
                }
            }
            if (!isWallFound) { return false; }

            isWallFound = false;
            for (int i = Y_WALL_KEYS.Count - 1; i >= 0; i--)
            {
                var WALL_KEY = Y_WALL_KEYS[i];
                if (WALL_KEY >= corner.Item2)
                {
                    if (this.CheckCornerWall(corner.Item1, Y_WALLS[WALL_KEY]))
                    {
                        isWallFound = true; break;
                    }
                }
            }
            if (!isWallFound) { return false; }

            return true;
        }

        private bool CheckCornerWall(double crossValue, List<(double, double)> WALLS)
        {
            foreach (var WALL in WALLS) 
            {
                if (crossValue >= WALL.Item1 && crossValue <= WALL.Item2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}