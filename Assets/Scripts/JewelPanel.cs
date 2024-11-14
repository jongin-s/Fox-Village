using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JewelPanel : MonoBehaviour
{
    public FixedTouchField script;
    public ItemCollection item;
    public GameObject[] labels;
    public Button[] buttons;
    public TextMeshProUGUI[] numbers;

    int count = 0;

    private void Start()
    {
        labels[0].SetActive(false);
        labels[1].SetActive(false);

        buttons[0].onClick.AddListener(Decrease);
        buttons[1].onClick.AddListener(Increase);

        numbers[0].text = item.coin.ToString();
        numbers[1].text = item.coin2.ToString();
        numbers[2].text = (count * 1000).ToString();
        numbers[3].text = count.ToString();
    }

    private void Update()
    {
        numbers[2].text = (count * 1000).ToString();
        numbers[3].text = count.ToString();
    }

    void Decrease()
    {
        count -= 1;
        if (count < 0) { count = 0; }
    }

    void Increase()
    {
        count += 1;
        if (count > 1000) { count = 1000; }
    }

    public void Buy()
    {
        if (count * 1000 > item.coin)
        {
            StopCoroutine(Talk(1));
            StartCoroutine(Talk(1));
            return;
        }
        else
        {
            StopCoroutine(Talk(2));
            StartCoroutine(Talk(2));
            item.coin -= count * 1000;
            item.coin2 += count;
            numbers[0].text = item.coin.ToString();
            numbers[1].text = item.coin2.ToString();
            numbers[2].text = (count * 1000).ToString();
            numbers[3].text = count.ToString();
        }
    }

    IEnumerator Talk(int i)
    {
        if (i == 1)
        {
            labels[0].SetActive(true);
            yield return new WaitForSeconds(2);
            labels[0].SetActive(false);
        }
        if (i == 2)
        {
            labels[1].SetActive(true);
            yield return new WaitForSeconds(2);
            labels[1].SetActive(false);
        }
    }
}
