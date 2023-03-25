using System;
using PuzzleBoard.Domain.Models;

namespace PuzzleBoard.Domain.Interfaces;

/// <summary>
/// This factory generates IBoardListEntry instances.  Avoids the possibility
/// of them being incorrectly initialised.
/// </summary>
public interface IBoardListEntryFactory
{
    IBoardListEntry Create(string word, StartingPosition position);
    IBoardListEntry Create(BoardListEntry entry);
    BoardListEntry Transform(IBoardListEntry entry);
}