using System;
using System.Linq;
#nullable enable

namespace PuzzleBoard.Domain.Models
{
    public enum WordDirections
    {
        north, northEast, east, southEast, south, southWest, West, northWest
    }

    public static class WordDirection
    {
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
}
