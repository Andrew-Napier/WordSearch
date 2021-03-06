﻿using System;
#nullable enable

namespace PuzzleBoard
{
    public class StartingPosition
    {
        private readonly int _row, _col, _intersects;
        private readonly WordDirections _direction;

        public WordDirections Direction
        {
            get { return _direction; }
        }

        public int Col
        {
            get { return _col; }
        }

        public int Row
        {
            get { return _row; }
        }

        public int Intersects
        {
            get { return _intersects; }
        }

        public StartingPosition(int row, int col, WordDirections directions, int intersects = 0)
        {
            _row = row;
            _col = col;
            _direction = directions;
            _intersects = intersects;
        }

        public StartingPosition(StartingPosition source, int intersects)
        {
            _row = source._row;
            _col = source._col;
            _direction = source._direction;
            _intersects = intersects;
        }
    }
}

