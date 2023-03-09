using UnityEngine;

namespace Zlitz.Utilities
{
    public static class PersistentGameObject
    {
        public static GameObject NewObject(string name)
        {
            GameObject go = new GameObject(name);
            Object.DontDestroyOnLoad(go);
            return go;
        }
    }
}
