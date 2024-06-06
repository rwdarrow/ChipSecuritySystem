using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    public class Backtracking : ILongestValidSequence
    {
        public List<ColorChip> FindLongestValidSequence(List<ColorChip> chips, Color startColor, Color endColor)
        {
            List<ColorChip> longestValidSequence = null;

            // backtracking will allow us to consider each chip iteratively and return back if the path is invalid
            void Backtrack(List<ColorChip> currentSequence, List<ColorChip> remainingChips, Color currentColor)
            {
                // set the new longest sequence if the current color is the end color and we have an empty sequence or a new longer sequence
                if (currentColor == endColor)
                {
                    if (longestValidSequence == null || currentSequence.Count > longestValidSequence.Count)
                    {
                        longestValidSequence = new List<ColorChip>(currentSequence);
                    }
                    // in this case we need to go to the next chip (or finish)
                    return;
                }

                // Consider remaining chips that are possible next in the sequence by either having a start or end color that is the current color
                foreach (var chip in remainingChips.Where(chip => chip.StartColor == currentColor || chip.EndColor == currentColor).ToList())
                {
                    currentSequence.Add(chip);
                    remainingChips.Remove(chip);

                    var nextColor = chip.StartColor == currentColor ? chip.EndColor : chip.StartColor;

                    // next iteration will consider the current sequence up until now, remove the already seen chip, and consider the next color on the current chip
                    Backtrack(currentSequence, remainingChips, nextColor);

                    // we need to add the current chip back to remaining chips for consideration in subsequent iterations
                    // the backtracking algorithm will ensure the longest sequence that is explored is kept
                    remainingChips.Add(chip);
                    currentSequence.RemoveAt(currentSequence.Count - 1);
                }
            }

            // iterate through chips array beginning with an empty list to store the current sequence and the valid starting color
            foreach (ColorChip chip in chips.ToList())
            {
                Backtrack(new List<ColorChip>(), chips, startColor);
            }

            return longestValidSequence;
        }
    }
}
