using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject Screen;
    public Slider LoadingBarFill;

    private void Start()
    {
        Screen.SetActive(false);
        LoadingBarFill.gameObject.SetActive(false);
    }

    public void Retry()
    {
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
        gameObject.SetActive(false);
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneID);
        op.allowSceneActivation = true;
        Time.timeScale = 1f;

        Screen.SetActive(true);
        LoadingBarFill.gameObject.SetActive(true);

        while (!op.isDone)
        {
            float progressValue = Mathf.Clamp01(op.progress / 0.9f);

            LoadingBarFill.value = progressValue;

            yield return null;
        }
    }
}
