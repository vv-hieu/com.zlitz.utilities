using UnityEngine;

namespace Zlitz.Utilities
{
    public class WhiteNoiseSampler2D : ISampler2D
    {
        private Vector2 m_offset;

        public WhiteNoiseSampler2D()
        {
            RandomNumberGenerator rng = new RandomNumberGenerator();
            m_offset = new Vector2(rng.NextFloat(-289.0f, 289.0f), rng.NextFloat(-289.0f, 289.0f));
        }

        public WhiteNoiseSampler2D(int seed)
        {
            RandomNumberGenerator rng = new RandomNumberGenerator(seed);
            m_offset = new Vector2(rng.NextFloat(-289.0f, 289.0f), rng.NextFloat(-289.0f, 289.0f));
        }

        public float Sample(float x, float y)
        {
            x += m_offset.x;
            y += m_offset.y;
            x -= Mathf.Floor(x / 289.0f) * 289.0f;
            y -= Mathf.Floor(y / 289.0f) * 289.0f;

            float res = Mathf.Sin(12.9898f * x + 78.233f * y) * 43758.5453f;
            return res - Mathf.Floor(res);
        }
    }
}
