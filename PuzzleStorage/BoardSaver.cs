using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PuzzleBoard;

namespace PuzzleStorage
{
    public class BoardStorageClass
    {
        public BoardListEntryPoco[] Entries { get; set; }
        public string Answer { get; set; }
    }

    public class BoardSaver
    {
        private BoardStorageClass store;

        public BoardSaver(IBoard puzzle, IBoardListEntryFactory factory)
        {
            store = new BoardStorageClass();
            store.Answer = puzzle.List().GetBlattedWord();
            store.Entries =
                (from entry in puzzle.List().GetEntries()
                select factory.Transform(entry)).ToArray();
        }

        public void Save(int counter)
        {
            using (StreamWriter file = File.CreateText($"puzzle{counter,0:D6}.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                var s = JsonConvert.SerializeObject(store);
                serializer.Serialize(file, store);
            }
        }
    }
}