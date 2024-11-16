using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 오브젝트 Game Over Panel의 컴포넌트로 삽입

public class GameOverMenu : MonoBehaviour
{
    public GameObject Screen;  // 로딩 화면
    public Slider LoadingBarFill;  // 로딩 바

    private void Start()
    {
        Screen.SetActive(false);
        LoadingBarFill.gameObject.SetActive(false);  // 시작 시에는 로딩 화면과 로딩 바를 비활성화
    }

    public void Retry()  // 게임 오버 메뉴에서 Retry 버튼을 누르면 실행되는 함수
    {
        PlayerPrefs.SetInt("HP", 200);
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));  // 현재 씬을 다시 로드
        gameObject.SetActive(false);  // 이 오브젝트(게임 오버 메뉴)를 비활성화
    }

    IEnumerator LoadSceneAsync(int sceneID)  // 로딩 화면 코루틴
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneID);
        op.allowSceneActivation = true;  // 현재 씬을 다시 로드
        Time.timeScale = 1f;

        Screen.SetActive(true);
        LoadingBarFill.gameObject.SetActive(true);  // 로딩 화면과 로딩 바를 활성화

        while (!op.isDone)  // 로딩이 완료되지 않은 동안
        {
            float progressValue = Mathf.Clamp01(op.progress / 0.9f);  // 로딩 정도가 0과 1 사이의 값이 되도록 제한

            LoadingBarFill.value = progressValue;  // 로딩 바의 채움 정도는 그 로딩 정도

            yield return null;  // while문을 나갈 때는 return null
        }
    }
}
