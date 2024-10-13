using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] weapons;
    GameObject equipWeapon;

    Weapon wep1;
    Weapon wep2;

    MeshRenderer weps1;
    MeshRenderer weps2;

    Collider wepc1;
    Collider wepc2;

    bool w1 = false;
    bool w2 = false;

    public void Weapon1Down()
    {
        w1 = true;
    }

    public void Weapon1Up()
    {
        w1 = false;
    }

    public void Weapon2Down()
    {
        w2 = true;
    }

    public void Weapon2Up()
    {
        w2 = false;
    }

    private void Start()
    {
        wep1 = weapons[0].GetComponent<Weapon>();
        weps1 = weapons[0].GetComponentInChildren<MeshRenderer>();
        wepc1 = weapons[0].GetComponent<Collider>();

        wep2 = weapons[1].GetComponent<Weapon>();
        weps2 = weapons[1].GetComponentInChildren<MeshRenderer>();
        wepc2 = weapons[1].GetComponent<Collider>();

        wep1.enabled = true;
        weps1.enabled = true;
        wepc1.enabled = true;

        wep2.enabled = false;
        weps2.enabled = false;
        wepc2.enabled = false;
    }

    public void Swap()
    {
        if (w1 || w2)
        {
            if (equipWeapon != null)
            {
                if (w1)
                {
                    wep2.enabled = false;
                    weps2.enabled = false;
                    wepc2.enabled = false;

                    wep1.enabled = true;
                    weps1.enabled = true;
                    wepc1.enabled = true;
                }
                if (w2)
                {
                    wep1.enabled = false;
                    weps1.enabled = false;
                    wepc1.enabled = false;

                    wep2.enabled = true;
                    weps2.enabled = true;
                    wepc2.enabled = true;
                }
            }
            else
            {
                equipWeapon = weapons[0];
                wep1.enabled = true;
                weps1.enabled = true;
                wepc1.enabled = true;
            }
        }
    }
}
