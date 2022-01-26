using System;
using System.Collections.Generic;
using PuzzleBoard.Domain.Interfaces;
using PuzzleBoard.Domain.Models;
using WordChooser;
#nullable enable

namespace PuzzleBoard
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




    /// <summary>
    /// This factory generates IBoardListEntry instances.  Avoids the possibility
    /// of them being incorrectly initialised.
    /// </summary>
    public interface IBoardListEntryFactory
    {
        IBoardListEntry Create(string word, StartingPosition position);
        IBoardListEntry Create(BoardListEntryPoco entry);
        BoardListEntryPoco Transform(IBoardListEntry entry);
    }


    /// <summary>
    /// During puzzle generation, this interface helps determine what action the
    /// generator should perform next.  Call the Configure() method first, to help
    /// the decision maker, make the right decision.
    /// </summary>
    public interface IDecisionMaker
    {
        void Configure(IBoard board, IRelatableWordsDictionary dictionary);
        bool IsTimeToAttemptBlattingWord();
        bool IsTimeToTryAddingWord();
        public void AssessCircuitBreaker();
    }

    /// <summary>
    /// Gets/Sets how many words appear in the puzzle, going in each direction.
    /// The puzzle generator uses this to try and mix up the way words are added.
    /// </summary>
    public interface IDirectionCounts
    {
        public int GetCount(WordDirections direction);
        public void IncrementCount(WordDirections direction);
    }

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

    /// <summary>
    /// Random number generator that returns how long to make the next word.
    /// It's weighted, so shorter words are preferred, as they "stack" more
    /// easily into the puzzle.
    /// </summary>
    public interface IRandomPicker
    {
        int PickWeightedWordLength();
        bool PickBoolean();
    }

}
