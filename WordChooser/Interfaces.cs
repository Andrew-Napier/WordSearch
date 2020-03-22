using System;
using System.Collections.Generic;

namespace WordChooser
{
    public interface IConfig
    {
        string SourceUrl();
        string XnonTextClass();
        string XtextPath();
        string XtitlePath();
        char[] WhiteSpace();
        string[] ExclusionList();
    }

    public interface IRelatableWordsDictionary
    {
        IEnumerable<string> GetTitle();
        bool IsEmpty();
        bool IsWordAvailable(int length);
        int MinLengthOfWord();
        int MaxLengthOfWord();
        string PopWordOfLength(int length);
        void PrepareDictionary();
    }

    public interface IWordFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> unfilteredList);
    }

    public interface IWordSource
    {
        IEnumerable<string> GetListOfWords();
        IEnumerable<string> GetTitle();
    }
}
