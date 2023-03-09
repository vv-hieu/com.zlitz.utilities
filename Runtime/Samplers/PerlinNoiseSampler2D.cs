using UnityEngine;

namespace Zlitz.Utilities
{
    public class PerlinNoiseSampler2D : ISampler2D
    {
        private float   m_scale;
        private Vector2 m_offset;

        public float scale => m_scale;

        public PerlinNoiseSampler2D(float scale)
        {
            m_scale = scale;
            RandomNumberGenerator rng = new RandomNumberGenerator();
            m_offset = new Vector2(rng.NextFloat(-289.0f, 289.0f), rng.NextFloat(-289.0f, 289.0f));
        }

        public PerlinNoiseSampler2D(int seed, float scale)
        {
            m_scale = scale;
            RandomNumberGenerator rng = new RandomNumberGenerator(seed);
            m_offset = new Vector2(rng.NextFloat(-289.0f, 289.0f), rng.NextFloat(-289.0f, 289.0f));
        }

        public float Sample(float x, float y)
        {
            x = x * m_scale + m_offset.x;
            y = y * m_scale + m_offset.y;
            return Mathf.Clamp01(Mathf.PerlinNoise(x, y));
        }
    }
}
