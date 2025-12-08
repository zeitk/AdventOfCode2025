using AdventOfCode2025.Model;

namespace AdventOfCode2025.Days
{
    public class Day8 : Day
    {
        public Day8()
            : base(8)
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
            var totalComparisons = 1000;
            var nodes = new List<(int, int, int)>();          // Format: x-value, y-value, z-value
            var distances = new List<(double, int, int)>();   // Format: distance, source node, target node
            var groups = new Dictionary<int, HashSet<int>>(); // Format: key-node, value-group
            var existingSets = new HashSet<HashSet<int>>();   // Format: Set of the sets in the groups dict

            // 1. Prepare nodes
            foreach (var line in this.Input)
            {
                var positions = line.Split(',');
                var x = int.Parse(positions[0]);
                var y = int.Parse(positions[1]);
                var z = int.Parse(positions[2]);
                nodes.Add((x, y, z));
            }

            // 2. Do an n^2 algorithm to compare each node to every other. Keep track of distances
            for (var i = 0; i < nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    var distance = this.ComputeDistance(nodes[i], nodes[j]);
                    distances.Add((distance, i, j));
                }
            }

            // 3. Initialize the groups dictionary
            for (int i = 0; i < nodes.Count; i++)
            {
                var set = new HashSet<int>() { i };
                groups.Add(i, set);
                existingSets.Add(set);
            }

            // 4. Sort by distances and arrange in groups
            distances.Sort((a, b) => a.Item1 - b.Item1 > 0 ? 1 : -1);
            for (int i = 0; i < Math.Min(totalComparisons, distances.Count); i++)
            {
                var node1 = distances[i].Item2;
                var node2 = distances[i].Item3;
                var set1 = groups[node1];
                var set2 = groups[node2];

                if (set1 != set2)
                {
                    set1.UnionWith(set2);
                    groups[node2] = set1;
                    existingSets.Remove(set2);

                    foreach (var node in set1)
                    {
                        if (node != node1 && node != node2)
                        {
                            groups[node] = set1;
                        }
                    }
                }
            }

            // 5. Get sizes of existing sets
            var sizes = new List<double>();
            foreach (var set in existingSets)
            {
                sizes.Add(set.Count);
            }

            // 6. Sort desc and sum the size of the largest sets
            sizes.Sort((a, b) => a - b > 0 ? -1 : 1);
            Console.WriteLine($"Final: {sizes[0] * sizes[1] * sizes[2]}");
        }

        private void Part2()
        {
            var nodes = new List<(int, int, int)>();          // Format: x-value, y-value, z-value
            var distances = new List<(double, int, int)>();   // Format: distance, source node, target node
            var groups = new Dictionary<int, HashSet<int>>(); // Format: key-node, value-group

            // 1. Prepare nodes
            foreach (var line in this.Input)
            {
                var positions = line.Split(',');
                var x = int.Parse(positions[0]);
                var y = int.Parse(positions[1]);
                var z = int.Parse(positions[2]);
                nodes.Add((x, y, z));
            }

            // 2. Do an n^2 algorithm to compare each node to every other. Keep track of distances
            for (var i = 0; i < nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    var distance = this.ComputeDistance(nodes[i], nodes[j]);
                    distances.Add((distance, i, j));
                }
            }

            // 3. Initialize the groups dictionary
            for (int i = 0; i < nodes.Count; i++)
            {
                var set = new HashSet<int>() { i };
                groups.Add(i, set);
            }

            // 4. Sort by distances and arrange in groups
            distances.Sort((a, b) => a.Item1 - b.Item1 > 0 ? 1 : -1);
            for (int i = 0; i < distances.Count; i++)
            {
                var node1 = distances[i].Item2;
                var node2 = distances[i].Item3;
                var set1 = groups[node1];
                var set2 = groups[node2];

                set1.UnionWith(set2);
                groups[node2] = set1;

                if (set1.Count == nodes.Count)
                {
                    Console.Write($"Final coordinates: ");
                    Console.Write($"({nodes[node1].Item1},{nodes[node1].Item2},{nodes[node1].Item3})-");
                    Console.WriteLine($"({nodes[node2].Item1},{nodes[node2].Item2},{nodes[node2].Item3})");

                    double answer = (double)nodes[node1].Item1 * (double)nodes[node2].Item1;
                    Console.WriteLine($"Answer: {answer}");
                    return;
                }

                foreach (var node in set1)
                {
                    if (node != node1 && node != node2)
                    {
                        groups[node] = set1;
                    }
                }
            }
        }

        private double ComputeDistance((int, int, int) node1, (int, int, int) node2)
        {
            var diff1 = node1.Item1 - node2.Item1;
            var diff2 = node1.Item2 - node2.Item2;
            var diff3 = node1.Item3 - node2.Item3;
            var distance = Math.Sqrt(Math.Pow(diff1, 2) + Math.Pow(diff2, 2) + Math.Pow(diff3, 2));
            return distance;
        }
    }
}
