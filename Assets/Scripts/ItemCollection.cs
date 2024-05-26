using UnityEngine;

public class ItemCollection : MonoBehaviour {

    public int coin;  // 현재 코인의 개수, Inspector에서 입력
    public int maxCoin;  // 가능한 최대 코인의 개수, 역시 Inspector에서 입력

    public GameManager manager;  // Game Manager로 coin을 보내주기 위해 사용

    void OnTriggerEnter(Collider other)  // 아이템의 Trigger Collider가 작동했을 때
    {
        if (other.tag == "Item")  // 충돌 물체의 태그가 "Item"이라면
        {
            Item item = other.GetComponent<Item>();  // other, 즉 아이템의 Item 컴포넌트를 가져옴

            switch (item.type)  // switch는 아이템의 타입
            {
                case Item.Type.Bronze:  // 아이템의 타입이 Bronze라면
                    coin += item.value;  // coin을 Bronze의 value만큼 증가
                    break;

                case Item.Type.Silver:
                    coin += item.value;
                    break;

                case Item.Type.Gold:
                    coin += item.value;
                    break;  // 그 외의 다른 타입에 대해서도 동일하게 작성
            }
        }
        if (coin > maxCoin) coin = maxCoin;  // coin이 maxCoin을 넘어가지 않도록 설정
        manager.GetItem(coin);  // coin을 Game Manager로 전달
    }
}
