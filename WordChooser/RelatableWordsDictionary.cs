using System;
using System.Collections.Generic;

namespace WordChooser
{

    public class RelatableWordsDictionary : IRelatableWordsDictionary
    {
        private IWordSource _wordSource;
        private int _maxLength = 0;
        private Dictionary<int, Stack<string>> _sourceLists
            = new Dictionary<int, Stack<string>>();
        private int _minLength = int.MaxValue;

        public RelatableWordsDictionary(IWordSource wordSource)
        {
            _wordSource = wordSource;
        }

        public void PrepareDictionary()
        {
            int count = 0;
            foreach(var word in _wordSource.GetListOfWords())
            {
                var length = word.Length;
                _maxLength = Math.Max(length, _maxLength);
                _minLength = Math.Min(length, _minLength);
                if (!_sourceLists.ContainsKey(length))
                {
                    _sourceLists.Add(length, new Stack<string>());
                }
                _sourceLists[length].Push(word);
                count += 1;
            }
            Console.WriteLine($"Words in dictionary: {count}");
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

            VerifyMinMax(length);

            return value;
        }

        private void VerifyMinMax(int length)
        {
            if (length == _minLength)
            {
                _minLength = adjustAsNecessary(length, +1);
            }

            if (length == _maxLength)
            {
                _maxLength = adjustAsNecessary(length, -1);
            }
        }

        private int adjustAsNecessary(int length, int velocity)
        {
            int proposed = length;
            while (proposed >= _minLength && proposed <= _maxLength)
            {
                if (_sourceLists.ContainsKey(proposed))
                {
                    if (_sourceLists[proposed].Count > 0)
                    {
                        return proposed;
                    }
                }
                proposed += velocity;
            }
            return proposed;
        }

        public IEnumerable<string> GetTitle() =>
            _wordSource.GetTitle();

        public bool IsWordAvailable(int length)
        {
            return _sourceLists.ContainsKey(length)
                && _sourceLists[length].TryPeek(out string value);
        }

        public int MaxLengthOfWord() => _maxLength;
        public int MinLengthOfWord() => _minLength;

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
