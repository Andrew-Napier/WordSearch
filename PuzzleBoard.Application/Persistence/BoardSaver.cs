using PuzzleBoard.Domain.Interfaces;
using System.Text.Json;

#nullable enable

namespace PuzzleStorage;

public class BoardSaver
{
    private readonly BoardStorage _store;

    public BoardSaver(IBoardWrite puzzle, IBoardListEntryFactory factory)
    {
        _store = new BoardStorage();
        _store.Answer = puzzle.List().GetBlattedWord();
        _store.Entries =
            (from entry in puzzle.List().GetEntries()
             select factory.Transform(entry)).ToArray();
    }

    public void Save(int counter)
    {
        using (StreamWriter file = File.CreateText($"puzzle{counter,0:D6}.json"))
        {
            var jsonObject = JsonSerializer.Serialize(_store);
            file.Write(jsonObject);
        }
    }
}