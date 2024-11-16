using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// 일시정지 메뉴 패널의 컴포넌트

public class PauseMenu : MonoBehaviour
{
    public GameObject[] canvas;  // 다시 보여줘야 할 모든 캔버스 요소
    public GameObject options;  // 옵션 패널
    public GameObject text;  // 게임이 저장되었음을 알리는 메시지
    GameManager manager;  // 게임 매니저, 게임 저장에 사용

    public void Awake()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();  // 게임 매니저를 수동으로 찾고 컴포넌트를 가져옴
    }

    private void Start()
    {
        options.SetActive(false);
        text.SetActive(false);  // 옵션 패널과 메시지를 숨김
    }

    public void ResumeGame()  // 게임 재개 버튼
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(true);  // UI 요소를 다시 보여줌
        }
        gameObject.SetActive(false);
        text.SetActive(false);  // 옵션 패널과 메시지를 비활성화
        Time.timeScale = 1f;  // 게임의 시간을 원래대로
    }

    public void SaveGame()  // 저장 버튼
    {
        manager.Save();  // 게임을 저장
        StopCoroutine(Text());
        StartCoroutine(Text());  // 메시지를 표시
    }

    public void Options()  // 옵션 버튼
    {
        options.SetActive(true);  // 옵션 패널을 표시
    }

    public void QuitGame()  // 종료 버튼
    {
        SceneManager.LoadScene(0);  // 메인 메뉴 씬으로 복귀
    }

    IEnumerator Text()  // 메시지를 2초 동안 출력하는 코루틴
    {
        text.SetActive(true);
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
    }
}
