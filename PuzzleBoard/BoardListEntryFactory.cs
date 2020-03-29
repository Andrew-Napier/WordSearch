using System;
namespace PuzzleBoard
{
    public class BoardListEntryFactory : IBoardListEntryFactory
    {
        public BoardListEntryFactory()
        {
        }

        public IBoardListEntry Create(string word, StartingPosition position)
        {
            return new BoardListEntry(word, position);
        }
    }
}
