using System;
using PuzzleBoard.Domain.Interfaces;
using PuzzleBoard.Domain.Models;
#nullable enable

namespace PuzzleBoard.Domain.Factories;

public class BoardListEntryFactory : IBoardListEntryFactory
{
    public IBoardListEntry Create(string word, StartingPosition position)
    {
        return new BoardListEntry(word, position);
    }

    public IBoardListEntry Create(BoardListEntry entry)
    {
        // TODO: This smells.  Is this just a copier for IBoardListEntry?
        var position = new StartingPosition(
            entry.GetPosition());
           
        return new BoardListEntry(entry.GetWord(), position);
    }

    public BoardListEntry Transform(IBoardListEntry entry)
    {
        // TODO: Not sure IBoardListEntry <==> BoardListEntry is still necessary
        return new BoardListEntry(entry.GetWord(), entry.GetPosition());
    }
}