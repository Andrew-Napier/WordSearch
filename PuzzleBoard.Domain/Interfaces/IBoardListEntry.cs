using System;
using PuzzleBoard.Domain.Models;

namespace PuzzleBoard.Domain.Interfaces;

/// <summary>
/// Describes a word in the puzzle.  (By starting position and the word itself)<br/>
/// Cannot be instantiated via dependency injection, instead, use an instance of <see cref="IBoardListEntryFactory"/>
/// to create them.
/// </summary>
public interface IBoardListEntry
{
    string GetWord();
    StartingPosition GetPosition();
}