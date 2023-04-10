using System.IO;
using Newtonsoft.Json;
using PuzzleBoard;
using PuzzleBoard.Domain.Interfaces;

#nullable enable

namespace PuzzleStorage;

public class BoardLoader
{
    private IBoard _board;
    private IBoardListEntryFactory _factory;

    public BoardLoader(IBoard startingBoard,
        IBoardListEntryFactory factory)
    {
        _board = startingBoard;
        _factory = factory;
    }

    public IBoard Load(int counter)
    {
        BoardStorage storage;

        using (StreamReader file = File.OpenText($"puzzle{counter,0:D6}.json"))
        {
            var value = file.ReadLine();

            storage = !string.IsNullOrEmpty(value)
                ? JsonConvert.DeserializeObject<BoardStorage>(value)
                : new BoardStorage();
        }
        foreach(var entry in storage.Entries)
        {
            var puzzleAddition = _factory.Create(entry);
            _board = _board.AddWord(puzzleAddition.GetWord(),
                puzzleAddition.GetPosition());
        }
        _board.BlatWord(storage.Answer);

        return _board;
    }
}