using System;
using System.Collections.Generic;
using PuzzleBoard.Domain.Interfaces;
using PuzzleBoard.Domain.Models;
#nullable enable

namespace PuzzleBoard;

public class PlacementChecker
{
    private int _maxSize;
    private IRandomPicker _random;

    public PlacementChecker(IRandomPicker random, IPuzzleSize size)
    {
        _maxSize = size.Max();
        _random = random;
    }

    public IEnumerable<StartingPosition> GetPossibilities(IBoardWrite lettersGrid, string word)
    {
        var positions = new List<StartingPosition>();
        lettersGrid.Enumerate((r, c) =>
        {
            foreach (WordDirections d in (WordDirections[])Enum.GetValues(typeof(WordDirections)))
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
        });

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