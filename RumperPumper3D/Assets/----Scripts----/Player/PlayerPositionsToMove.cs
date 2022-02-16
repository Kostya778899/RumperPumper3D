using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionsToMove : MonoBehaviour
{
    [SerializeField] private Transform[] _positions;


    public Transform[] GetPositions() => _positions;
}
