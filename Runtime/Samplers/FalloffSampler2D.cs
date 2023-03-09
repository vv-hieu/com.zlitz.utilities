using UnityEngine;

namespace Zlitz.Utilities
{
    public class FalloffSampler2D : ISampler2D
    {
        private Vector2 m_min;
        private Vector2 m_max;
        private Vector2 m_size;

        public FalloffSampler2D()
        {
            m_min = Vector2.zero;
            m_max = Vector2.one;
            m_size = m_max - m_min;
        }

        public FalloffSampler2D(Vector2 from, Vector2 to)
        {
            m_min  = new Vector2(Mathf.Min(from.x, to.x), Mathf.Min(from.y, to.y));
            m_max  = new Vector2(Mathf.Max(from.x, to.x), Mathf.Max(from.y, to.y));
            m_size = m_max - m_min;
        }

        public float Sample(float x, float y)
        {
            x = 2.0f * (x - m_min.x) / m_size.x - 1.0f;
            y = 2.0f * (y - m_min.x) / m_size.y - 1.0f;
            return Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
        }
    }
}
