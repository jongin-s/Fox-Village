using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public CanvasGroup[] canvasGroups;  // 숨겨야 할 모든 UI 요소의 CanvasGroup
    public GameObject pause;  // 일시정지 메뉴
    public GameObject options;  // 옵션 메뉴

    private void Start()
    {
        pause.SetActive(false);
        options.SetActive(false);  // 처음에는 일시정지 메뉴와 옵션 메뉴를 비활성화
    }

    public void TogglePause()  // 일시정지 버튼을 누르면
    {
        for (int i = 0; i < canvasGroups.Length; i++)
        {
            CanvasGroup canvasGroup = canvasGroups[i];
            canvasGroup.alpha = 0f;  // 투명도를 0으로 설정
            canvasGroup.interactable = false;  // 상호작용 비활성화
            canvasGroup.blocksRaycasts = false;  // UI 클릭 차단
        }

        pause.SetActive(true);  // 일시정지 메뉴를 보여줌
        Time.timeScale = 0f;  // 게임의 시간을 멈춤
    }
}
