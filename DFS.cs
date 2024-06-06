using System.Collections.Generic;

namespace ChipSecuritySystem
{
    /// <summary>
    /// The DFS method uses similar logic to the backtracking method, but is more time efficient by making use
    /// of a separately-constructed graph data structure to represent the relationships between the chips.
    /// This data structure allows us to check whether a possible path is valid. Although it is more time efficient, 
    /// it is less space efficient since it requires the separate data structure, and is a bit more complex to implement.
    /// </summary>
    public class DFS : ILongestValidSequence
    {
        public List<ColorChip> FindLongestValidSequence(List<ColorChip> chips, Color startColor, Color endColor)
        {
            // unlike backtracking, we will use a separate data structure to improve time efficiency at the cost of space
            var graph = BuildGraph(chips);
            var visited = new HashSet<ColorChip>();
            var longestPath = new List<ColorChip>();
            var currentPath = new List<ColorChip>();

            // like backtracking, we will explore the paths recursively, but the graph we built eliminates the need to track the current state
            void DFS(Color currentColor)
            {
                // logic is similar to backtracking, we end when we encounter an end color
                if (currentColor == endColor)
                {
                    if (currentPath.Count > longestPath.Count)
                    {
                        longestPath = new List<ColorChip>(currentPath);
                    }
                    return;
                }

                // the first main logical difference from backtracking is that our graph will tell us whether or not we need to bother continuing down this path.
                // If there are no more chips with the current color, terminate the current path exploration
                if (!graph.ContainsKey(currentColor))
                    return;

                // again similar logic to backtracking, with the difference being iterating over the graph nodes rather than chips in the current list state
                foreach (var chip in graph[currentColor])
                {
                    // second main logical difference is checking our visited chips list. There is no need to bother checking if we've already visited this chip
                    if (!visited.Contains(chip))
                    {
                        // similar logic to backtracking, just replacing the state manipulation of the list with the visited list
                        visited.Add(chip);
                        currentPath.Add(chip);

                        var nextColor = chip.StartColor == currentColor ? chip.EndColor : chip.StartColor;

                        DFS(nextColor);

                        currentPath.RemoveAt(currentPath.Count - 1);
                        visited.Remove(chip);
                    }
                }
            }

            DFS(startColor);
            return longestPath;
        }

        // The graph will represent the edge/vertex relationship between the color chips
        private static Dictionary<Color, List<ColorChip>> BuildGraph(List<ColorChip> chips)
        {
            // A dictionary has O(1) lookup and allows us to define associate each color with all chips starting or ending with that color
            var graph = new Dictionary<Color, List<ColorChip>>();

            // Setting the color as the key lets us lookup the validity of a path quickly in our DFS algorithm
            // Since the value is the list of "possible" chips, we can get an instant determination of validity without 
            // having to brute force our way through each permutation, as with backtracking
            foreach (var chip in chips)
            {
                if (!graph.ContainsKey(chip.StartColor))
                    graph[chip.StartColor] = new List<ColorChip>();
                if (!graph.ContainsKey(chip.EndColor))
                    graph[chip.EndColor] = new List<ColorChip>();
                graph[chip.StartColor].Add(chip);
                graph[chip.EndColor].Add(chip);
            }

            return graph;
        }
    }
}
