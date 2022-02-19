using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : MonoBehaviour
{
    [SerializeField] private RoadTileSettings _settings;

    private List<LetDefault> _lets;


    public void Updating()
    {
        int letsToSpawnCount = UnityEngine.Random.Range(_settings.LetsToSpawnMinCount, _settings.LetsToSpawnMaxCount);

        if (_lets != null) foreach (var item in _lets) Destroy(item);
        _lets = new List<LetDefault>();

        for (int i = 0; i < letsToSpawnCount; i++)
        {
            _lets.Add(Instantiate(_settings.LetsPrefabs.GetRandomElement(), transform).GetComponent<LetDefault>());
        }
        RandomizeLetsPosition(_lets);
    }
    private void RandomizeLetsPosition(List<LetDefault> value) {
        foreach (var item in value) item.transform.localPosition = _settings.LetsToSpawnPositions.GetRandomElement(); }

    private void Start() => Updating();
}
