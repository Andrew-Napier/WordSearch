using System.Collections.Generic;
#nullable enable

namespace PuzzleBoard.Domain.Interfaces;

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