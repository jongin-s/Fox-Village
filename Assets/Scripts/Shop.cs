using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 샵 입구 영역의 컴포넌트

public class Shop : MonoBehaviour
{
    public FixedTouchField script;  // 카메라 터치를 가능하게 하는 컴포넌트
    public RectTransform uiGroup;  // UI 집단(여기서는 샵 패널)
    public ItemCollection item;  // 아이템 컬렉션 컴포넌트
    public WeaponSwitch swap;  // 무기 교체 컴포넌트

    public GameObject[] weapon;  // 무기 아이템들
    public int[] itemPrice;  // 각 무기의 가격
    public int[] itemType;  // 각 무기가 푸른 보석(0)을 요구하는지, 붉은 보석(1)을 요구하는지 여부
    public Transform itemPos;  // 무기 아이템이 등장할 위치
    public GameObject[] text;  // 샵 패널의 메시지, 이상은 모두 인스펙터에서 설정

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")  // 플레이어가 샵 입구에 들어서면
        {
            script.enabled = false;  // 카메라 이동을 비활성화
            uiGroup.anchoredPosition = Vector3.zero;  // 샵 패널을 화면 중앙으로
            for (int i = 0; i < text.Length; i++)
            {
                text[i].SetActive(false);  // 샵 패널의 메시지를 비활성화
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")  // 플레이어가 샵 입구를 나서면
        {
            script.enabled = true;  // 카메라 이동을 다시 활성화
            uiGroup.anchoredPosition = Vector3.down * 2000;  // 샵 패널을 원래 위치로
        }
    }

    public void Buy(int index)  // 구매 함수
    {
        int price = itemPrice[index];  // 지불할 가격을 설정
        if (itemType[index] == 0)  // 푸른 보석을 지불해야 한다면
        {
            if (price > item.coin)  // 보석이 부족하다면
            {
                StopCoroutine(Talk(1));
                StartCoroutine(Talk(1));  // 잔돈 부족 메시지를 출력
                return;
            }
            else
            {
                StopCoroutine(Talk(2));
                StartCoroutine(Talk(2));  // 구매 성공 메시지를 출력
                item.coin -= price;  // 현재 잔돈에서 가격을 차감
                Instantiate(weapon[index], itemPos.position, itemPos.rotation);  // 지정한 위치에 무기 아이템을 스폰
            }
        }
        if (itemType[index] == 1)  // 붉은 보석에 대해서도 똑같이 적용
        {
            if (price > item.coin2)
            {
                StopCoroutine(Talk(1));
                StartCoroutine(Talk(1));
                return;
            }
            else
            {
                StopCoroutine(Talk(2));
                StartCoroutine(Talk(2));
                item.coin2 -= price;
                Instantiate(weapon[index], itemPos.position, itemPos.rotation);
            }
        }
    }

    IEnumerator Talk(int i)  // 메시지 함수
    {
        if (i == 1)  // 보석이 부족하다면
        {
            text[0].SetActive(true);
            yield return new WaitForSeconds(2);
            text[0].SetActive(false);  // 잔돈 부족 메시지를 출력
        }
        if (i == 2)  // 보석이 충분하다면
        {
            text[1].SetActive(true);
            yield return new WaitForSeconds(2);
            text[1].SetActive(false);  // 구매 성공 메시지를 출력
        }
    }
}
