using System;
#nullable enable

namespace PuzzleBoard
{
    public class PuzzleSize : IPuzzleSize
    {
        readonly int _value;

        public PuzzleSize(int value)
        {
            _value = value;
        }

        public int Max() => _value;
    }
}
