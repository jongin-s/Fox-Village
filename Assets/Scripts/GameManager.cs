using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TMP 기능을 사용하기 위해 반드시 임포트

public class GameManager : MonoBehaviour {

    public TextMeshProUGUI invTxt0;  // Inventory Panel의 각 슬롯의 텍스트

    public void Awake() {

        invTxt0 = GameObject.Find("Slot (0)").GetComponentInChildren<TextMeshProUGUI>();  // 구체적인 슬롯 이름에서 자식의 컴포넌트를 가져오지 않으면 다른 "Text (TMP)" 오브젝트가 업데이트됨
    }

    public void GetItem(int count) {

        invTxt0.text = $"{count}";  // $"{count}": 문자열 내에서 변수를 그대로 출력하는 문법
    }
}
