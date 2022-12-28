using System;
using System.Collections.Generic;
#nullable enable

namespace PuzzleBoard.Domain.Interfaces
{

    /// <summary>
    /// Random number generator that returns how long to make the next word.
    /// It's weighted, so shorter words are preferred, as they "stack" more
    /// easily into the puzzle.
    /// </summary>
    public interface IRandomPicker
    {
        int PickWeightedWordLength();
        bool PickBoolean();
    }

}
