﻿
using System;
using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell;

namespace WordChooser
{
    public interface IWordFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> unfilteredList);
    }

    public class WordFilter : IWordFilter
    {
        private  WordList _dictionary = null;

        public WordFilter()
        {
            _dictionary = WordList.CreateFromFiles("SpellCheck/en_GB.dic");
        }

        public IEnumerable<string> Filter(IEnumerable<string> unfilteredList)
        {
            var filteredList = from word in unfilteredList
                               where word.IsContainingOnlyLetters()
                               && _dictionary.Check(word)
                               && word.Length > 3
                               && word.Length < 16
                               select word;
            return filteredList;
        }
    }
}
