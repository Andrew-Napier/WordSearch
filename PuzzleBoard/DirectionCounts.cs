using System;
using System.Collections.Generic;
using PuzzleBoard.Domain.Interfaces;
using PuzzleBoard.Domain.Models;
#nullable enable

namespace PuzzleBoard;

public class DirectionCounts : IDirectionCounts
{
    private Dictionary<WordDirections, int> directionCounts = new Dictionary<WordDirections, int>();

    public DirectionCounts()
    {
        foreach (WordDirections d in (WordDirections[])Enum.GetValues(typeof(WordDirections)))
        {
            directionCounts[d] = 0;
        }

    }

    public int GetCount(WordDirections direction)
    {
        return directionCounts[direction];
    }

    public void IncrementCount(WordDirections direction)
    {
        directionCounts[direction]++;
    }
}