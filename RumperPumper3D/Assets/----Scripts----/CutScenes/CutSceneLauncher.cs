using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Launchers/CutScene")]
public class CutSceneLauncher : ScriptableObject
{
    [SerializeField] private CutScenesContainer _cutScenesContainer;
    [SerializeField] private ComponentsContainer _cutScenePlayerContainer;
    [SerializeField] private string _cutScenesSceneName = "CutScenes";

    private CutScenesContainer.CutSceneSettings _currentCutSceneSettings;
    private Action _currentCallback;


    public void StartCutScene(in int index, Action callback = null)
    {
        SceneManager.LoadScene(_cutScenesSceneName);
        _currentCutSceneSettings = _cutScenesContainer.Get(index);
        _currentCallback = callback;
    }
    public void StartCutScene(in string name, Action callback = null) => StartCutScene(_cutScenesContainer.IndexOf(name), callback);
    public void StartCutScene_(int index) => StartCutScene(index);
    public void StartCutScene_(string name) => StartCutScene(name);

    private void OnEnable() => SceneManager.sceneLoaded += OnLoadedScene;
    private void OnDisable() => SceneManager.sceneLoaded -= OnLoadedScene;

    private void OnLoadedScene(Scene scene, LoadSceneMode LoadSceneMode)
    {
        if (scene.name != _cutScenesSceneName) return;
        _cutScenePlayerContainer.Get<CutScenePlayer>().Play(_currentCutSceneSettings, _currentCallback);
    }
}
