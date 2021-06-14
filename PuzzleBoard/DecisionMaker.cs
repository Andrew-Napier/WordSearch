using WordChooser;
#nullable enable

namespace PuzzleBoard
{
    public class DecisionMaker : IDecisionMaker
    {
        private IRelatableWordsDictionary _dictionary;
        private IBoardList _wordCollection;
        private IBoard _board;
        private IPuzzleSize _puzzleSize;


        public DecisionMaker(IPuzzleSize puzzleSize)
        {
            _puzzleSize = puzzleSize;
        }

        public void Configure(IBoard board, IRelatableWordsDictionary dictionary)
        {
            _board = board;
            _wordCollection = board.List();
            _dictionary = dictionary;
        }

        public bool IsPuzzleStillViable(out string reasonForNonViability, out PuzzleExceptionRanking ranking)
        {
            int blanksRemaining = _board.BlanksRemaining();
            reasonForNonViability = string.Empty;
            ranking = PuzzleExceptionRanking.notApplicable;

            if (blanksRemaining == 0)
            {
                reasonForNonViability = string.Empty;
                return true;
            }

            ranking = (_dictionary.StartingWordCount() > 42)
                ? PuzzleExceptionRanking.canRetry
                : PuzzleExceptionRanking.noRetry;

            if (_dictionary.IsEmpty())
            {
                reasonForNonViability = "No more words to choose from";
                return false;
            }

            if (blanksRemaining < 4)
            {
                reasonForNonViability = "Not enough space for word";
                return false;
            }

            if (blanksRemaining < _dictionary.MinLengthOfWord())
            {
                reasonForNonViability = "No word short enough to blat";
                return false;
            }

            if (blanksRemaining > _dictionary.MaxLengthOfWord() &&
                _dictionary.MinLengthOfWord() > _puzzleSize.Max())
            {
                reasonForNonViability = "Too many blanks to blat, no words short enough to add";
                return false;
            }

            ranking = PuzzleExceptionRanking.notApplicable;
            return true;
        }

        public bool IsTimeToAttemptBlattingWord()
        {
            int blanksToFill = _board.BlanksRemaining();

            return _wordCollection.Count() > 15
                && _dictionary.IsWordAvailable(blanksToFill)
                && blanksToFill > 3;
        }

        public bool IsTimeToTryAddingWord()
        {
            int blanksToFill = _board.BlanksRemaining();

            return blanksToFill > 3
                && !_dictionary.IsEmpty();
        }
    }
}
