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

    public void LoadScene(int sceneID)
    {
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneID);

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
