using System;
using System.Collections.Generic;

namespace WordChooser
{
    /// <summary>
    /// Readonly access to the configuration settings used.
    /// </summary>
    public interface IConfig
    {
        string SourceUrl();
        string XnonTextClass();
        string XtextPath();
        string XtitlePath();
        char[] WhiteSpace();
        string[] ExclusionList();
    }

    /// <summary>
    /// A word list accessor interface with handy methods for
    /// retrieving words of required lengths.  Depletes its listing as
    /// words are retrieved.
    /// </summary>
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

    /// <summary>
    /// Removes misspelt words and various special cases. (i.e. swear words etc)
    /// </summary>
    public interface IWordFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> unfilteredList);
    }

    /// <summary>
    /// Gets words from an external source that have been verified via the
    /// IWordFilter interface.  Can also retrieve a title that relates to the
    /// source of the words. (which may or may not reflect the subject matter
    /// of the final puzzle.)
    /// </summary>
    public interface IWordSource
    {
        IEnumerable<string> GetListOfWords();
        IEnumerable<string> GetTitle();
    }
}
