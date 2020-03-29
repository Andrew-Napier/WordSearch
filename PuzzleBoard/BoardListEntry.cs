using System;
namespace PuzzleBoard
{
    public class BoardListEntry : IBoardListEntry
    {
        private StartingPosition _startingPosition;
        private string _word;

        public BoardListEntry(string word, StartingPosition position)
        {
            _word = word;
            _startingPosition = position;
        }

        public StartingPosition GetPosition() => _startingPosition;

        public string GetWord() => _word;
    }
}
