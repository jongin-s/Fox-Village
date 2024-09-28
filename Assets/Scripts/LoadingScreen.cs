using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject Screen;
    public GameObject MainMenu;
    public Slider LoadingBarFill;

    private void Start()
    {
        Screen.SetActive(false);
        MainMenu.SetActive(true);
        LoadingBarFill.gameObject.SetActive(false);
    }

    public void LoadScene()
    {
        if (!PlayerPrefs.HasKey("Scene"))
        {
            StartCoroutine(LoadSceneAsync(1));
        }
        else
        {
            StartCoroutine(LoadSceneAsync(PlayerPrefs.GetInt("Scene")));
        }
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneID);
        op.allowSceneActivation = true;
        Time.timeScale = 1f;

        Screen.SetActive(true);
        MainMenu.SetActive(false);
        LoadingBarFill.gameObject.SetActive(true);

        while (!op.isDone)
        {
            float progressValue = Mathf.Clamp01(op.progress / 0.9f);

            LoadingBarFill.value = progressValue;

            yield return null;
        }
    }
}
