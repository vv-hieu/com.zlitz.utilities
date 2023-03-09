using UnityEngine;
using UnityEditor;

namespace Zlitz.Utilities
{
    public static class ScriptableObjectExtensions
    {
        public static string GetScriptPath(this ScriptableObject scriptableObject)
        {
            return AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(scriptableObject));
        }
    }
}