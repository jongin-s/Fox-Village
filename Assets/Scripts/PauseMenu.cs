using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] canvas;
    GameManager manager;

    public void Awake()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void ResumeGame()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(true);
        }
        gameObject.SetActive(false);
    }

    public void SaveGame()
    {
        manager.Save();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
