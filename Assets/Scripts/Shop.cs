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
    public int[] itemType;
    public Transform itemPos;
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
            uiGroup.anchoredPosition = Vector3.down * 2000;
        }
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];
        if (itemType[index] == 0)
        {
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
                Instantiate(weapon[index], itemPos.position, itemPos.rotation);
            }
        }
        if (itemType[index] == 1)
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
