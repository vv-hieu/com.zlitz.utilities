using System;
using UnityEngine;

namespace Zlitz.Utilities
{
    [Serializable]
    public struct Optional<T>
    {
        [SerializeField] private bool m_enabled;
        [SerializeField] private T m_value;

        public bool enabled => m_enabled;
        public T value => m_value;

        public static Optional<T> Empty()
        {
            Optional<T> res = new Optional<T>();

            res.m_enabled = false;
            res.m_value = default(T);

            return res;
        }

        public static Optional<T> Of(T value)
        {
            Optional<T> res = new Optional<T>();

            res.m_enabled = true;
            res.m_value = value;

            return res;
        }
    }
}
