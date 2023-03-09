using System;

namespace Zlitz.Utilities
{
    public class RandomNumberGenerator
    {
        public int seed { get; private set; }
    
        private Random m_random;
    
        private static Random s_random = new Random();
    
        public int NextInt(int minInclusive, int maxExclusive)
        {
            return m_random.Next(minInclusive, maxExclusive);
        }
    
        public float NextFloat(float minInclusive, float maxInclusive)
        {
            return Math.Abs((float)m_random.NextDouble() - 0.5f) * 2.0f * (maxInclusive - minInclusive) + minInclusive;
        }

        public RandomNumberGenerator()
        {
            seed = p_RandomSeed();

            m_random = new Random(seed);
        }

        public RandomNumberGenerator(int seed)
        {
            this.seed = seed;
    
            m_random = new Random(seed);
        }
    
        private static int p_RandomSeed()
        {
            int i1 = s_random.Next(256);
            int i2 = s_random.Next(256);
            int i3 = s_random.Next(256);
            int i4 = s_random.Next(256);
    
            return i1 << 24 | i2 << 16 | i3 << 8 | i4;
        }
    }
}