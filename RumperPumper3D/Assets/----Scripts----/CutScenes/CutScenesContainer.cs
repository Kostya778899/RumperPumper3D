using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Containers/CutScenes")]
public class CutScenesContainer : ScriptableObject
{
    [SerializeField] private CutSceneSettings[] _cutScenesSettings;

    [Serializable]
    public struct CutSceneSettings
    {
        [SerializeField] private string _name;
        [SerializeField] private GameObject _prefab;

        public string Name => _name;
        public GameObject Prefab => _prefab;
    }


    public CutSceneSettings Get(in int index) => _cutScenesSettings[index];
    public int IndexOf(in string name)
    {
        for (int i = 0; i < _cutScenesSettings.Length; i++) if (_cutScenesSettings[i].Name == name) return i;
        throw new KeyNotFoundException();
    }
}
