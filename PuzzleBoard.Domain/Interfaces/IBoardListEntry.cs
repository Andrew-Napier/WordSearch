using System;
using PuzzleBoard.Domain.Models;

namespace PuzzleBoard.Domain.Interfaces
{
    /// <summary>
    /// Describes a word in the puzzle.  (By starting position and the word itself)<br/><para></para>
    /// Cannot be instantiated via dependency injection, instead, use an instance of
    /// <para><code>IBoardListEntryFactory</code></para> to create them.
    /// </summary>
    public interface IBoardListEntry
    {
        string GetWord();
        StartingPosition GetPosition();
    }
}

