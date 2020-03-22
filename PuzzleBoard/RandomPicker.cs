using System;
namespace PuzzleBoard
{
    public class RandomPicker : IRandomPicker
    {
        private Random _rnd;
        private int[] _weights = {0,0,0,5,5,5,4,3,2,1,1};
        private int _sumOfWeights = 0;


        public RandomPicker()
        {
            int seed = DateTime.UtcNow.GetHashCode();
            _rnd = new Random(seed);
            for (int i = 0; i < _weights.Length; i++)
                _sumOfWeights += _weights[i];
        }

        public bool PickBoolean()
        {
            return (_rnd.Next() % 2 == 0);
        }

        public int PickWeightedWordLength()
        {
            int picker = _rnd.Next(_sumOfWeights)+1;
            for(int i = 0; i < _weights.Length; i++)
            {
                picker -= _weights[i];
                if (picker <= 0)
                {
                    return i + 1;
                }
            }
            return -1;
        }
    }
}
