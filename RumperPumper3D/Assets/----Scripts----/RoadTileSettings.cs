using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/RoadTile", fileName = "NewRoadTileSettings")]
public class RoadTileSettings : ScriptableObject
{
    public LetDefault[] LetsPrefabs;
    public Vector3[] LetsToSpawnPositions;
    [Min(0f)] public int LetsToSpawnMinCount = 1, LetsToSpawnMaxCount = 3;
}
