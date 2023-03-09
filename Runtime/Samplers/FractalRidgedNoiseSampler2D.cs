using UnityEngine;

namespace Zlitz.Utilities
{
    public class FractalRidgedNoiseSampler2D : ISampler2D
    {
        private FractalPerlinNoiseSampler2D m_perlin;

        private float m_sharpness;

        public float scale       => m_perlin.scale;
        public int   octaves     => m_perlin.octaves;
        public float persistence => m_perlin.persistence;
        public float lacunarity  => m_perlin.lacunarity;
        public float sharpness   => m_sharpness;

        public FractalRidgedNoiseSampler2D(float scale, int octaves, float persistence, float lacunarity, float sharpness)
        {
            m_perlin = new FractalPerlinNoiseSampler2D(scale, octaves, persistence, lacunarity);

            m_sharpness = sharpness;
        }

        public FractalRidgedNoiseSampler2D(int seed, float scale, int octaves, float persistence, float lacunarity, float sharpness)
        {
            m_perlin = new FractalPerlinNoiseSampler2D(seed, scale, octaves, persistence, lacunarity);

            m_sharpness = sharpness;
        }
        
        public float Sample(float x, float y)
        {
            return Mathf.Pow(1.0f - Mathf.Abs(m_perlin.Sample(x, y) * 2.0f - 1.0f), m_sharpness);
        }
    }
}
