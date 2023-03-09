using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zlitz.Utilities
{
    public interface IWeightedPool<T>
    {
        T Get(RandomNumberGenerator rng);

        T Get(RandomNumberGenerator rng, Predicate<T> condition);
    }

    [Serializable]
    public class WeightedPool<T> : IWeightedPool<T>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private Entry[] m_entries;

        public Entry[] entries => m_entries;

        private float m_totalWeight;

        public T Get(RandomNumberGenerator rng)
        {
            if (m_entries != null)
            {
                float random  = rng.NextFloat(0.0f, m_totalWeight);
                float current = 0.0f;

                foreach (Entry entry in m_entries)
                {
                    current += entry.weight;
                    if (current >= random)
                    {
                        return entry.value;
                    }
                }
            }

            return default(T);
        }

        public T Get(RandomNumberGenerator rng, Predicate<T> condition)
        {
            if (m_entries != null)
            {
                float total = 0.0f;
                Entry[] entries = new Entry[m_entries.Length];
                int i = 0;

                foreach (Entry entry in m_entries)
                {
                    if (condition(entry.value))
                    {
                        entries[i++] = entry;
                        total += entry.weight;
                    }
                }

                float random  = rng.NextFloat(0.0f, total);
                float current = 0.0f;

                for (int j = 0; j < i; j++)
                {
                    Entry entry = entries[j];
                    current += entry.weight;
                    if (current >= random)
                    {
                        return entry.value;
                    }
                }
            }

            return default(T);
        }

        public void AddEntry()
        {
            List<Entry> entries = new List<Entry>();
            if (m_entries != null)
            {
                entries.AddRange(m_entries);
            }
            entries.Add(new Entry());
            m_entries = entries.ToArray();
        }

        public void RemoveEntry(int index)
        {
            List<Entry> entries = new List<Entry>();
            if (m_entries != null)
            {
                entries.AddRange(m_entries);
            }
            if (index >= 0 && index < entries.Count)
            {
                entries.RemoveAt(index);
            }
            m_entries = entries.ToArray();
        }

        private void p_Validate()
        {
            m_totalWeight = 0.0f;
            if (m_entries != null)
            {
                foreach (Entry entry in m_entries)
                {
                    m_totalWeight += entry.weight;
                }
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() => p_Validate();

        void ISerializationCallbackReceiver.OnBeforeSerialize() => p_Validate();

        [Serializable]
        public struct Entry : ISerializationCallbackReceiver
        {
            [SerializeField]
            private T m_value;

            [SerializeField]
            private float m_weight;

            public T value => m_value;
            public float weight => m_weight;

            public Entry(T value, float weight)
            {
                m_value  = value;
                m_weight = weight;
            }

            public void p_Validate()
            {
                m_weight = Mathf.Max(0.0f, m_weight);
            }

            void ISerializationCallbackReceiver.OnAfterDeserialize() => p_Validate();

            void ISerializationCallbackReceiver.OnBeforeSerialize() => p_Validate();
        }
    }
}