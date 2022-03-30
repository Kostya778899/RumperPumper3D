using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using CMath;

[CreateAssetMenu(menuName = "Containers/AlwaysActiveObjects")]
public class AlwaysActiveObjectsContainer : ScriptableObject, IActivatable
{
    [SerializeField] private GameObject[] _alwaysActiveObjectsPrefabs;

    [NonSerialized] private GameObject[] _alwaysActiveObjects;
    [NonSerialized] private bool _isActivated = false;

    public void Activate()
    {
        if (_isActivated) return;
        _isActivated = true;

#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif
        _alwaysActiveObjects = new GameObject[_alwaysActiveObjectsPrefabs.Length];
        for (int i = 0; i < _alwaysActiveObjects.Length; i++) _alwaysActiveObjects[i] = Instantiate(_alwaysActiveObjectsPrefabs[i]);
    }
}
