using System;
using System.Collections.Generic;

namespace PuzzleBoard
{
    public class PlacementChooser
    {
        private Dictionary<WordDirections, int> _directionScores;

        public PlacementChooser(Dictionary<WordDirections, int> counts)
        {
            _directionScores = counts;
        }

        public StartingPosition ChooseBestPlacementOption(StartingPosition opt1, StartingPosition opt2)
        {
            if (opt2 == null) return opt1;
            if (opt1 == null) return opt2;

            if (opt1.Intersects == opt2.Intersects)
            {
                if (_directionScores[opt1.Direction] <= _directionScores[opt2.Direction])
                    return opt1;
                else
                    return opt2;
            }
            else
            {
                if (opt1.Intersects <= opt2.Intersects)
                    return opt1;
                else
                    return opt2;
            }
        }

      }
}
