using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

public class Speech : MonoBehaviour
{
    IntButton intButton;  // 상호작용 버튼의 컴포넌트인 스크립트
    GameManager manager;  // 게임 매니저

    public GameObject[] canvas;
    public GameObject dynamic;  // 상호작용 버튼 그 자체
    public NPCConversation conv;  // 애셋으로 개발한 NPC 대화

    private void Awake()
    {
        intButton = GameObject.Find("Canvas").GetComponent<IntButton>();
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();  // IntButton과 GameManager 스크립트를 불러옴
        dynamic.SetActive(false);  // 상호작용 버튼을 숨김
    }

    private void OnTriggerEnter(Collider other)
    {
        dynamic.SetActive(true);  // NPC와 가까워지면 상호작용 버튼이 드러남
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && intButton.click)  // 콜라이더 안의 오브젝트의 태그가 "NPC"이고 상호작용 버튼을 눌렀다면
        {
            for (int i = 0; i < canvas.Length; i++)
            {
                canvas[i].SetActive(false);
            }
            ConversationManager.Instance.StartConversation(conv);  // NPC 대화 시작
        }
        if (!ConversationManager.Instance.IsConversationActive)
        {
            for (int i = 0; i < canvas.Length; i++)
            {
                canvas[i].SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(true);
        }
        dynamic.SetActive(false);  // NPC와 멀어지면 상호작용 버튼이 다시 숨겨짐
        ConversationManager.Instance.EndConversation();  // NPC와 멀어지면 대화가 진행 중이라도 자동으로 종료됨
    }
}
