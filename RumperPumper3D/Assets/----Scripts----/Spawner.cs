using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected T ObjectToSpawn;
    [SerializeField] protected int CountToSpawn = 10;
    protected List<T> PoolingSpawnedObjects;


    public abstract void Spawn();
}
