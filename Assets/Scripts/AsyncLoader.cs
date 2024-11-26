using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour
{
    [SerializeField] Image loadingSlider;

    private void Start()
    {
        LoadScene(GameSceneManager.Instance.GetSceneToLoad());
    }

    public void LoadScene(string sceneToLoad)
    {
        Debug.Log(GameSceneManager.Instance.GetSceneToLoad());
        StartCoroutine(LoadLevelAsync(GameSceneManager.Instance.GetSceneToLoad()));
    }

    IEnumerator LoadLevelAsync(string sceneToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (loadOperation.isDone == false)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.fillAmount = progressValue;
            yield return null;
        }
    }
}