using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Move", fileName = "NewMoveSettings")]
public class MoveSettings : ScriptableObject
{
    public Vector3 Direction = new Vector3(0f, 1f, 0f);
    public float Distance = 10f;
    public float Speed = 5f;
    public bool IsMoving = true;
}
