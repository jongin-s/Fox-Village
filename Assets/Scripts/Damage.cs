using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 오브젝트(Player2)에 컴포넌트로 추가

public class Damage : MonoBehaviour
{
    public int maxHealth;  // 최대 체력, 인스펙터에서 설정
    [HideInInspector] public int curHealth;  // 현재 체력
    [HideInInspector] public bool isDamage;  // 대미지 여부
    [HideInInspector] public bool isDead;  // 기절 여부, 아래 3개의 인수는 인스펙터에서 숨김

    public AudioClip hitSound;  // 적에게 맞았을 때 사운드 클립
    public AudioClip deathSound;  // 기절했을 때 사운드 클립
    public HealthBar healthBar;  // UI의 플레이어 체력 바
    public GameObject menu;  // 리플레이 메뉴

    Rigidbody rigid;  // 플레이어의 Rigidbody
    Animator anim;  // 플레이어 메시의 애니메이터

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();  // 컴포넌트를 가져옴

        if (!PlayerPrefs.HasKey("HP"))  // 저장된 HP 값이 없다면
        {
            curHealth = maxHealth;  // 시작할 때 현재 체력은 최대 체력과 같음
        }
        else
        {
            curHealth = PlayerPrefs.GetInt("HP");  // 아니라면 그 HP 값으로 지정
        }
        healthBar.SetMaxHealth(maxHealth);  // 체력 바를 최대 체력으로 설정
        healthBar.SetHealth(curHealth);  // 그 다음 체력 바를 현재 체력으로 설정
        menu.SetActive(false);  // 시작할 때 리플레이 메뉴는 꺼져 있어야 함
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyAttack")
        {
            if (!isDamage && curHealth > 0)  // EnemyAttack 콜라이더와 처음 겹치고, 현재 대미지 상태가 아니고, 현재 체력이 0보다 클 때
            {
                EnemyAttack enemyAtk = other.GetComponent<EnemyAttack>();  // 콜라이더의 EnemyAttack 컴포넌트를 가져옴
                curHealth -= enemyAtk.damage;  // 플레이어의 현재 체력에서 EnemyAttack의 대미지를 뺌
                healthBar.SetHealth(curHealth);  // 체력 바를 현재 체력으로 설정

                Vector3 reactVec = transform.position - other.transform.position;  // 넉백 3차원 벡터 reactVec은 몬스터에서 플레이어로 이은 직선과 같음

                StartCoroutine(OnDamage(reactVec));  // OnDamage 코루틴은 reactVec을 인수로 받음

                Debug.Log($"Player: {curHealth}");  // 디버그용 로그
            }
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)  // 플레이어가 대미지를 입을 때 발동하는 코루틴
    {
        isDamage = true;  // 대미지 상태 발동
        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);  // 카메라 포지션에서 맞았을 때 오디오 클립을 재생

        reactVec = reactVec.normalized;  // reactVec의 값을 1로 치환
        reactVec += Vector3.up;  // reactVec에 Vector3(0, 1, 0)을 더함 (약간 튀어오르는 효과를 위해)
        rigid.AddForce(reactVec * 2.5f, ForceMode.Impulse);  // reactVec을 2.5배, Rigidbody에 순간적으로 가함

        yield return new WaitForSeconds(0.1f);

        isDamage = false;  // 0.1초 후 대미지 상태 해제

        if (curHealth <= 0)  // 현재 체력이 0 이하일 때
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);  // 카메라 포지션에서 기절했을 때 오디오 클립을 재생
            gameObject.layer = 9;  // DeadEnemy로 레이어를 변경

            anim.SetTrigger("doDie");  // 기절 애니메이션 발동

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);  // 위의 맞았을 때 reactVec과 배수만 빼고 동일
            isDead = true;  // 기절 여부 불리언, 다른 스크립트에서도 사용
            menu.SetActive(true);  // 리플레이 메뉴 활성화
        }
    }
}
