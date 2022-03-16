using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : SceneLoader
{
    public void Reset_() => LoadScene(SceneManager.GetActiveScene().buildIndex);
}
