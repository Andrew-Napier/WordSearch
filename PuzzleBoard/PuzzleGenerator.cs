using System;
using System.Linq;
using System.Collections.Generic;
using WordChooser;

namespace PuzzleBoard
{
    public class PuzzleGenerator
    {
        private Board lettersGrid;
        private PlacementChecker placeGenerator;
        private IRelatableWordsDictionary wordGenerator;
        private Dictionary<WordDirections, int> directionCounts;
        private IRandomPicker random;

        public PuzzleGenerator(Board lettersGrid,
            PlacementChecker placeGenerator,
            IRelatableWordsDictionary wordGenerator,
            Dictionary<WordDirections, int> directionCounts,
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
            var letters = new List<char>();
            foreach(var w in title)
            {
                foreach (var l in w)
                    letters.Add(l);
            }
            var wordsToFind = new List<string>();
            while (!done)
            {
                Console.Write($"\rFitted words: {wordsToFind.Count}. Failed Count: {rejectedWordsCount}");
                int wordLength = random.PickWeightedWordLength();
                if (!wordGenerator.IsWordAvailable(wordLength)) continue;

                string word = wordGenerator.PopWordOfLength(wordLength).ToUpperInvariant();
                if (wordsToFind.Contains(word)) continue;

                var possibilities = placeGenerator.GetPossibilities(lettersGrid, word);
                StartingPosition bestPossible = null;
                foreach (var possible in possibilities)
                {
                    var pc = new PlacementChooser(directionCounts);
                    bestPossible = pc.ChooseBestPlacementOption(possible, bestPossible);                    
                }
                if (bestPossible != null)
                {
                    directionCounts[bestPossible.Direction] += 1;
                    lettersGrid = lettersGrid.AddWord(word, bestPossible);
                    wordsToFind.Add(word);
                }
                else
                {
                    rejectedWordsCount++;
                }

                var correctLength = lettersGrid.BlanksRemaining;
                if (correctLength == letters.Count)
                {
                    foreach (var w in title)
                    {
                        lettersGrid = lettersGrid.BlatWord(w);
                    }
                    done = true;
                }
                else if ((correctLength < 9 && correctLength > 4) || wordsToFind.Count > 17)
                {
                    done = wordGenerator.IsWordAvailable(correctLength);
                    if (done)
                    {
                        lettersGrid.BlatWord(wordGenerator.PopWordOfLength(correctLength));
                    }
                }
                if (!done)
                {
                    bool canStillWork = false;
                    for(int i = 5; i < 9; i++)
                    {
                        canStillWork |= (wordGenerator.IsWordAvailable(i));
                    }
                    if (!canStillWork)
                    {
                        lettersGrid.Display();
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


