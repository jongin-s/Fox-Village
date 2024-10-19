using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{
    public GameObject Screen;
    public Slider LoadingBarFill;

    private void Start()
    {
        Screen.SetActive(false);
        LoadingBarFill.gameObject.SetActive(false);
    }

    void Update()
    {
        if (transform.position.y <= 0)
        {
            StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
        }
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
