using UnityEngine;

namespace Zlitz.Utilities
{
    public class WhiteNoiseSampler3D : ISampler3D
    {
        private Vector3 m_offset;

        public WhiteNoiseSampler3D()
        {
            RandomNumberGenerator rng = new RandomNumberGenerator();
            m_offset = new Vector3(rng.NextFloat(-289.0f, 289.0f), rng.NextFloat(-289.0f, 289.0f), rng.NextFloat(-289.0f, 289.0f));
        }

        public WhiteNoiseSampler3D(int seed)
        {
            RandomNumberGenerator rng = new RandomNumberGenerator(seed);
            m_offset = new Vector3(rng.NextFloat(-289.0f, 289.0f), rng.NextFloat(-289.0f, 289.0f), rng.NextFloat(-289.0f, 289.0f));
        }

        public float Sample(float x, float y, float z)
        {
            x += m_offset.x;
            y += m_offset.y;
            z += m_offset.z;
            x -= Mathf.Floor(x / 289.0f) * 289.0f;
            y -= Mathf.Floor(y / 289.0f) * 289.0f;
            z -= Mathf.Floor(z / 289.0f) * 289.0f;

            float res = Mathf.Sin(12.9898f * x + 78.233f * y + 144.7272f * z) * 43758.5453f;
            return res - Mathf.Floor(res);
        }
    }
}
