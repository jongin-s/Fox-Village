using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] weapons;
    public bool[] hasWeapons;
    GameObject nearObject;
    public GameObject equipWeapon;
    WeaponButton w;
    int equipWeaponIndex;

    private void Start()
    {
        w = GameObject.Find("Canvas").GetComponent<WeaponButton>();

        //hasWeapons[0] = true;
        //hasWeapons[1] = true;
        //hasWeapons[2] = true;
        //hasWeapons[3] = true;
    }

    private void Update()
    {
        Swap();
    }

    void Swap()
    {
        if (w.sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) Debug.Log("skip");
        if (w.sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1)) return;
        if (w.sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2)) return;
        if (w.sDown4 && (!hasWeapons[3] || equipWeaponIndex == 3)) return;

        int weaponIndex = -1;
        if (w.sDown1) { weaponIndex = 0; Debug.Log("index"); }
        if (w.sDown2) weaponIndex = 1;
        if (w.sDown3) weaponIndex = 2;
        if (w.sDown4) weaponIndex = 3;

        if (w.sDown1 || w.sDown2 || w.sDown3 || w.sDown4)
        {
            if (equipWeapon != null)
            {
                equipWeapon.SetActive(false);
                Debug.Log("not null");
            }

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex];
            weapons[weaponIndex].SetActive(true);
            Debug.Log("null");
        }
    }

    void Interact()
    {
        Item item = nearObject.GetComponent<Item>();
        int weaponIndex = item.value;
        hasWeapons[weaponIndex] = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = other.gameObject;
            Interact();
        }
    }
}
