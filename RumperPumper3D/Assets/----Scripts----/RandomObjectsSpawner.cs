using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using CMath;

[Serializable]
public class RandomObjectsSpawner<T> : IActivatable where T : UnityEngine.Object
{
    public UnityEvent<int> OnSpawn;

    [SerializeField] private Flow[] _flows;
    [SerializeField] private Transform[] _spawnPositions;


    private interface ISpawnPosition { public Vector3 Get(); }

    [Serializable] private class Flow : IActivatable
    {
        [SerializeField] private ObjectSettings[] _objectsSettings = null;
        [SerializeField] private int[] _enabledPositionsIndexes = null;
        [Range(0f, 1f), SerializeField] private float _notSpawnProbability = 0f;
        [Min(1), SerializeField] private int _spawnObjectsCount = 1;

        public int SpawnObjectsCount => _spawnObjectsCount;

        private float _generalSpawnProbability = 0f;


        public void Activate()
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


    public void Activate()
    {
        foreach (var item in _flows) item.Activate();
    }

    public T[] Spawn()
    {
        List<T> objects = new(_flows.Length);

        foreach (var item in _flows)
        {
            List<int> busyPositionsIndexes = new(1);
            Transform GetRandomSpawnPosition(ObjectSettings objectSettings)
            {
                int spawnPositionIndex = 0;

                if (busyPositionsIndexes is not null && busyPositionsIndexes.Count > 0)
                {
                    const int attemptsCount = 15;
                    for (int i0 = 0; i0 < attemptsCount; i0++)
                    {
                        spawnPositionIndex = item.GetRandomSpawnPositionIndex(objectSettings);

                        foreach (var item in busyPositionsIndexes)
                        {
                            if (item != spawnPositionIndex)
                            {
                                busyPositionsIndexes.Add(spawnPositionIndex);
                                return _spawnPositions[spawnPositionIndex];
                            }
                        }
                    }
                }
                else
                {
                    spawnPositionIndex = item.GetRandomSpawnPositionIndex(objectSettings);
                    busyPositionsIndexes.Add(spawnPositionIndex);
                    return _spawnPositions[spawnPositionIndex];
                }

                Debug.Log("LL");
                return null;
            }
            for (int i = 0; i < item.SpawnObjectsCount; i++)
            {
                ObjectSettings objectSettings = item.GetRandomObjectSettings();
                if (objectSettings is not null)
                {
                    //Transform spawnPosition = _spawnPositions[item.GetRandomSpawnPositionIndex(objectSettings)];
                    Transform spawnPosition = GetRandomSpawnPosition(objectSettings) ?? _spawnPositions[0];
                    T @object = UnityEngine.Object.Instantiate(objectSettings.ObjectPrefab, spawnPosition);
                    objects.Add(@object);
                }
            }
        }

        return objects.ToArray();
    }
}
