using PuzzleBoard.Domain.Interfaces;

namespace PuzzleBoard.Domain.Models
{
    public class BoardListEntry : IBoardListEntry
    {
        private string word;
        private StartingPosition position;

        public BoardListEntry(string word, StartingPosition position)
        {
            this.word = word;
            this.position = position;
        }

        public WordDirections Directions { get; internal set; }

        public StartingPosition GetPosition() => this.position;

        public string GetWord() => this.word;
    }
}