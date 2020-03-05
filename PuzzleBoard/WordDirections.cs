using System;
namespace PuzzleBoard
{
    public enum WordDirections
    {
        north, northEast, east, southEast, south, southWest, West, northWest

    }

    public static class WordDirectionMethods
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
    }
}
