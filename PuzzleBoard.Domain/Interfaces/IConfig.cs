#nullable enable

using PuzzleBoard.Domain.Interfaces;

namespace PuzzleBoard.Domain.Interfaces
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
}
