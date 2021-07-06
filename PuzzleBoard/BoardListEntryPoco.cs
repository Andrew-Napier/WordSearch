using Newtonsoft.Json;
#nullable enable


namespace PuzzleBoard
{
    public class BoardListEntryPoco
    {
        [JsonProperty("W")]
        public string Word { get; set; } = string.Empty;
        [JsonProperty("C")]
        public int Column { get; set; }
        [JsonProperty("R")]
        public int Row { get; set; }
        [JsonProperty("D")]
        public WordDirections Directions { get; set; }
    }
}