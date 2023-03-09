namespace Zlitz.Utilities
{
    public class ValueReference<T>
    {
        private Getter m_get;
        private Setter m_set;

        public T value
        {
            get => m_get();
            set => m_set(value);
        }

        public ValueReference(Getter get, Setter set)
        {
            m_get = get;
            m_set = set;
        }

        public delegate T Getter();
        public delegate void Setter(T value);
    }
}
