using System;
using WordChooser;

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
        public bool IsEmpty(int r, int c);
        public bool IsMatching(char letter, int row, int col);
    }

    /// <summary>
    /// During puzzle generation, this interface helps determine what action the
    /// generator should perform next.  Call the Configure() method first, to help
    /// the decision maker, make the right decision.
    /// </summary>
    public interface IDecisionMaker
    {
        void Configure(IBoard board, IWordCollection addedWords, IRelatableWordsDictionary dictionary);
        bool IsTimeToAttemptBlattingWord();
        bool IsTimeToTryAddingWord();
        bool IsPuzzleStillViable(out string reasonForNonViability);
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
        public StartingPosition ChooseBestPlacementOption(
            StartingPosition opt1,
            StartingPosition opt2,
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

    /// <summary>
    /// A writable list of words, used to store what words have been added to the
    /// puzzle.  Contains a function that advises if the incoming word is already
    /// in the list, or is a sub-set / super-set of a word in the list. (Either
    /// case should be rejected to prevent duplicates occurring in the puzzle.)
    /// </summary>
    public interface IWordCollection
    {
        void Add(string word);
        int Count();
        void Display();
        bool IsPreexisting(string word);
    }


}
