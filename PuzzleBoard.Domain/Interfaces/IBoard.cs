using System;
using PuzzleBoard.Domain.Models;

namespace PuzzleBoard.Domain.Interfaces;

/// <summary>
/// Write-Access interface to the puzzle-board.  "puzzle-board" is
/// the NxN sized grid.
/// </summary>
public interface IBoard
{
    public IBoard AddWord(string word, StartingPosition position);
    /// <summary>
    /// Counts the number of unused squares on a puzzle-board.
    /// </summary>
    /// <returns>Number of empty squares</returns>
    public int BlanksRemaining();
    /// <summary>
    /// Takes a given word and puts each letter of the word in to a blank spot in the puzzle-board.
    /// </summary>
    /// <param name="word">The word to be "blatted"</param>
    /// <returns>The new version of the puzzle-board once the word has been "blatted".</returns>
    public IBoard BlatWord(string word);
    /// <summary>
    /// Displays the current puzzle-board to stdout.
    /// </summary>
    public void Display();
    /// <summary>
    /// Performs an action of every letter in the puzzle board in turn.
    /// </summary>
    /// <param name="action">The user-defined function to perform given the row and column</param>
    public void Enumerate(Action<int, int> action);

    /// <summary>
    /// Checks to see if a square of the puzzle board has been filled with a letter.
    /// </summary>
    /// <param name="r">The row index to check</param>
    /// <param name="c">The Column index to check</param>
    /// <returns>true if no letter has been placed at [r,c]</returns>
    public bool IsEmpty(int r, int c);
    /// <summary>
    /// Helper method to see if a position on the puzzle-board has the letter as specified.
    /// </summary>
    /// <param name="letter">The letter that should match at the location given.</param>
    /// <param name="row">The zero-based row-index to check</param>
    /// <param name="col">The zero-based column-index to check</param>
    /// <returns>true if the letter matches, otherwise false. (false for empty spots too)</returns>
    public bool IsMatching(char letter, int row, int col);
    /// <summary>
    /// Sneaky access to the underlying holder classes.
    /// TODO: Remove this if possible, it smells!
    /// </summary>
    /// <returns>The <see cref="IBoardList"/> used to generate this board.</returns>
    public IBoardList List();
}