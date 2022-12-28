using PuzzleBoard.Domain.Models;
#nullable enable

namespace PuzzleBoard.Domain.Interfaces
{
    /// <summary>
    /// Gets/Sets how many words appear in the puzzle, going in each direction.
    /// The puzzle generator uses this to try and mix up the way words are added.
    /// </summary>
    public interface IDirectionCounts
    {
        public int GetCount(WordDirections direction);
        public void IncrementCount(WordDirections direction);
    }

}
