using System.Collections.Generic;
#nullable enable

namespace PuzzleBoard.Domain.Interfaces;

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
    int StartingWordCount();
}