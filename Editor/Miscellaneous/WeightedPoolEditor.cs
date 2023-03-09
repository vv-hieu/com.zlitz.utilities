using UnityEngine;
using UnityEditor;

namespace Zlitz.Utilities
{
    [CustomPropertyDrawer(typeof(WeightedPool<>))]
    public class WeightedPoolPropertyDrawer : PropertyDrawer
    {
        private bool m_foldoutState = true;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty entriesProperty = property.FindPropertyRelative("m_entries");
            if (entriesProperty.arraySize > 0 && m_foldoutState)
            {
                return 2.0f * EditorGUIUtility.singleLineHeight + (EditorGUI.GetPropertyHeight(entriesProperty.GetArrayElementAtIndex(0)) + EditorGUIUtility.singleLineHeight * 0.25f) * entriesProperty.arraySize;
            }

            return EditorGUIUtility.singleLineHeight * (m_foldoutState ? 2.25f : 1.0f);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty entriesProperty = property.FindPropertyRelative("m_entries");
            
            property.serializedObject.Update();

            position.height = EditorGUIUtility.singleLineHeight;
            m_foldoutState = EditorGUI.BeginFoldoutHeaderGroup(position, m_foldoutState, label);

            if (m_foldoutState)
            {
                entriesProperty.serializedObject.Update();
                EditorGUI.indentLevel++;

                for (int i = 0; i < entriesProperty.arraySize; i++)
                {
                    SerializedProperty entryProperty = entriesProperty.GetArrayElementAtIndex(i);
                    position.y += position.height;
                    position.height = EditorGUI.GetPropertyHeight(entryProperty);
                    EditorGUI.PropertyField(position, entryProperty, new GUIContent("Entry " + i));

                    Rect rectRemoveBottom = position;
                    rectRemoveBottom.x += rectRemoveBottom.width - 80.0f;
                    rectRemoveBottom.width = 80.0f;
                    rectRemoveBottom.height = EditorGUIUtility.singleLineHeight;
                    if (GUI.Button(rectRemoveBottom, "Remove"))
                    {
                        var methodInfo = property.GetValue<object>().GetType().GetMethod("RemoveEntry");
                        methodInfo.Invoke(property.GetValue<object>(), new object[] { i });
                    }

                    position.y += 0.25f * EditorGUIUtility.singleLineHeight;
                }

                EditorGUI.indentLevel++;

                position.y += position.height;
                position.height = EditorGUIUtility.singleLineHeight;
                if (GUI.Button(position, "Add"))
                {
                    var methodInfo = property.GetValue<object>().GetType().GetMethod("AddEntry");
                    methodInfo.Invoke(property.GetValue<object>(), null);
                }

                entriesProperty.serializedObject.ApplyModifiedProperties();
            }

            property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndFoldoutHeaderGroup();
        }
    }

    [CustomPropertyDrawer(typeof(WeightedPool<>.Entry))]
    public class WeightedPoolEntryPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return ((label != null && label.text != "") ? 1.25f * EditorGUIUtility.singleLineHeight : 0.0f) +
                EditorGUI.GetPropertyHeight(property.FindPropertyRelative("m_value"), new GUIContent("Value")) +
                EditorGUI.GetPropertyHeight(property.FindPropertyRelative("m_weight"), new GUIContent("Weight"));
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty  = property.FindPropertyRelative("m_value");
            SerializedProperty weightProperty = property.FindPropertyRelative("m_weight");

            if (label != null && label.text != "")
            {
                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.LabelField(position, label);
                position.y += 1.25f * EditorGUIUtility.singleLineHeight;
            }

            EditorGUI.indentLevel++;

            position.height = base.GetPropertyHeight(valueProperty, new GUIContent("Value"));
            EditorGUI.PropertyField(position, valueProperty, new GUIContent("Value"));
            position.y += position.height;

            position.height = base.GetPropertyHeight(weightProperty, new GUIContent("Weight"));
            EditorGUI.PropertyField(position, weightProperty, new GUIContent("Weight"));

            EditorGUI.indentLevel--;
        }
    }
}
