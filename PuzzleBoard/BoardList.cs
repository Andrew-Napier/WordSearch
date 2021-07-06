using System;
using System.Collections.Generic;
#nullable enable

namespace PuzzleBoard
{
    public class BoardList : IBoardList
    {
        private string _blatted = String.Empty;
        private List<IBoardListEntry> _entries = new List<IBoardListEntry>();
        private IBoardListEntryFactory _factory;

        public BoardList(IBoardListEntryFactory factory)
        {
            _factory = factory;
        }

        public void AddWord(string word, StartingPosition position)
        {
            _entries.Add(_factory.Create(word, position));
        }

        public void BlatWord(string word)
        {
            _blatted = word;
        }

        public int Count() => _entries.Count;

        public string GetBlattedWord() => _blatted;

        public IEnumerable<IBoardListEntry> GetEntries() => _entries;


        public bool IsPreexisting(string word)
        {
            foreach (var entry in _entries)
            {
                if (word.Contains(entry.GetWord()))
                {
                    return true;
                }

                if (entry.GetWord().Contains(word))
                {
                    return true;
                }
            }
            return false;
        }

        public void Sort()
        {
            _entries.Sort((a, b) => { return a.GetWord().CompareTo(b.GetWord()); });
        }
    }
}
