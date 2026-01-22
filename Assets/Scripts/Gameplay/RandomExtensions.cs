using UnityEngine;

namespace Gameplay
{
    public static class RandomExtensions
    {
        private static readonly int _rangeArrayLength = 2;

        public static float RangeArray(float[] range)
        {
            if (range.Length < _rangeArrayLength)
            {
                throw new System.Exception($"Length of range array must be >= {_rangeArrayLength}");
            }

            return Random.Range(range[0], range[1]);
        }
    }
}