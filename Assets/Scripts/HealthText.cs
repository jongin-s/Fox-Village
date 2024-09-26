using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI text;
    Damage damage;

    private void Start()
    {
        damage = player.GetComponent<Damage>();
    }

    private void Update()
    {
        text.text = damage.curHealth + "/" + damage.maxHealth;
    }
}
