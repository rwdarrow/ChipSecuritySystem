using System;
using System.Collections.Generic;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Color start = Color.Blue;
            Color end = Color.Green;

            List<ColorChip> chips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple),
            };

            var longestValidSequenceBacktracking = new Backtracking().FindLongestValidSequence(chips, start, end);
            var longestValidSequenceDFS = new DFS().FindLongestValidSequence(chips, start, end);

            if (longestValidSequence != null)
            {
                Console.WriteLine(start + " " + String.Join(" ", longestValidSequence) + " " + end);
            } else
            {
                Console.WriteLine(Constants.ErrorMessage);
            }

            Console.ReadLine();
        }
    }
}
