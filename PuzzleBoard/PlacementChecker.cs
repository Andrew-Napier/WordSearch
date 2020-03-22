using System;
using System.Collections.Generic;

namespace PuzzleBoard
{
    public class PlacementChecker
    {
        private int _maxSize;
        private IRandomPicker _random;
        private const int boardSize = 11;

        public PlacementChecker(IRandomPicker random)
        {
            _maxSize = boardSize;
            _random = random;
        }

        public IEnumerable<StartingPosition> GetPossibilities(IBoard lettersGrid, string word)
        {
            var positions = new List<StartingPosition>();
            for (int r = 0; r < _maxSize; r++)
            {
                for (int c = 0; c < _maxSize; c++)
                {
                    foreach (WordDirections d in Enum.GetValues(typeof(WordDirections)))
                    {
                        if (isWordFitting(word.Length, r, c, d))
                        {
                            int intersects = 0;
                            bool spotForWord = true;
                            for (int i = 0; i < word.Length; i++)
                            {
                                var l = word[i];
                                var row = r + (d.RowDirection() * i);
                                var col = c + (d.ColDirection() * i);

                                if (lettersGrid.IsMatching(l, row, col))
                                {
                                    intersects++;
                                }
                                else if (!lettersGrid.IsEmpty(row, col))
                                {
                                    spotForWord = false;
                                }
                            }
                            if (spotForWord)
                            {
                                if (_random.PickBoolean())
                                    positions.Insert(0, new StartingPosition(r, c, d, intersects));
                                else
                                    positions.Add(new StartingPosition(r, c, d, intersects));
                            }
                        }
                    }
                }
            }

            return positions;
        }


        private bool isWordFitting(int wordLength, int r, int c, WordDirections d)
        {
            return
                r + (d.RowDirection() * wordLength) >= 0
                && r + (d.RowDirection() * wordLength) <= _maxSize
                && c + (d.ColDirection() * wordLength) >= 0
                && c + (d.ColDirection() * wordLength) <= _maxSize;
        }
    }
}
