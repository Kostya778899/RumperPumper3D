using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Containers/AlwaysActiveObjects")]
public class AlwaysActiveObjectsContainer : ScriptableObject
{
    [SerializeField] private GameObject[] _alwaysActiveObjectsPrefabs;

    [NonSerialized] private GameObject[] _alwaysActiveObjects;
    [NonSerialized] private bool _isActivated = false;


    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
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
