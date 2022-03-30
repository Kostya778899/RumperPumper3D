using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace CMath
{
    public delegate T CGetter<T>();


    public interface IActivatable { public void Activate(); }
    public interface IDeActivatable { public void DeActivate(); }
    public interface IIncluded : IActivatable, IDeActivatable { private void SetActive(bool isActive) { } }
    public interface IDeActivatableByLaser : IDeActivatable { }
    public interface IUpdatable { public void Updating(); }
    public interface ISaveable<T> { public T Get(); public void Set(T value); }


    public static class CMath
    {
        public static int GetScroller(int index, int length) { return index < length ? index : index % length; }
    }
    public static class CConvert
    {
        public static Vector3 ToVector3(float value) => new Vector3(value, value, value);
    }
    public static class CArray
    {
        public static T RandomItem<T>(this T[] value) => value[UnityEngine.Random.Range(0, value.Length)];
    }
    public static class CList
    {
        //public static T Item<T>(this List<T> value, int index) => value[Place(value.Count, index)];
        public static void Move<T>(this List<T> value, int oldIndex, int newIndex)
        {
            T item = value[oldIndex];
            value.RemoveAt(oldIndex);
            value.Insert(newIndex, item);
        }
        public static List<T> Equate<T>(List<T> list)
        {
            if (list == null) return null;
            List<T> newList = new List<T>(list.Count);
            for (int i = 0; i < list.Count; i++) newList.Add(list[i]);
            return newList;
        }
    }
    public static class CAnimationCurve
    {
        public static Keyframe GetFirstKey(this AnimationCurve curve) => curve.keys[0];
        public static Keyframe GetLastKey(this AnimationCurve curve) => curve.keys[curve.length - 1];
    }
    public static class CLineRenderer
    {
        public static void SetPositionSmoothly(this LineRenderer lineRenderer, int index, Vector3 value, float duration,
            Action callback = null) =>
            DOTween.To(() => lineRenderer.GetPosition(index), x => lineRenderer.SetPosition(index, x), value, duration)
            .OnComplete(() => callback?.Invoke());
        public static void SetPositions(this LineRenderer lineRenderer, in Vector3 newPosition)
        {
            for (int i = 0; i < lineRenderer.positionCount; i++) lineRenderer.SetPosition(i, newPosition);
        }
        public static void SetPositionsSmoothly(this LineRenderer lineRenderer, float duration, Action callback = null,
            params (int index, Vector3 position)[] value)
        {
            int completeSettersCount = 0;
            foreach (var item in value) lineRenderer.SetPositionSmoothly(item.index, item.position, duration, () =>
            {
                if (++completeSettersCount >= value.Length) callback?.Invoke();
            });
        }
    }
    #region Vector
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
    public static class CVector
    {
        public static int Place(int length, int index) => index >= length ? index % length : index;
        public static float Place(float length, float index) => index >= length ? index % length : index;


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
        public static Vector3 Place(this Vector3 vector, float angle)
        {
            vector.x = Place(vector.x, angle);
            vector.y = Place(vector.y, angle);
            vector.z = Place(vector.z, angle);
            return vector;
        }

        public static Vector3 SetXYZ(CGetter<float> value) => new Vector3(value.Invoke(), value.Invoke(), value.Invoke());
    }
    #endregion
#if UNITY_EDITOR
    public class CEditor
    {
        public static void SetObjectDirty(GameObject @object)
        {
            EditorUtility.SetDirty(@object);
            EditorSceneManager.MarkSceneDirty(@object.scene);
        }
    }
#endif


    public class TagSelectorAttribute : PropertyAttribute
    {
        public bool UseDefaultTagFieldDrawer = false;
    }
}
