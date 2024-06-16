using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator anim;
    AttackButton attackButton;
    Weapon weapon;

    bool isAtkReady;
    float atkDelay;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        weapon = GetComponentInChildren<Weapon>();
        attackButton = GameObject.Find("Canvas").GetComponent<AttackButton>();
    }

    // Update is called once per frame
    void Update()
    {
        Atk();
    }

    void Atk()
    {
        atkDelay += Time.deltaTime;
        isAtkReady = weapon.rate < atkDelay;

        if (attackButton.click && isAtkReady)
        {
            weapon.Use();
            anim.SetTrigger("doSwing");
            atkDelay = 0;
        }
    }
}
