using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using CMath;

[Serializable]
public class RandomObjectsSpawner<T> : IUpdatable where T : UnityEngine.Object
{
    public UnityEvent<int> OnSpawn;

    [SerializeField] private Flow[] _flows;
    [SerializeField] private Transform[] _spawnPositions;


    private interface ISpawnPosition { public Vector3 Get(); }

    [Serializable] private class Flow : IUpdatable
    {
        [SerializeField] private ObjectSettings[] _objectsSettings = null;
        [SerializeField] private int[] _enabledPositionsIndexes = null;
        [Range(0f, 1f), SerializeField] private float _notSpawnProbability = 0f;
        private float _generalSpawnProbability = 0f;


        public void Updating()
        {
            _generalSpawnProbability = 0f;
            foreach (var item in _objectsSettings) _generalSpawnProbability += item.SpawnProbability;
        }

        public ObjectSettings GetRandomObjectSettings()
        {
            if (UnityEngine.Random.value < _notSpawnProbability) return null;

            float randomProbabilitiesSum = UnityEngine.Random.Range(0f, _generalSpawnProbability);
            float probabilitiesSum = 0f;
            foreach (var item in _objectsSettings)
            {
                probabilitiesSum += item.SpawnProbability;
                if (probabilitiesSum >= randomProbabilitiesSum) return item;
            }

            throw new ArgumentOutOfRangeException();
        }
        public int GetRandomSpawnPositionIndex(ObjectSettings objectSettings)
        {
            if (objectSettings.CustomEnabledPositionsIndexes != null && objectSettings.CustomEnabledPositionsIndexes.Length > 0)
                return objectSettings.CustomEnabledPositionsIndexes.RandomItem();
            return _enabledPositionsIndexes.RandomItem();
        }
    }
    [Serializable] private class ObjectSettings
    {
        [SerializeField] private T _objectPrefab = null;
        [Min(0f), SerializeField] private float _spawnProbability = 1f;
        [SerializeField] private int[] _customEnabledPositionsIndexes = null;
        [SerializeField] private UnityEvent _onSpawn;

        public T ObjectPrefab => _objectPrefab;
        public float SpawnProbability => _spawnProbability;
        public int[] CustomEnabledPositionsIndexes => _customEnabledPositionsIndexes;
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

    public T[] Spawn()
    {
        List<T> objects = new(_flows.Length);

        for (int i = 0; i < _flows.Length; i++)
        {
            ObjectSettings objectSettings = _flows[i].GetRandomObjectSettings();
            if (objectSettings is not null)
            {
                Transform spawnPosition = _spawnPositions[_flows[i].GetRandomSpawnPositionIndex(objectSettings)];
                T @object = UnityEngine.Object.Instantiate(objectSettings.ObjectPrefab, spawnPosition);
                objects.Add(@object);
            }
        }

        return objects.ToArray();
    }
}
