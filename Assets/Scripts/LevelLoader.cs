using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadGeme()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadNewGeme()
    {
        FindObjectOfType<PlayerPrefsController>().ResetGame();
        SceneManager.LoadScene("Game");
    }
   
}
