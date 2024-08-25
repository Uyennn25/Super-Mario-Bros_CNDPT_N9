using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public int world = 1;

    public int level = 1;


    public void StartGame()
    {
        SceneManager.LoadSceneAsync($"{world}-{level}");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
