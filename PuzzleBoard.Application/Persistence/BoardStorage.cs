using PuzzleBoard;
using PuzzleBoard.Domain.Models;


namespace PuzzleStorage;

/// <summary>
/// Contains am array of <see cref="BoardListEntryPoco"/> entries that make up the puzzle,
/// and the <see cref="Answer"/> for the puzzle that is blatted into the remaining empty places.
/// </summary>
public class BoardStorage
{
    public BoardListEntryPoco[] Entries { get; set; }
    public string Answer { get; set; } = string.Empty;
}