using System.Collections.ObjectModel;
using UnityEngine;

// 플레이어 오브젝트의 컴포넌트로 삽입

public class ItemCollection : MonoBehaviour
{

    [HideInInspector] public int coin;  // 현재 푸른 보석의 개수, Inspector에서 숨김
    public int maxCoin;  // 가능한 최대 푸른 보석의 개수, Inspector에서 입력
    [HideInInspector] public int coin2;  // 현재 붉은 보석의 개수, Inspector에서 숨김
    public int maxCoin2;  // 가능한 최대 붉은 보석의 개수, Inspector에서 입력

    public GameManager manager;  // Game Manager로 coin을 보내주기 위해 사용

    WeaponSwitch weaponSwitch;  // 플레이어의 무기 교체 컴포넌트

    private void Start()
    {
        weaponSwitch = GetComponent<WeaponSwitch>();  // 컴포넌트를 가져옴

        if (!PlayerPrefs.HasKey("coin") || !PlayerPrefs.HasKey("coin2"))  // 푸른 보석과 붉은 보석 중 하나라도 null이라면
        {
            coin = 0;
            coin2 = 0;  // 둘 다 그 값은 0
        }
        else
        {
            coin = PlayerPrefs.GetInt("coin");
            coin2 = PlayerPrefs.GetInt("coin2");  // 아니라면 저장된 값을 가져와서 적용
        }
        manager.GetItem(manager.invTxt0, coin);
        manager.GetItem(manager.invTxt1, coin2);  // 보석 값을 Game Manager로 전달
    }

    void OnTriggerEnter(Collider other)  // 아이템의 Trigger Collider가 작동할 때마다
    {
        if (other.tag == "Item")  // 충돌 물체의 태그가 "Item"이라면
        {
            Item item = other.GetComponent<Item>();  // other, 즉 아이템의 Item 컴포넌트를 가져옴

            switch (item.type)  // switch는 아이템의 타입
            {
                case Item.Type.Jade:  // 아이템의 타입이 Jade라면
                    coin += 25;  // coin을 Jade의 value만큼 증가
                    break;

                case Item.Type.Diamond:
                    coin += 100;
                    break;

                case Item.Type.Ruby:
                    coin += 50;
                    break;

                case Item.Type.Amethyst:
                    coin += 50;
                    break;  // 그 외의 다른 타입에 대해서도 동일하게 작성

                case Item.Type.Weapon:
                    weaponSwitch.hasWeapons[item.value] = true;
                    break;  // Weapon 타입 무기라면 상응하는 일련번호의 무기의 소유 여부를 true로 설정
            }
        }
        if (coin > maxCoin) coin = maxCoin;
        if (coin2 > maxCoin2) coin2 = maxCoin2;  // coin이 maxCoin을 넘어가지 않도록 설정
        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetInt("coin2", coin2);  // 보석 값을 업데이트하고 저장
        manager.GetItem(manager.invTxt0, coin);
        manager.GetItem(manager.invTxt1, coin2);  // coin을 Game Manager로 전달
    }
}
