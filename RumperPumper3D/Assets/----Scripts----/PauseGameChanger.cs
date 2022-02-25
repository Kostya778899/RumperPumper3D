using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PauseGameChanger", fileName = "New" + nameof(PauseGameChanger))]
public class PauseGameChanger : ScriptableObject
{
    [HideInInspector] public bool IsPaused { get; private set; } = false;
    [HideInInspector] public event Action OnPause, OnResume;


    public void Pause()
    {
        IsPaused = true;
        OnPause?.Invoke();

        Time.timeScale = 0f;
    }
    public void Resume()
    {
        IsPaused = false;
        OnResume?.Invoke();

        Time.timeScale = 1f;
    }
}
