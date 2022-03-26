using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePlayer : MonoBehaviour
{



    public void Play(CutScenesContainer.CutSceneSettings cutSceneSettings, Action callback)
    {
        Debug.Log($"Play:    {cutSceneSettings.Name}    {cutSceneSettings.Prefab}");
    }
}
