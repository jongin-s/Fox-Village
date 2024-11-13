using UnityEngine;

// 플레이어 오브젝트(Player2)에 컴포넌트로 추가

public class Attack : MonoBehaviour
{
    Animator anim;  // 애니메이션 효과
    AttackButton attackButton;  // 캔버스의 공격 버튼
    Weapon weapon;  // 장착한 무기의 Weapon 컴포넌트
    Damage damage;  // 플레이어 오브젝트의 Damage 컴포넌트
    WeaponSwitch weaponSwitch;  // 플레이어 오브젝트의 WeaponSwitch 컴포넌트

    bool isAtkReady;  // 공격 준비 여부 불리언
    float atkDelay;  // 공격 지연 시간

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        weaponSwitch = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSwitch>();
        attackButton = GameObject.Find("Canvas").GetComponent<AttackButton>();
        damage = GetComponent<Damage>();  // 컴포넌트를 가져옴
    }

    void Update()
    {
        if (weaponSwitch.equipWeapon != null)
        {
            weapon = weaponSwitch.equipWeapon.GetComponentInChildren<Weapon>();  // 장착한 무기가 있을 때 그 무기의 Weapon 컴포넌트를 가져옴, 항상 실행
        }
        Atk();  // Weapon 컴포넌트가 있을 때 공격
    }

    void Atk()
    {
        atkDelay += Time.deltaTime;  // 공격 지연 시간은 deltaTime(두 프레임 간의 시간)의 누적합
        if (weapon != null)
        {
            isAtkReady = weapon.rate < atkDelay;  // 장착한 무기가 있을 때, 무기의 공격 속도가 공격 지연 속도보다 낮으면 공격 준비 완료

            if (attackButton.click && isAtkReady && !damage.isDead)  // 공격 버튼을 클릭하고, 공격 준비 상태이고, 사망 상태가 아닐 때
            {
                weapon.Use();  // 무기 사용
                anim.SetTrigger("doSwing");  // 무기 휘두르기 애니메이션 발동
                atkDelay = 0;  // 공격 지연 속도를 초기화
            }
        }
    }
}
