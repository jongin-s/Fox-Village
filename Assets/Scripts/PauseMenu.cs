using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] canvas;
    public GameObject options;
    GameManager manager;

    public void Awake()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Start()
    {
        options.SetActive(false);
    }

    public void ResumeGame()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(true);
        }
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SaveGame()
    {
        manager.Save();
    }

    public void Options()
    {
        options.SetActive(true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
