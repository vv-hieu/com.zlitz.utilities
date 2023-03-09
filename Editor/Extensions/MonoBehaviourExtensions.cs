using UnityEngine;
using UnityEditor;

namespace Zlitz.Utilities
{
    public static class MonoBehaviourExtensions
    {
        public static string GetScriptPath(this MonoBehaviour monoBehaviour)
        {
            return AssetDatabase.GetAssetPath(MonoScript.FromMonoBehaviour(monoBehaviour));
        }
    }
}
