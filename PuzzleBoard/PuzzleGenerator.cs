using System;
using System.Linq;
using System.Collections.Generic;
using WordChooser;
using Microsoft.Extensions.DependencyInjection;

namespace PuzzleBoard
{
    public class PuzzleGenerator
    {
        private Board lettersGrid;
        private PlacementChecker placeGenerator;
        private IRelatableWordsDictionary wordGenerator;
        private IDirectionCounts directionCounts;
        private IRandomPicker random;


        public PuzzleGenerator(IServiceProvider provider, IRelatableWordsDictionary relatableWordsDictionary)
        {
            this.lettersGrid = provider.GetRequiredService<Board>();
            this.placeGenerator = provider.GetRequiredService<PlacementChecker>();
            this.wordGenerator = relatableWordsDictionary;
            this.directionCounts = provider.GetRequiredService<IDirectionCounts>();
            this.random = provider.GetRequiredService<IRandomPicker>();
        }

        public PuzzleGenerator(Board lettersGrid,
            PlacementChecker placeGenerator,
            IRelatableWordsDictionary wordGenerator,
            IDirectionCounts directionCounts,
            IRandomPicker randomPicker)
        {
            this.lettersGrid = lettersGrid;
            this.placeGenerator = placeGenerator;
            this.wordGenerator = wordGenerator;
            this.directionCounts = directionCounts;
            this.random = randomPicker;
        }

        public void Execute()
        {
            var done = false;
            int rejectedWordsCount = 0;
            string[] title = wordGenerator.GetTitle().ToArray();
            var wordsToFind = new List<string>();
            while (!done)
            {
                if (wordGenerator.IsEmpty())
                {
                    Console.WriteLine($"\nWords: {wordsToFind.Count} Blanks Remaining: {lettersGrid.BlanksRemaining} Rejected Words: {rejectedWordsCount}");
                    throw new Exception("out of words...");
                }
                int wordLength = random.PickWeightedWordLength();
                if (!wordGenerator.IsWordAvailable(wordLength)) continue;

                string word = wordGenerator.PopWordOfLength(wordLength).ToUpperInvariant();
                if (wordsToFind.Contains(word)) continue;

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
                    lettersGrid = lettersGrid.AddWord(word, bestPossible);
                    wordsToFind.Add(word);
                }
                else
                {
                    rejectedWordsCount++;
                }

                var correctLength = lettersGrid.BlanksRemaining;
                Console.Write($"\rFitted words: {wordsToFind.Count}. Failed Count: {rejectedWordsCount}. Remaining blanks: {correctLength}  ");
                if (correctLength <= wordGenerator.MaxLengthOfWord() &&
                    wordsToFind.Count > 15)
                {
                    done = wordGenerator.IsWordAvailable(correctLength);
                    if (done)
                    {
                        lettersGrid.BlatWord(wordGenerator.PopWordOfLength(correctLength));
                    }
                    else if (correctLength < 4)
                    {
                        Console.WriteLine($"\nWords: {wordsToFind.Count} Blanks Remaining: {correctLength} Rejected Words: {rejectedWordsCount}");
                        throw new Exception("failed to word...");
                    }
                }
            }
            lettersGrid.Display();

            wordsToFind.Sort();
            foreach (var w in wordsToFind)
                Console.WriteLine($"- {w}");
        }
    }
}


