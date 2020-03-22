using System;
namespace PuzzleBoard
{
    public class PuzzleException : Exception
    {
        public PuzzleException()
        { }

        public PuzzleException(string message) : base(message)
        { }
    }
}
