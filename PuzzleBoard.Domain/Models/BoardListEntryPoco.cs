#nullable enable


using System.Text.Json.Serialization;

namespace PuzzleBoard.Domain.Models;

/// <summary>
/// Describes an entry in the puzzle (with storage concerns???)
/// The entry describes the actual word, its row, column, and direction
/// the word is laid out in from its starting point.
/// </summary>
public class BoardListEntryPoco
{
    [JsonPropertyName("W")]
    public string Word { get; set; } = string.Empty;
    [JsonPropertyName("C")]
    public int Column { get; set; }
    [JsonPropertyName("R")]
    public int Row { get; set; }
    [JsonPropertyName("D")]
    public WordDirections Directions { get; set; }
}