using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public FixedTouchField script;
    public RectTransform uiGroup;
    public ItemCollection item;
    public WeaponSwitch swap;

    public GameObject[] weapon;
    public int[] itemPrice;
    public GameObject[] text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            script.enabled = false;
            uiGroup.anchoredPosition = Vector3.zero;
            for (int i = 0; i < text.Length; i++)
            {
                text[i].SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            script.enabled = true;
            uiGroup.anchoredPosition = Vector3.down * 1000;
        }
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];
        if (price > item.coin)
        {
            StopCoroutine(Talk(1));
            StartCoroutine(Talk(1));
            return;
        }
        else
        {
            StopCoroutine(Talk(2));
            StartCoroutine(Talk(2));
            item.coin -= price;
            swap.weapons[index] = weapon[index];
        }
    }

    IEnumerator Talk(int i)
    {
        if (i == 1)
        {
            text[0].SetActive(true);
            yield return new WaitForSeconds(2);
            text[0].SetActive(false);
        }
        if (i == 2)
        {
            text[1].SetActive(true);
            yield return new WaitForSeconds(2);
            text[1].SetActive(false);
        }
    }
}
