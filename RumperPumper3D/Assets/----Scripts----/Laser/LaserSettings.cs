using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Laser", fileName = "New" + nameof(LaserSettings))]
public class LaserSettings : ScriptableObject
{
    public LineRenderer GraphicsRayPrefab;
    [Min(0f)] public float
        RayDistance = 100f,
        RayAppearanceDuration = 0.1f,
        RotateDuration = 0.2f,
        RechargeTime = 5f;
}
