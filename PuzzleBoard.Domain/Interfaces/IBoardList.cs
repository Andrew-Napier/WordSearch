using System;
using PuzzleBoard.Domain.Models;

namespace PuzzleBoard.Domain.Interfaces
{
    /// <summary>
    /// A writable list of words, used to store what words have been added to the
    /// puzzle.  Contains a function that advises if the incoming word is already
    /// in the list, or is a sub-set / super-set of a word in the list. (Either
    /// case should be rejected to prevent duplicates occurring in the puzzle.)
    /// When adding words, you need to specify where the word starts and which
    /// direction it has been added in.
    /// </summary>
    public interface IBoardList
    {
        void AddWord(string word, StartingPosition position);
        void BlatWord(string word);
        int Count();
        string GetBlattedWord();
        IEnumerable<IBoardListEntry> GetEntries();
        bool IsPreexisting(string word);
        void Sort();
    }
}

