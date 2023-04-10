using System;
using System.Linq;
#nullable enable

namespace PuzzleBoard.Domain.Models;

/// <summary>
/// Uses compass points to indicate the direction a word "travels" in from
/// its first letter.  (north is up)
/// </summary>
public enum WordDirections
{
    north, northEast, east, southEast, south, southWest, West, northWest
}

public static class WordDirection
{
    /// <summary>
    /// Row-index delta based on the given WordDirection.
    /// </summary>
    /// <param name="self"></param>
    /// <returns>-1 to go up, 0 to not change, +1 to go down.</returns>
    public static int RowDirection(this WordDirections self)
    {
        switch (self)
        {
            case WordDirections.north:
            case WordDirections.northEast:
            case WordDirections.northWest:
                return -1;

            case WordDirections.south:
            case WordDirections.southEast:
            case WordDirections.southWest:
                return +1;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Column index delta based on the given WordDirection.
    /// </summary>
    /// <param name="self"></param>
    /// <returns>-1 to go left, 0 to not change, +1 to go right</returns>
    public static int ColDirection(this WordDirections self)
    {
        switch (self)
        {
            case WordDirections.northWest:
            case WordDirections.West:
            case WordDirections.southWest:
                return -1;
            case WordDirections.northEast:
            case WordDirections.east:
            case WordDirections.southEast:
                return 1;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Iterate through all possible directions, executing a user-defined function in turn.
    /// </summary>
    /// <param name="func">A boolean function indicating whether the enumeration should continue (return false to terminate)</param>
    /// <param name="reverse">Iterates anti-clockwise when true</param>
    public static void Enumerate(Func<WordDirections, bool> func, bool reverse = false)
    {
        var eachDirection = (WordDirections[])Enum.GetValues(typeof(WordDirections));
        if (reverse)
        {
            foreach (WordDirections d in eachDirection.Reverse())
            {
                if (!func(d))
                    break;
            }
        }
        else
        {
            foreach (WordDirections d in eachDirection)
            {
                if (!func(d))
                    break;
            }
        }
    }
}