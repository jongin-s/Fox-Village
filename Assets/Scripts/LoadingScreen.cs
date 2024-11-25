using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 메인 메뉴 씬의 빈 오브젝트 LoadingScreen의 컴포넌트

public class LoadingScreen : MonoBehaviour
{
    public GameObject Screen;  // 로딩 화면
    public GameObject MainMenu;  // 메인 메뉴 패널
    public Slider LoadingBarFill;  // 로딩 바

    private void Start()
    {
        Screen.SetActive(false);
        MainMenu.SetActive(true);
        LoadingBarFill.gameObject.SetActive(false);  // 메인 메뉴는 활성, 로딩 화면과 바는 비활성
    }

    public void LoadScene()  // 게임 시작 버튼을 눌렀을 때
    {
        if (!PlayerPrefs.HasKey("Scene"))  // 저장된 씬 번호가 없다면
        {
            StartCoroutine(LoadSceneAsync(1));  // 첫 번째 맵을 로드
        }
        else
        {
            StartCoroutine(LoadSceneAsync(PlayerPrefs.GetInt("Scene")));  // 아니라면 저장된 씬을 로드
        }
    }

    IEnumerator LoadSceneAsync(int sceneID)  // 씬을 로딩하는 코루틴
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneID);
        op.allowSceneActivation = true;  // 씬을 직접 로드하는 함수
        Time.timeScale = 1f;

        Screen.SetActive(true);
        MainMenu.SetActive(false);
        LoadingBarFill.gameObject.SetActive(true);  // 메인 메뉴는 비활성, 로딩 화면과 바는 활성

        while (!op.isDone)  // 로딩이 완료되기 전까지
        {
            float progressValue = Mathf.Clamp01(op.progress / 0.9f);

            LoadingBarFill.value = progressValue;

            yield return null;  // 로딩 정도를 로딩 바에 반영
        }
    }
}
