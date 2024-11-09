using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] canvas;
    public GameObject options;
    public GameObject text;
    GameManager manager;

    public void Awake()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Start()
    {
        options.SetActive(false);
        text.SetActive(false);
    }

    public void ResumeGame()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(true);
        }
        gameObject.SetActive(false);
        text.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SaveGame()
    {
        manager.Save();
        StopCoroutine(Text());
        StartCoroutine(Text());
    }

    public void Options()
    {
        options.SetActive(true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator Text()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
    }
}
