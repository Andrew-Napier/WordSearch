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

        public int Row { get; internal set; }
        public int Column { get; internal set; }
        public WordDirections Directions { get; internal set; }
    }
}