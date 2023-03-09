using UnityEditor;

namespace Zlitz.Utilities
{
    public abstract class ScriptlessEditor : Editor
    {
        private static readonly string[] s_excluded = new string[] { "m_Script" };

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, s_excluded);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
