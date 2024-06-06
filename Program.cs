using System;
using System.Collections.Generic;

namespace ChipSecuritySystem
{
    class Program
    {
        readonly static Color start = Color.Blue;
        readonly static Color end = Color.Green;

        static void Main(string[] args)
        {
            List<ColorChip> chips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple),
            };

            var watch = System.Diagnostics.Stopwatch.StartNew();
            var longestValidSequenceBacktracking = new Backtracking().FindLongestValidSequence(chips, start, end);
            watch.Stop();
            var backtrackingElapsedTime = watch.ElapsedMilliseconds;
            Console.WriteLine("Backtracking Method:");
            PrintResults(longestValidSequenceBacktracking, backtrackingElapsedTime);

            watch.Restart();
            var longestValidSequenceDFS = new DFS().FindLongestValidSequence(chips, start, end);
            watch.Stop();
            var dfsElapsedTime = watch.ElapsedMilliseconds;
            Console.WriteLine("DFS Method:");
            PrintResults(longestValidSequenceDFS, dfsElapsedTime);

            Console.ReadLine();
        }

        private static void PrintResults(List<ColorChip> result, long elapsedTime)
        {
            if (result != null)
            {
                Console.WriteLine(start + " " + String.Join(" ", result) + " " + end);
                Console.WriteLine("Elapsed time (ms): " + elapsedTime + "\n");
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage);
            }
        }
    }
}
