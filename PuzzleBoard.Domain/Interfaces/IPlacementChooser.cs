using PuzzleBoard.Domain.Models;
#nullable enable

namespace PuzzleBoard.Domain.Interfaces;

/// <summary>
/// This decides between two different placements of the same word.
/// </summary>
public interface IPlacementChooser
{
    public StartingPosition? ChooseBestPlacementOption(
        StartingPosition? opt1,
        StartingPosition? opt2,
        IDirectionCounts directionCounts);
}