using System.Collections.Generic;

namespace Zlitz.Utilities
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Shuffle(list, new RandomNumberGenerator());
        }

        public static void Shuffle<T>(this IList<T> list, RandomNumberGenerator rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.NextInt(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
