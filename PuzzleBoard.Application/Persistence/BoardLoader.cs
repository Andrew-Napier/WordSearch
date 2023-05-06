using System.IO;
using Newtonsoft.Json;
using PuzzleBoard;
using PuzzleBoard.Domain.Interfaces;

#nullable enable

namespace PuzzleStorage;

public class BoardLoader
{
    private IBoardWrite _boardWrite;
    private IBoardListEntryFactory _factory;

    public BoardLoader(IBoardWrite startingBoardWrite,
        IBoardListEntryFactory factory)
    {
        _boardWrite = startingBoardWrite;
        _factory = factory;
    }

    public IBoardWrite Load(int counter)
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
            _boardWrite = _boardWrite.AddWord(puzzleAddition.GetWord(),
                puzzleAddition.GetPosition());
        }
        _boardWrite.BlatWord(storage.Answer);

        return _boardWrite;
    }
}