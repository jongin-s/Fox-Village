using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어의 컴포넌트

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] weapons;  // 플레이어 손의 무기들
    public bool[] hasWeapons;  // 각 무기의 소유 여부
    GameObject nearObject;  // 근처 아이템
    public GameObject equipWeapon;  // 현재 장착한 무기
    WeaponButton w;  // 각 무기 버튼
    int equipWeaponIndex;  // 현재 장착한 무기의 일련번호

    private void Start()
    {
        w = GameObject.Find("Canvas").GetComponent<WeaponButton>();  // 무기 버튼 컴포넌트를 가져옴
    }

    private void Update()
    {
        Swap();  // 상시 실행
    }

    void Swap()  // 실제 무기 교체 함수
    {
        if (w.sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) Debug.Log("skip");
        if (w.sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1)) return;
        if (w.sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2)) return;
        if (w.sDown4 && (!hasWeapons[3] || equipWeaponIndex == 3)) return;  // 현재 장착한 무기의 버튼을 눌렀을 때는 아무것도 일어나지 않음

        int weaponIndex = -1;  // 처음 무기의 일련번호는 -1
        if (w.sDown1) { weaponIndex = 0; Debug.Log("index"); }
        if (w.sDown2) weaponIndex = 1;
        if (w.sDown3) weaponIndex = 2;
        if (w.sDown4) weaponIndex = 3;  // 무기 버튼을 누르면 해당하는 일련번호를 호출

        if (w.sDown1 || w.sDown2 || w.sDown3 || w.sDown4)  // 어떤 무기 버튼을 누르더라도
        {
            if (equipWeapon != null)  // 현재 장착한 무기가 있다면
            {
                equipWeapon.SetActive(false);  // 그 장착한 무기를 비활성화
                Debug.Log("not null");
            }

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex];  // 새로 장착할 무기의 일련번호를 호출
            weapons[weaponIndex].SetActive(true);  // 새로 장착할 무기를 활성화
            Debug.Log("null");
        }
    }

    void Interact()  // 무기 입수 함수
    {
        Item item = nearObject.GetComponent<Item>();  // 근처 아이템의 아이템 컴포넌트를 가져옴
        int weaponIndex = item.value;  // item.value는 각 무기의 일련번호
        hasWeapons[weaponIndex] = true;  // 일련번호에 해당하는 무기의 소유여부는 true
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")  // 주운 아이템의 종류가 "Weapon"일 때
        {
            nearObject = other.gameObject;  // 근처 아이템은 주운 아이템
            Interact();  // 무기 입수 함수를 실행
        }
    }
}
