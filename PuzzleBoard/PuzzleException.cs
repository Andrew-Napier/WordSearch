using System;
namespace PuzzleBoard
{
    public enum PuzzleExceptionRanking
    {
        notApplicable,
        canRetry,
        noRetry
    }

    public class PuzzleException : Exception
    {
        public PuzzleExceptionRanking Ranking { get; set; }
        public PuzzleException()
        { }

        public PuzzleException(string message, PuzzleExceptionRanking rank) : base(message)
        {
            Ranking = rank;
        }
    }
}
