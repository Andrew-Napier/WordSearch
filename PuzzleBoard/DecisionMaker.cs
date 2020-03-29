using System;
using WordChooser;

namespace PuzzleBoard
{
    public class DecisionMaker : IDecisionMaker
    {
        private IRelatableWordsDictionary _dictionary;
        private IBoardList _wordCollection;
        private IBoard _board;

        public DecisionMaker()
        {
        }

        public void Configure(IBoard board, IRelatableWordsDictionary dictionary)
        {
            _board = board;
            _wordCollection = board.List();
            _dictionary = dictionary;
        }

        public bool IsPuzzleStillViable(out string reasonForNonViability)
        {
            int blanksRemaining = _board.BlanksRemaining();
            if (blanksRemaining == 0)
            {
                reasonForNonViability = string.Empty;
                return true;
            }

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

            /* One more test:
             * Some condition about blanks remaining and word lengths still to choose from... */
            if (blanksRemaining < _dictionary.MinLengthOfWord())
            {
                reasonForNonViability = "No word short enough to blat";
                return false;
            }

            reasonForNonViability = string.Empty;
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
