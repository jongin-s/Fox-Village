using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JewelPanel : MonoBehaviour
{
    public ItemCollection item;  // 아이템의 종류 컴포넌트
    public GameObject[] labels;  // 알림 메시지 오브젝트
    public Button[] buttons;  // 왼쪽과 오른쪽 버튼
    public TextMeshProUGUI[] numbers;  // 각 보석의 개수를 표시하는 텍스트

    int count = 0;  // 붉은 보석의 개수를 표현

    private void Start()
    {
        labels[0].SetActive(false);
        labels[1].SetActive(false);  // 시작할 때 알림 에시지를 숨김

        buttons[0].onClick.AddListener(Decrease);  // buttons[0]: 왼쪽 버튼, buttons[1]: 오른쪽 버튼
        buttons[1].onClick.AddListener(Increase);  // Button.onClick.AddListener(): 인스펙터를 거치지 않고 버튼에 함수를 바로 할당

        numbers[0].text = PlayerPrefs.GetInt("coin").ToString();  // 현재 푸른 보석의 개수
        numbers[1].text = PlayerPrefs.GetInt("coin2").ToString();  // 현재 붉은 보석의 개수
        numbers[2].text = (count * 1000).ToString();  // 지불할 푸른 보석의 개수
        numbers[3].text = count.ToString();  // 구매할 붉은 보석의 개수
    }

    private void Update()
    {
        numbers[2].text = (count * 1000).ToString();
        numbers[3].text = count.ToString();  // 푸른 보석 1000개 = 붉은 보석 1개, 실시간으로 개수 변동을 반영
    }

    void Decrease()  // 왼쪽 버튼을 눌렀을 때
    {
        count -= 1;  // 개수 1씩 감소
        if (count < 0) { count = 0; }  // 최솟값은 0
    }

    void Increase()  // 오른쪽 버튼을 눌렀을 때
    {
        count += 1;  // 개수 1씩 증가
        if (count > 1000) { count = 1000; }  // 최댓값은 1000
    }

    public void Buy()  // 구매 버튼을 눌렀을 때
    {
        if (count * 1000 > item.coin)  // 푸른 보석의 현재 개수보다 지불하려는 개수가 더 많다면
        {
            StopCoroutine(Talk(1));
            StopCoroutine(Talk(2));
            StartCoroutine(Talk(1));
            return;  // 경고 메시지 출력
        }
        else
        {
            StopCoroutine(Talk(1));
            StopCoroutine(Talk(2));
            StartCoroutine(Talk(2));  // 성공 메시지 출력
            item.coin -= count * 1000;
            item.coin2 += count;  // 두 보석의 개수를 변경
            PlayerPrefs.SetInt("coin", item.coin);
            PlayerPrefs.SetInt("coin2", item.coin2);
            numbers[0].text = PlayerPrefs.GetInt("coin").ToString();
            numbers[1].text = PlayerPrefs.GetInt("coin2").ToString();  // 즉시 저장하고 반영
            count = 0;  // 카운트를 초기화
            numbers[2].text = (count * 1000).ToString();
            numbers[3].text = count.ToString();  // 초기화한 카운트를 반영
        }
    }

    IEnumerator Talk(int i)
    {
        if (i == 1)  // 경고 메시지
        {
            labels[0].SetActive(true);
            yield return new WaitForSeconds(2);
            labels[0].SetActive(false);
        }
        if (i == 2)  // 성공 메시지
        {
            labels[1].SetActive(true);
            yield return new WaitForSeconds(2);
            labels[1].SetActive(false);
        }
    }
}
