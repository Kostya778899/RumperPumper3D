using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using CMath;

public class RoadTile : MonoBehaviour, IUpdatable
{
    [SerializeField] private RandomObjectsSpawner<Let> _letsSpawner;
    [SerializeField] private RandomObjectsSpawner<GameObject> _environmentSpawner;

    [SerializeField] private RoadTileSettings _settings;

    private List<Let> _lets = new(5);
    private List<GameObject> _environment = new(5);


    public void Updating()
    {
        foreach (var item in _lets) Destroy(item.gameObject);
        foreach (var item in _environment) Destroy(item.gameObject);

        _lets = _letsSpawner.Spawn().ToList();
        _environment = _environmentSpawner.Spawn().ToList();
    }

    private void Awake()
    {
        _letsSpawner.Activate();
        _environmentSpawner.Activate();
        _environment = _environmentSpawner.Spawn().ToList();
    }
}
