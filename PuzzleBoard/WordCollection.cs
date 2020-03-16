using System;
using System.Collections.Generic;

namespace PuzzleBoard
{
    public interface IWordCollection
    {
        void Add(string word);
        int Count();
        void Display();
        bool IsPreexisting(string word);
    }

    public class WordCollection : IWordCollection
    {
        private List<string> _list = new List<string>();

        public WordCollection()
        {
        }

        public void Add(string word)
            => _list.Add(word);
        

        public int Count()
            => _list.Count;

        public void Display()
        {
            _list.Sort();
            foreach (var w in _list)
                Console.WriteLine($"- {w}");
        }

        public bool IsPreexisting(string word)
        {
            if (_list.Contains(word))
            {
                return true;
            }
            foreach (var existingWord in _list)
            {
                if (word.Contains(existingWord))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
