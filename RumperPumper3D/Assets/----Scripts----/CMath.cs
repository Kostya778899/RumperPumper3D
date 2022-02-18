using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CMath
{
    public static bool IsMore(this Vector3 a, float b)
    {
        if (a.x > b || a.y > b || a.z > b) return true;
        return false;
    }
}
