using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameSceneManager.Instance.SetSceneToLoad("Game Scene");
        SceneManager.LoadSceneAsync("Loading Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}