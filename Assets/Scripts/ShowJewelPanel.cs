using UnityEngine;

public class ShowJewelPanel : MonoBehaviour
{
    public GameObject jewelPanel; // JewelPanel을 드래그하여 연결합니다.
    
    private void Start()
    {
        if (jewelPanel != null)
        {
            jewelPanel.SetActive(false); // 시작할 때 JewelPanel을 비활성화
            CenterPanel(); // 패널을 캔버스의 중앙으로 이동
        }
    }

    // 패널의 활성/비활성 상태를 토글하는 함수
    public void ToggleJewelPanel()
    {
        if (jewelPanel != null)
        {
            bool isActive = jewelPanel.activeSelf;
            jewelPanel.SetActive(!isActive); // 현재 상태의 반대로 설정
        }
    }

    // JewelPanel을 캔버스 중앙으로 이동하는 함수
    private void CenterPanel()
    {
        RectTransform panelRect = jewelPanel.GetComponent<RectTransform>();
        if (panelRect != null)
        {
            panelRect.anchoredPosition = Vector2.zero; // 중앙으로 설정
        }
    }
}
