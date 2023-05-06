using PuzzleBoard.Domain.Interfaces;
#nullable enable

namespace PuzzleBoard;

public class DecisionMaker : IDecisionMaker
{
    private IRelatableWordsDictionary? _dictionary;
    private IBoardList? _wordCollection;
    private IBoardWrite? _board;
    private IPuzzleSize _puzzleSize;


    public DecisionMaker(IPuzzleSize puzzleSize)
    {
        _puzzleSize = puzzleSize;
    }

    public void Configure(IBoardWrite boardWrite, IRelatableWordsDictionary dictionary)
    {
        _board = boardWrite;
        _wordCollection = boardWrite.List();
        _dictionary = dictionary;
    }

    public void AssessCircuitBreaker()
    {

        if (_board == null || _dictionary == null)
        {
            throw new PuzzleException(
                "BoardWrite or dictionary is missing.",
                PuzzleExceptionRanking.noRetry);
        }

        int blanksRemaining = _board.BlanksRemaining();

        if (blanksRemaining == 0)
        {
            return;
        }

        PuzzleExceptionRanking ranking =
            (_dictionary.StartingWordCount() > 42)
                ? PuzzleExceptionRanking.canRetry
                : PuzzleExceptionRanking.noRetry;

        if (_dictionary.IsEmpty())
        {
            throw new PuzzleException(
                "No more words to choose from",
                ranking);
        }

        if (blanksRemaining < 4)
        {
            throw new PuzzleException(
                "Not enough space for word",
                ranking);
        }

        if (blanksRemaining < _dictionary.MinLengthOfWord())
        {
            throw new PuzzleException(
                "No word short enough to blat",
                ranking);
        }

        if (blanksRemaining > _dictionary.MaxLengthOfWord() &&
            _dictionary.MinLengthOfWord() > _puzzleSize.Max())
        {
            throw new PuzzleException(
                "Too many blanks to blat, no words short enough to add",
                ranking);
        }
    }

    public bool IsTimeToAttemptBlattingWord()
    {
        if (_board == null || _wordCollection == null || _dictionary == null)
        {
            return false;
        }
        int blanksToFill = _board.BlanksRemaining();

        return _wordCollection.Count() > 15
               && _dictionary.IsWordAvailable(blanksToFill)
               && blanksToFill > 3;
    }

    public bool IsTimeToTryAddingWord()
    {
        if (_board == null || _dictionary == null)
        {
            return false;
        }

        int blanksToFill = _board.BlanksRemaining();

        return blanksToFill > 3
               && !_dictionary.IsEmpty();
    }
}