using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class RandomObjectsSpawner<T> where T : UnityEngine.Object
{
    public UnityEvent<int> OnSpawn;

    [SerializeField] private ObjectSettings _objects;

    [Serializable]
    private struct ObjectSettings
    {
        public T ObjectPrefab;
        [Range(0f, 100f)] public float SpawnProbability;
        public int Flow;
        public UnityEvent OnSpawn;
    }


    public void Spawn()
    {

    }
}
