namespace PuzzleBoard
{
    public interface IPlacementChooser
    {
        public StartingPosition ChooseBestPlacementOption(
            StartingPosition opt1,
            StartingPosition opt2,
            IDirectionCounts directionCounts);
    }

    public class PlacementChooser : IPlacementChooser
    {

        public StartingPosition ChooseBestPlacementOption(
            StartingPosition opt1,
            StartingPosition opt2,
            IDirectionCounts directionCounts)
        {
            if (opt2 == null) return opt1;
            if (opt1 == null) return opt2;

            if (opt1.Intersects == opt2.Intersects)
            {
                if (directionCounts.GetCount(opt1.Direction) <= directionCounts.GetCount(opt2.Direction))
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
