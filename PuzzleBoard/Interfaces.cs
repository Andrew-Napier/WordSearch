using System;
using WordChooser;

namespace PuzzleBoard
{
    public interface IBoard
    {
        public IBoard AddWord(string word, StartingPosition position);
        public int BlanksRemaining();
        public IBoard BlatWord(string word);
        public void Display();
        public bool IsEmpty(int r, int c);
        public bool IsMatching(char letter, int row, int col);
    }

    public interface IDecisionMaker
    {
        void Configure(IBoard board, IWordCollection addedWords, IRelatableWordsDictionary dictionary);
        bool IsTimeToAttemptBlattingWord();
        bool IsTimeToTryAddingWord();
        bool IsPuzzleStillViable(out string reasonForNonViability);
    }

    public interface IDirectionCounts
    {
        public int GetCount(WordDirections direction);
        public void IncrementCount(WordDirections direction);
    }

    public interface IPlacementChooser
    {
        public StartingPosition ChooseBestPlacementOption(
            StartingPosition opt1,
            StartingPosition opt2,
            IDirectionCounts directionCounts);
    }

    public interface IRandomPicker
    {
        int PickWeightedWordLength();
        bool PickBoolean();
    }

    public interface IWordCollection
    {
        void Add(string word);
        int Count();
        void Display();
        bool IsPreexisting(string word);
    }


}
