using System;
using WordChooser;
using Microsoft.Extensions.DependencyInjection;
#nullable enable

namespace PuzzleBoard
{
    public class PuzzleGenerator
    {
        private IBoard _lettersGrid;
        private PlacementChecker _placeGenerator;
        private IRelatableWordsDictionary _wordGenerator;
        private IDecisionMaker _decisionMaker;
        private IDirectionCounts _directionCounts;
        private IPuzzleSize _puzzleSize;
        private IRandomPicker _random;
        private IBoardList _wordsToFind;
        private IBoardList _rejectedWords;

        public PuzzleGenerator(IServiceProvider provider, IRelatableWordsDictionary relatableWordsDictionary)
        {
            this._lettersGrid = provider.GetRequiredService<IBoard>();
            this._placeGenerator = provider.GetRequiredService<PlacementChecker>();
            this._puzzleSize = provider.GetRequiredService<IPuzzleSize>();
            this._wordGenerator = relatableWordsDictionary;
            this._decisionMaker = provider.GetService<IDecisionMaker>();
            this._directionCounts = provider.GetRequiredService<IDirectionCounts>();
            this._random = provider.GetRequiredService<IRandomPicker>();
            this._rejectedWords = provider.GetRequiredService<IBoardList>();
            this._wordsToFind = provider.GetService<IBoardList>();
        }

        public IBoard Execute()
        {

            int blankSpaces = _lettersGrid.BlanksRemaining();

            while (blankSpaces > 0)
            {
                _decisionMaker.Configure(_lettersGrid, _wordGenerator);
                if (_decisionMaker.IsTimeToAttemptBlattingWord())
                {
                    _lettersGrid = AttemptBlattingWord(_lettersGrid, _wordGenerator);
                }
                else if (_decisionMaker.IsTimeToTryAddingWord())
                {
                    _lettersGrid = AttemptAddingWord(_lettersGrid, _wordGenerator);
                }
                if (!_decisionMaker.IsPuzzleStillViable(out string excuse, out PuzzleExceptionRanking rank))
                {
                    throw new PuzzleException(excuse, rank);
                }
                blankSpaces = _lettersGrid.BlanksRemaining();
                Console.Write($"Added: {_wordsToFind.Count()}, Rejected: {_rejectedWords.Count()}, Blanks: {blankSpaces}   \r");
            }

            return _lettersGrid;
        }

        private IBoard AttemptBlattingWord(IBoard lettersGrid, IRelatableWordsDictionary wordGenerator)
        {
            int length = lettersGrid.BlanksRemaining();
            if (wordGenerator.IsWordAvailable(length))
            {
                return lettersGrid.BlatWord(wordGenerator.PopWordOfLength(length));
            }

            return lettersGrid;
        }

        private IBoard AttemptAddingWord(IBoard lettersGrid, IRelatableWordsDictionary wordGenerator)
        {
            int wordLength = _random.PickWeightedWordLength();
            if (!GetApproximateWordLength(ref wordLength, wordGenerator)) return lettersGrid;

            string word = wordGenerator.PopWordOfLength(wordLength).ToUpperInvariant();
            if (_wordsToFind.IsPreexisting(word)) return lettersGrid;

            var possibilities = _placeGenerator.GetPossibilities(lettersGrid, word);
            StartingPosition bestPossible = null;
            foreach (var possible in possibilities)
            {
                var pc = new PlacementChooser();
                bestPossible = pc.ChooseBestPlacementOption(possible, bestPossible, _directionCounts);
            }
            if (bestPossible != null)
            {
                _directionCounts.IncrementCount(bestPossible.Direction);
                _wordsToFind.AddWord(word, bestPossible);

                return lettersGrid.AddWord(word, bestPossible);
            }
            else
            {
                AddToRejectedWordList(word);
                return lettersGrid;
            }
        }

        private void AddToRejectedWordList(string word)
        {
            _rejectedWords.AddWord(word, null);
        }


        private bool GetApproximateWordLength(ref int wordLength, IRelatableWordsDictionary wordGenerator)
        {
            int startPoint = wordLength;
            int variation = 0;
            int max = _puzzleSize.Max();

            while ((startPoint-variation) > 3 && wordLength < max)
            {
                wordLength = startPoint - variation;
                if (wordGenerator.IsWordAvailable(wordLength)) return true;
                wordLength = startPoint + variation;
                if (wordGenerator.IsWordAvailable(wordLength)) return true;
                variation++;
            }
            return false;
        }

    }
}


