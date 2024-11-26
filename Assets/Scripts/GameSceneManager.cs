using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    [SerializeField] private string sceneToLoad;
    public string GetSceneToLoad() { return sceneToLoad; }
    public void SetSceneToLoad(string s) { sceneToLoad = s; }
}