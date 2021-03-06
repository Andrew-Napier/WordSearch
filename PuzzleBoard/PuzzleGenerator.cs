﻿using System;
using WordChooser;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
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
        private List<string> _rejectedWords = new List<string>();

        public PuzzleGenerator(
            IBoard lettersGrid,
            PlacementChecker placeGenerator,
            IPuzzleSize puzzleSize,
            IRelatableWordsDictionary relatableWordsDictionary,
            IDecisionMaker decisionMaker,
            IDirectionCounts directionCounts,
            IRandomPicker random,
            IBoardList wordsToFind)
        {
            this._lettersGrid = lettersGrid;
            this._placeGenerator = placeGenerator;
            this._puzzleSize = puzzleSize;
            this._wordGenerator = relatableWordsDictionary;
            this._decisionMaker = decisionMaker;
            this._directionCounts = directionCounts;
            this._random = random;
            this._wordsToFind = wordsToFind;
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

                _decisionMaker.AssessCircuitBreaker();

                blankSpaces = _lettersGrid.BlanksRemaining();
                Console.Write($"Added: {_wordsToFind.Count()}, Rejected: {_rejectedWords.Count}, Blanks: {blankSpaces}   \r");
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
            StartingPosition? bestPossible = null;
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
            _rejectedWords.Add(word);
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


