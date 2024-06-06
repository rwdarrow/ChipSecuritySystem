using System.Collections.Generic;

namespace ChipSecuritySystem
{
    public class DFS : ILongestValidSequence
    {
        public List<ColorChip> FindLongestValidSequence(List<ColorChip> chips, Color startColor, Color endColor)
        {
            var graph = BuildGraph(chips);
            var visited = new HashSet<ColorChip>();
            var longestPath = new List<ColorChip>();
            var currentPath = new List<ColorChip>();

            void DFS(Color currentColor)
            {
                if (currentColor == endColor)
                {
                    if (currentPath.Count > longestPath.Count)
                    {
                        longestPath = new List<ColorChip>(currentPath);
                    }
                    return;
                }

                if (!graph.ContainsKey(currentColor))
                    return;

                foreach (var chip in graph[currentColor])
                {
                    if (!visited.Contains(chip))
                    {
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

        private static Dictionary<Color, List<ColorChip>> BuildGraph(List<ColorChip> chips)
        {
            var graph = new Dictionary<Color, List<ColorChip>>();
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
