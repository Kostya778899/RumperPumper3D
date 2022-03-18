using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMath;

public class RoadTile : MonoBehaviour
{
    [SerializeField] private RoadTileSettings _settings;

    private List<Let> _lets;


    public void UpdatingLets()
    {
        int letsToSpawnCount = UnityEngine.Random.Range(_settings.LetsToSpawnMinCount, _settings.LetsToSpawnMaxCount);

        if (_lets != null) foreach (var item in _lets) Destroy(item.gameObject);
        _lets = new List<Let>();

        for (int i = 0; i < letsToSpawnCount; i++)
        {
            _lets.Add(Instantiate(_settings.LetsPrefabs.RandomItem(), transform).GetComponent<Let>());
        }
        RandomizeLetsPositions(_lets);
    }
    private void RandomizeLetsPositions(List<Let> value) {
        foreach (var item in value) item.transform.localPosition = _settings.LetsToSpawnPositions.RandomItem(); }

    //private void Start() => UpdatingLets();
}
