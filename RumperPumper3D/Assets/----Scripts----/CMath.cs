using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CMath
{
    public static int Place(int length, int index) => index >= length ? index % length : index;

    #region Vector3
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
