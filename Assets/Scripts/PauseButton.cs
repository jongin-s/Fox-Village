using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Canvas 오브젝트의 컴포넌트

public class PauseButton : MonoBehaviour
{
    public GameObject[] canvas;  // 숨겨야 할 모든 UI 요소
    public GameObject pause;  // 일시정지 메뉴
    public GameObject options;  // 옵션 메뉴

    private void Start()
    {
        pause.SetActive(false);
        options.SetActive(false);  // 처음에는 일시정지 메뉴와 옵션 메뉴를 비활성화
    }

    public void TogglePause()  // 일시정지 버튼을 누르면
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);  // UI 요소를 숨김
        }
        pause.SetActive(true);  // 일시정지 메뉴를 보여줌
        Time.timeScale = 0f;  // 게임의 시간을 멈춤
    }
}
