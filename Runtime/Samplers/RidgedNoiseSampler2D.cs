using UnityEngine;

namespace Zlitz.Utilities
{
    public class RidgedNoiseSampler2D : ISampler2D
    {
        private PerlinNoiseSampler2D m_perlin;

        private float m_sharpness;

        public float scale => m_perlin.scale;
        public float sharpness => m_sharpness;

        public RidgedNoiseSampler2D(float scale, float sharpness)
        {
            m_perlin = new PerlinNoiseSampler2D(scale);

            m_sharpness = sharpness;
        }

        public RidgedNoiseSampler2D(int seed, float scale, float sharpness)
        {
            m_perlin = new PerlinNoiseSampler2D(seed, scale);

            m_sharpness = sharpness;
        }

        public float Sample(float x, float y)
        {
            return Mathf.Pow(1.0f - Mathf.Abs(m_perlin.Sample(x, y) * 2.0f - 1.0f), m_sharpness);
        }
    }
}
