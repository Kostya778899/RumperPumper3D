using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CMath;

[Serializable]
public class RandomObjectsSpawner<T> : IUpdatable where T : UnityEngine.Object
{
    public UnityEvent<int> OnSpawn;

    [SerializeField] private Flow[] _flows;
    [SerializeField] private ISpawnPosition[] _spawnPositions;


    private interface ISpawnPosition { public Vector3 Get(); }

    [Serializable] private class Flow : IUpdatable
    {
        [SerializeField] private ObjectSettings[] _objectsSettings = null;
        [SerializeField] private int[] _enabledPositionsIndexes = null;
        private float _generalSpawnProbability = 0f;


        public void Updating()
        {
            _generalSpawnProbability = 0f;
            foreach (var item in _objectsSettings) _generalSpawnProbability += item.SpawnProbability;
        }

        public ObjectSettings GetRandomObjectSettings()
        {
            float randomProbabilitiesSum = UnityEngine.Random.Range(0f, _generalSpawnProbability);
            float probabilitiesSum = 0f;
            foreach (var item in _objectsSettings)
            {
                probabilitiesSum += item.SpawnProbability;
                if (probabilitiesSum >= randomProbabilitiesSum) return item;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
    [Serializable] private class ObjectSettings
    {
        [SerializeField] private T _objectPrefab = null;
        [Min(0f), SerializeField] private float _spawnProbability = 1f;
        [SerializeField] private UnityEvent _onSpawn;

        public T ObjectPrefab => _objectPrefab;
        public float SpawnProbability => _spawnProbability;
    }

    [Serializable] private struct Vector3SpawnPosition : ISpawnPosition
    {
        [SerializeField] private Vector3 _position;

        public Vector3 Get() => _position;
    }
    [Serializable] private struct TargetSpawnPosition : ISpawnPosition
    {
        [SerializeField] private Transform _target;

        public Vector3 Get() => _target.position;
    }


    public void Updating()
    {
        foreach (var item in _flows) item.Updating();
    }

    public void Spawn(Transform transform)
    {
        for (int i = 0; i < _flows.Length; i++)
        {
            UnityEngine.Object.Instantiate(_flows[i].GetRandomObjectSettings().ObjectPrefab);
        }
    }
}
