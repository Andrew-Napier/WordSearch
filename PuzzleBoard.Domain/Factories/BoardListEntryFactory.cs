using System;
using PuzzleBoard.Domain.Interfaces;
using PuzzleBoard.Domain.Models;
#nullable enable

namespace PuzzleBoard.Domain.Factories
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

        public IBoardListEntry Create(BoardListEntry entry)
        {
            var position = new StartingPosition(
                entry.Row,
                entry.Column,
                entry.Directions);
           
            return new BoardListEntry(entry.Word, position);
        }

        public BoardListEntry Transform(IBoardListEntry entry)
        {
            return new BoardListEntry
            {
                Word = entry.GetWord(),
                Column = entry.GetPosition().Col,
                Row = entry.GetPosition().Row,
                Directions = entry.GetPosition().Direction
            };
        }
    }
}
