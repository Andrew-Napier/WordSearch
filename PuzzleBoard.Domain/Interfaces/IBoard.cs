using System;
using PuzzleBoard.Domain.Models;

namespace PuzzleBoard.Domain.Interfaces
{
    /// <summary>
    /// Write-Access interface to the puzzle-board.  "puzzle-board" is
    /// the NxN sized grid.
    /// </summary>
    public interface IBoard
    {
        public IBoard AddWord(string word, StartingPosition position);
        public int BlanksRemaining();
        public IBoard BlatWord(string word);
        public void Display();
        public void Enumerate(Action<int, int> action);
        public bool IsEmpty(int r, int c);
        public bool IsMatching(char letter, int row, int col);
        public IBoardList List();
    }


}

