#nullable enable

using PuzzleBoard.Domain.Interfaces;
using WeCantSpell.Hunspell;
using WordChooser;

namespace PuzzleBoard.Infrastructure.Services;

public class WordFilter : IWordFilter
{
    private  WordList _dictionary;

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