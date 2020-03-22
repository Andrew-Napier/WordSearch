using System;
using System.Linq;
using System.Collections.Generic;
using WordChooser;
using Microsoft.Extensions.DependencyInjection;

namespace PuzzleBoard
{
    public class PuzzleGenerator
    {
        private IBoard lettersGrid;
        private PlacementChecker placeGenerator;
        private IRelatableWordsDictionary wordGenerator;
        private IDecisionMaker decisionMaker;
        private IDirectionCounts directionCounts;
        private IRandomPicker random;
        private IWordCollection wordsToFind;
        private IWordCollection rejectedWords;

        public PuzzleGenerator(IServiceProvider provider, IRelatableWordsDictionary relatableWordsDictionary)
        {
            this.lettersGrid = provider.GetRequiredService<IBoard>();
            this.placeGenerator = provider.GetRequiredService<PlacementChecker>();
            this.wordGenerator = relatableWordsDictionary;
            this.decisionMaker = provider.GetService<IDecisionMaker>();
            this.directionCounts = provider.GetRequiredService<IDirectionCounts>();
            this.random = provider.GetRequiredService<IRandomPicker>();
            this.rejectedWords = provider.GetRequiredService<IWordCollection>();
            this.wordsToFind = provider.GetService<IWordCollection>();
        }

        public void Execute()
        {

            int blankSpaces = lettersGrid.BlanksRemaining();

            while (blankSpaces > 0)
            {
                decisionMaker.Configure(lettersGrid, wordsToFind, wordGenerator);
                if (decisionMaker.IsTimeToAttemptBlattingWord())
                {
                    lettersGrid = AttemptBlattingWord(lettersGrid, wordGenerator);
                }
                else if (decisionMaker.IsTimeToTryAddingWord())
                {
                    lettersGrid = AttemptAddingWord(lettersGrid, wordGenerator);
                }
                if (!decisionMaker.IsPuzzleStillViable(out string excuse))
                {
                    throw new PuzzleException(excuse);
                }
                blankSpaces = lettersGrid.BlanksRemaining();
            }
            lettersGrid.Display();
            wordsToFind.Display();
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
            int wordLength = random.PickWeightedWordLength();
            if (!wordGenerator.IsWordAvailable(wordLength)) return lettersGrid;

            string word = wordGenerator.PopWordOfLength(wordLength).ToUpperInvariant();
            if (wordsToFind.IsPreexisting(word)) return lettersGrid;

            var possibilities = placeGenerator.GetPossibilities(lettersGrid, word);
            StartingPosition bestPossible = null;
            foreach (var possible in possibilities)
            {
                var pc = new PlacementChooser();
                bestPossible = pc.ChooseBestPlacementOption(possible, bestPossible, directionCounts);
            }
            if (bestPossible != null)
            {
                directionCounts.IncrementCount(bestPossible.Direction);
                wordsToFind.Add(word);

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
            rejectedWords.Add(word);
        }
    }
}


