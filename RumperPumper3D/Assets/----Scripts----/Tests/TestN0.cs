using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestN0 : MonoBehaviour
{
    [SerializeField] private RandomObjectsSpawner<GameObject> _spawner;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            _spawner.Spawn();
        }
    }
}
