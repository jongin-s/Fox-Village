using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator anim;
    AttackButton attackButton;
    Weapon weapon;
    Damage damage;
    WeaponSwitch weaponSwitch;

    bool isAtkReady;
    float atkDelay;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        weaponSwitch = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSwitch>();
        attackButton = GameObject.Find("Canvas").GetComponent<AttackButton>();
        damage = GetComponent<Damage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponSwitch.equipWeapon != null)
        {
            weapon = weaponSwitch.equipWeapon.GetComponentInChildren<Weapon>();
        }
        Atk();
    }

    void Atk()
    {
        atkDelay += Time.deltaTime;
        if (weapon != null)
        {
            isAtkReady = weapon.rate < atkDelay;

            if (attackButton.click && isAtkReady && !damage.isDead)
            {
                weapon.Use();
                anim.SetTrigger("doSwing");
                atkDelay = 0;
            }
        }
    }
}
