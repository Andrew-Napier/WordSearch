﻿using System;
using System.Collections.Generic;

namespace WordChooser
{
    public interface IRelatableWordsDictionary
    {
        IEnumerable<string> GetTitle();
        bool IsEmpty();
        bool IsWordAvailable(int length);
        int MaxLengthOfWord();
        string PopWordOfLength(int length);
        void PrepareDictionary();
    }

    public class RelatableWordsDictionary : IRelatableWordsDictionary
    {
        private IWordSource _wordSource;
        private int _maxLength = 0;
        private Dictionary<int, Stack<string>> _sourceLists
            = new Dictionary<int, Stack<string>>();

        public RelatableWordsDictionary(IWordSource wordSource)
        {
            _wordSource = wordSource;
        }

        public void PrepareDictionary()
        {
            foreach(var word in _wordSource.GetListOfWords())
            {
                var length = word.Length;
                _maxLength = Math.Max(length, _maxLength);
                if (!_sourceLists.ContainsKey(length))
                {
                    _sourceLists.Add(length, new Stack<string>());
                }
                _sourceLists[length].Push(word);
            }
        }

        public string PopWordOfLength(int length)
        {
            if (!_sourceLists.ContainsKey(length))
            {
                return string.Empty;
            }

            if (!_sourceLists[length].TryPop(out string value))
            {
                _sourceLists.Remove(length);
                return string.Empty;
            }

            return value;
        }

        public IEnumerable<string> GetTitle()
        {
            return _wordSource.GetTitle();
        }

        public bool IsWordAvailable(int length)
        {
            return _sourceLists.ContainsKey(length)
                && _sourceLists[length].TryPeek(out string value);
        }

        public int MaxLengthOfWord()
        {
            return _maxLength;
        }

        public bool IsEmpty()
        {
            foreach(var entry in _sourceLists.Values)
            {
                if (entry.Count > 0)
                    return false;
            }

            return true;
        }
    }
}
