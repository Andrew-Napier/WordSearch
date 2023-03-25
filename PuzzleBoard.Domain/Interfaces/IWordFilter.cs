using System.Collections.Generic;
#nullable enable

namespace PuzzleBoard.Domain.Interfaces;

/// <summary>
/// Removes misspelt words and various special cases. (i.e. swear words etc)
/// </summary>
public interface IWordFilter
{
    IEnumerable<string> Filter(IEnumerable<string> unfilteredList);
}