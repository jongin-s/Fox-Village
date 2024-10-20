using System.Collections.ObjectModel;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{

    [HideInInspector] public int coin;  // 현재 코인의 개수, Inspector에서 입력
    public int maxCoin;  // 가능한 최대 코인의 개수, 역시 Inspector에서 입력

    public GameManager manager;  // Game Manager로 coin을 보내주기 위해 사용

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("coin"))
            coin = 0;
        else
        {
            coin = PlayerPrefs.GetInt("coin");
            coin = maxCoin;
        }
        manager.GetItem(manager.invTxt15, coin);  // coin을 Game Manager로 전달
    }

    void OnTriggerEnter(Collider other)  // 아이템의 Trigger Collider가 작동했을 때
    {
        if (other.tag == "Item")  // 충돌 물체의 태그가 "Item"이라면
        {
            Item item = other.GetComponent<Item>();  // other, 즉 아이템의 Item 컴포넌트를 가져옴

            switch (item.type)  // switch는 아이템의 타입
            {
                case Item.Type.Bronze:  // 아이템의 타입이 Bronze라면
                    coin += 25;  // coin을 Bronze의 value만큼 증가
                    break;

                case Item.Type.Silver:
                    coin += 50;
                    break;

                case Item.Type.Gold:
                    coin += 100;
                    break;  // 그 외의 다른 타입에 대해서도 동일하게 작성

                case Item.Type.Jade:
                    coin += 25;
                    break;

                case Item.Type.Diamond:
                    coin += 100;
                    break;

                case Item.Type.Ruby:
                    coin += 50;
                    break;

                case Item.Type.Amethyst:
                    coin += 50;
                    break;
            }
        }
        if (coin > maxCoin) coin = maxCoin;  // coin이 maxCoin을 넘어가지 않도록 설정
        PlayerPrefs.SetInt("coin", coin);
        manager.GetItem(manager.invTxt15, coin);  // coin을 Game Manager로 전달
    }
}
