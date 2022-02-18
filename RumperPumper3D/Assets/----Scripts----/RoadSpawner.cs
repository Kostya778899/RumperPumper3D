using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : Spawner<Move>
{



    public override void Spawn()
    {
        Move move;
        if (CountToSpawn < PoolingSpawnedObjects.Count)
        {
            move = Instantiate(ObjectToSpawn, transform).GetComponent<Move>();
            PoolingSpawnedObjects.Add(move);
        }
        else move = PoolingSpawnedObjects[PoolingSpawnedObjects.Count - 1];
        move.Moving();
    }
}
