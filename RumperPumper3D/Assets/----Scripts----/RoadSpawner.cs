using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private RoadTile _roadTilePrefab;
    [SerializeField] private Transform _endRoadTilePosition;
    [Min(0f), SerializeField] private int _poolingTilesCount = 10;
    [Min(0f), SerializeField] private float _roadMoveSpeed = 3f;
    [SerializeField] private Vector3 _roadMoveDirection = Vector3.forward;
    [Min(0f), SerializeField] private Vector3 _distanceBetweenTiles = Vector3.one;

    private List<RoadTile> _roadTiles = new List<RoadTile>();


    private void Start()
    {
        SpawnTiles(_poolingTilesCount);
        StartCoroutine(MoveRoad());
    }

    private IEnumerator MoveRoad()
    {
        for (float t = 0; ; t += Time.deltaTime)
        {
            for (int i0 = 0; i0 < _roadTiles.Count; i0++)
            {
                if (_roadTiles[i0].transform.localPosition.IsMore(_endRoadTilePosition.localPosition))
                {
                    _roadTiles.Move(_roadTiles.Count - 1, 0);
                    t = 0f;
                    break;
                }
            }

            for (int i0 = 0; i0 < _roadTiles.Count; i0++)
                _roadTiles[i0].transform.localPosition = (_distanceBetweenTiles * i0) + (_roadMoveDirection * _roadMoveSpeed * t);
            yield return null;
        }
    }

    public void SpawnTiles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _roadTiles.Add(Instantiate(_roadTilePrefab, transform).GetComponent<RoadTile>());
            //_roadTiles[i].transform.localPosition = _distanceBetweenTiles * i;
        }
    }
}
