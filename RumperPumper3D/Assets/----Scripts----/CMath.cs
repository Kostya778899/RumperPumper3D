using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class CMath
{
    public static int Place(int length, int index) => index >= length ? index % length : index;

    #region Vector3
    [System.Serializable]
    public class Vector3Bool
    {
        public bool x, y, z;
        public static readonly Vector3Bool True = new Vector3Bool(true, true, true);
        public static readonly Vector3Bool False = new Vector3Bool(false, false, false);

        public Vector3Bool(bool x, bool y, bool z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(Vector3Bool))]
        public class NullableVarDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, label, property);

                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

                int indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;

                const float boolSize = 20f;
                DrawBool(new Vector2(position.x + 0f, position.y), boolSize, "x", "X");
                DrawBool(new Vector2(position.x + 30f, position.y), boolSize, "y", "Y");
                DrawBool(new Vector2(position.x + 60f, position.y), boolSize, "z", "Z");

                void DrawBool(in Vector2 position, float size, in string relativePropertyPath, in string label = null)
                {
                    const float labelXDistanceByBool = 17f;

                    EditorGUI.PropertyField(new Rect(position.x, position.y, size, size),
                        property.FindPropertyRelative(relativePropertyPath), GUIContent.none);
                    EditorGUI.LabelField(new Rect(position.x + labelXDistanceByBool, position.y, size, size),
                        label ?? relativePropertyPath);
                }

                EditorGUI.indentLevel = indent;
                EditorGUI.EndProperty();
            }
        }
#endif
    }


    public static bool IsMore(this Vector3 a, float b)
    {
        if (a.x > b || a.y > b || a.z > b) return true;
        return false;
    }
    public static bool IsMore(this Vector3 a, Vector3 b)
    {
        if (a.x > b.x || a.y > b.y || a.z > b.z) return true;
        return false;
    }
    #endregion
    #region List
    public static T GetElement<T>(this List<T> value, int index) => value[Place(value.Count, index)];
    public static void Move<T>(this List<T> value, int oldIndex, int newIndex)
    {
        T item = value[oldIndex];
        value.RemoveAt(oldIndex);
        value.Insert(newIndex, item);
    }
    #endregion
    #region IEquatable
    public static T GetRandomElement<T>(this T[] value) => value[UnityEngine.Random.Range(0, value.Length)];
    #endregion
}
