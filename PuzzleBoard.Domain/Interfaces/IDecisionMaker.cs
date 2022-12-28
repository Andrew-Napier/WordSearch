#nullable enable

namespace PuzzleBoard.Domain.Interfaces
{
    /// <summary>
    /// During puzzle generation, this interface helps determine what action the
    /// generator should perform next.  Call the Configure() method first, to help
    /// the decision maker, make the right decision.
    /// </summary>
    public interface IDecisionMaker
    {
        void Configure(IBoard board, IRelatableWordsDictionary dictionary);
        bool IsTimeToAttemptBlattingWord();
        bool IsTimeToTryAddingWord();
        public void AssessCircuitBreaker();
    }

}
