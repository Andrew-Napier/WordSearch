using PuzzleBoard;
using PuzzleBoard.Application.Models;


namespace PuzzleStorage;

public class BoardStorage
{
    public BoardListEntryPoco[] Entries { get; set; }
    public string Answer { get; set; } = string.Empty;
}