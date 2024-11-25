using UnityEngine;
using UnityEngine.SceneManagement;

// 메인 메뉴 패널의 컴포넌트

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
