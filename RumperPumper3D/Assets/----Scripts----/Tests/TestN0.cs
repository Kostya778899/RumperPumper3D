using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestN0 : MonoBehaviour
{
    [SerializeField] private RandomObjectsSpawner<GameObject> _spawner;

    private void Start()
    {
        _spawner.Updating();
        for (int i = 0; i < 100; i++) _spawner.Spawn(transform);
    }
}
