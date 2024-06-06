using System.Collections.Generic;

namespace ChipSecuritySystem
{
    public interface ILongestValidSequence
    {
        List<ColorChip> FindLongestValidSequence(List<ColorChip> chips, Color startColor, Color endColor);
    }
}
