using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePlayer : MonoBehaviour
{



    public void Play(CutScenesContainer.CutSceneSettings cutSceneSettings, Action callback)
    {
        Debug.Log($"Play:    {cutSceneSettings.Name}    {cutSceneSettings.Target}");

        var cutScene = Instantiate<CutScene>(cutSceneSettings.Target, transform);
        cutScene.OnComplete.AddListener(() => callback?.Invoke());
        cutScene.Activate();
    }
}
