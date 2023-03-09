using UnityEngine;

namespace Zlitz.Utilities
{
    public class FractalPerlinNoiseSampler2D : ISampler2D
    {
        private PerlinNoiseSampler2D m_perlin;

        private int       m_octaves;
        private float     m_persistence;
        private float     m_lacunarity;
        private float     m_amplitude;
        private Vector2[] m_offsets;

        public float scale       => m_perlin.scale;
        public int   octaves     => m_octaves;
        public float persistence => m_persistence;
        public float lacunarity  => m_lacunarity;

        public FractalPerlinNoiseSampler2D(float scale, int octaves, float persistence, float lacunarity)
        {
            m_perlin = new PerlinNoiseSampler2D(scale);

            m_octaves     = octaves;
            m_persistence = persistence;
            m_lacunarity  = lacunarity;

            RandomNumberGenerator rng = new RandomNumberGenerator();
            m_offsets = new Vector2[octaves];
            m_amplitude = 0.0f;
            float currentAmplitude = 1.0f;
            for (int i = 0; i < octaves; i++)
            {
                m_offsets[i] = new Vector2(rng.NextFloat(-289.0f, 289.0f), rng.NextFloat(-289.0f, 289.0f));
                m_amplitude += currentAmplitude;
                currentAmplitude *= persistence;
            }
        }

        public FractalPerlinNoiseSampler2D(int seed, float scale, int octaves, float persistence, float lacunarity)
        {
            m_perlin = new PerlinNoiseSampler2D(seed, scale);

            m_octaves     = octaves;
            m_persistence = persistence;
            m_lacunarity  = lacunarity;

            RandomNumberGenerator rng = new RandomNumberGenerator(seed);
            m_offsets = new Vector2[octaves];
            m_amplitude = 0.0f;
            float currentAmplitude = 1.0f;
            for (int i = 0; i < octaves; i++)
            {
                m_offsets[i] = new Vector2(rng.NextFloat(-289.0f, 289.0f), rng.NextFloat(-289.0f, 289.0f));
                m_amplitude += currentAmplitude;
                currentAmplitude *= persistence;
            }
        }
   
        public float Sample(float x, float y)
        {
            float res = 0.0f;

            float amplitude = 1.0f;
            float frequency = 1.0f;
            for (int i = 0; i < m_octaves; i++)
            {
                res += amplitude * (m_perlin.Sample(x * frequency + m_offsets[i].x, y * frequency + m_offsets[i].y) * 2.0f - 1.0f);

                amplitude *= m_persistence;
                frequency *= lacunarity;
            }

            return 0.5f * (res / m_amplitude + 1.0f);
        }
    }
}
