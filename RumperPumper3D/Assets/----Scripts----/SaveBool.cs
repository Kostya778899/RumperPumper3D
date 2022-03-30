using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMath;

[CreateAssetMenu(menuName = "Saves/Bool")]
public class SaveBool : ScriptableObject, ISaveable<bool>
{
    [SerializeField] private string _saveKey = nameof(SaveBool).ToString();
    [SerializeField] private bool _defaultValue = false;


    public bool Get() => Convert.ToBoolean(PlayerPrefs.GetInt(_saveKey, Convert.ToInt32(_defaultValue)));
    public void Set(bool value) => PlayerPrefs.SetInt(_saveKey, Convert.ToInt32(value));
}
