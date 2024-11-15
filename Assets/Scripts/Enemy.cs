using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// 몬스터 오브젝트(Enemy A, Enemy B)에 컴포넌트로 추가

public class Enemy : MonoBehaviour
{
    public enum Type { A, B };
    public Type enemyType;  // 몬스터의 타입

    public int maxHealth;  // 최대 체력
    public int curHealth;  // 현재 체력
    public float chaseRange;  // 플레이어가 가까워졌을 때 추격하는 거리
    public float homeRange;  // 홈 구역에서 돌아다니는 범위
    public bool isChase;  // 플레이어 추격 여부
    public bool isAttack;  // 플레이어 공격 여부

    public Transform target;  // 타깃 (플레이어)
    public BoxCollider meleeArea;  // 몬스터의 공격 범위
    public AudioClip hitSound;  // 맞았을 때 사운드 클립
    public AudioClip deathSound;  // 기절했을 때 사운드 클립
    public HealthBar healthBar;  // 체력 바
    public GameObject[] items;  // 기절했을 때 드랍하는 아이템들 (복수)
    public Vector3 initPosition;  // 처음 등장한 좌표, 홈 구역의 중심이 됨
    public GameObject deathParticleEffect;  // 기절했을 때 파티클 효과, 이상의 모든 인수는 인스펙터에서 설정

    public delegate void EnemyDead();
    public static event EnemyDead OnEnemyDead;  // 대리자와 상시 이벤트

    Rigidbody rigid;  // 몬스터의 Rigidbody
    NavMeshAgent nav;  // 몬스터의 추격 알고리즘

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();  // 컴포넌트를 가져옴

        curHealth = maxHealth;  // 시작할 때 현재 체력은 최대 체력과 같음
        healthBar.SetMaxHealth(maxHealth);  // 체력 바를 최대 체력으로 설정

        target = GameObject.FindWithTag("Player").transform;  // "Player" 태그를 가진 오브젝트는 하나(Player2)밖에 없으므로 타깃으로 설정
        initPosition = transform.position;  // 처음 등장한 좌표를 저장 (나중에 돌아올 수 있게)

        Invoke("ChaseStart", 0);  // 등장 즉시 추격 모드로 돌입하지만, 타깃이 가까이 다가와야만 추격
    }

    void ChaseStart()  // 추격 모드 함수
    {
        isChase = true;  // 추격 여부는 참
    }

    void Chase()  // 추격 함수
    {
        if (nav.enabled)  // 몬스터의 추격 알고리즘이 켜졌는지 확인
        {
            if (Vector3.Distance(transform.position, target.position) < chaseRange)  // 자신의 좌표와 타깃의 좌표의 거리를 재서 추격 거리보다 짧다면
            {
                nav.SetDestination(target.position);  // 타깃을 추격
            }
            else  // 아니라면
            {
                nav.SetDestination(new Vector3(initPosition.x + Random.Range(-homeRange, homeRange), initPosition.y, initPosition.z + Random.Range(-homeRange, homeRange)));  // 홈 구역을 "추격"
            }
            nav.isStopped = !isChase;  // NavMeshAgent의 정지 컨디션은 isChase 불리언의 반대
        }
    }

    private void Update()
    {
        Chase();  // 추격 함수는 상시 실행
    }

    IEnumerator Attack()  // 공격 코루틴
    {
        isChase = false;  // 공격 중일때는 잠시 추격을 멈추고
        isAttack = true;  // 공격을 실행

        switch (enemyType)
        {
            case Type.A:  // 일반형 몬스터
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;  // 1.2초 후 공격 범위가 활성화

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;  // 1초 후 공격 범위가 비활성화

                yield return new WaitForSeconds(1f);
                break;
            case Type.B:  // 돌격형 몬스터
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 25, ForceMode.Impulse);  // 현재 바라보는 방향으로 매우 빠르게 대시
                meleeArea.enabled = true;  // 2.1초 후 공격 범위가 활성화

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;  // 대시를 중지
                meleeArea.enabled = false;  // 0.5초 후 공격 범위가 비활성화

                yield return new WaitForSeconds(2f);
                break;
        }

        isChase = true;  // 공격이 끝나면 추격을 재개하고
        isAttack = false;  // 공격을 종료
    }

    void Targetting()  // 공격을 겨냥하는 함수
    {
        float targetRadius = 0;  // 겨냥 범위
        float targetRange = 0;  // 겨냥 거리

        switch (enemyType)  // 몬스터의 종류에 따라 위의 값이 달라짐
        {
            case Type.A:  // 일반형 몬스터 A는 공격을 재는 거리가 짧음
                targetRadius = 1;
                targetRange = 2;
                break;
            case Type.B:  // 돌격형 몬스터 B는 공격을 재는 거리가 김
                targetRadius = 1;
                targetRange = 10;
                break;
        }

        RaycastHit[] rayhits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));  // Raycast는 히트스캔 시스템의 이름

        // Raycast는 구를 일정한 벡터로 이동하며 작동
        // transform.position: 벡터의 시점 (몬스터의 현재 위치)
        // targetRadius: 구의 반지름
        // transform.forward: 벡터의 방향 (몬스터가 현재 바라보는 방향)
        // targetRange: 벡터의 크기
        // LayerMask: 특정한 레이어를 가져옴(Player2만이 갖고 있는 "Player" 레이어)

        if (rayhits.Length > 0 && !isAttack)  // Raycast 중 하나라도 조건을 만족하고, 현재 공격 중이 아니라면
        {
            StartCoroutine(Attack());  // 공격 코루틴을 실행
        }
    }

    void FixVelocity()  // 몬스터의 움직임을 개선하는 함수
    {
        if (isChase)  // 추격 중일 때는
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;  // 가속도를 0으로 설정 (미끄러짐을 방지하기 위해)
        }
    }

    private void FixedUpdate()
    {
        Targetting();
        FixVelocity();  // 공격 겨냥과 가속도는 물리 문제이므로 FixedUpdate로 상시 실행
    }

    private void OnTriggerEnter(Collider other)  // 무기 콜라이더와 처음으로 겹쳤을 때
    {
        if (other.tag == "Melee")  // 무기의 태그를 반드시 "Melee"로 설정!
        {
            Weapon weapon = other.GetComponent<Weapon>();  // 무기의 Weapon 컴포넌트를 가져옴
            curHealth -= weapon.damage;  // 무기의 대미지만큼 체력 감소
            healthBar.SetHealth(curHealth);  // 체력 바는 현재 체력을 반영

            Vector3 reactVec = transform.position - other.transform.position;  // 넉백 벡터

            StartCoroutine(OnDamage(reactVec));  // 맞았을 때 코루틴

            Debug.Log($"Melee: {curHealth}");  // 디버그용 로그
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)  // 맞았을 때 코루틴
    {
        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);  // 카메라 위치에서 맞았을 때 사운드 클립을 재생

        reactVec = reactVec.normalized;
        reactVec += Vector3.up;
        rigid.AddForce(reactVec * 2.5f, ForceMode.Impulse);  // 넉백 벡터 발동

        yield return new WaitForSeconds(0.1f);  // 대미지를 입은 후 0.1초 후에 다시 대미지를 입을 수 있음

        if (curHealth <= 0)  // 현재 체력이 0일 때
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);  // 카메라 위치에서 기절했을 때 사운드 클립을 재생
            gameObject.layer = 9;  // DeadEnemy로 레이어를 변경

            isChase = false;
            nav.enabled = false;  // 추격 중지

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);  // 마지막 넉백 벡터 발동

            Destroy(gameObject, 0.5f); // 몬스터 오브젝트를 삭제

            GameObject particle = Instantiate(deathParticleEffect, transform.position, Quaternion.identity);  // 파티클 효과 생성
            particle.transform.localScale = new Vector3(5, 5, 5);  // 파티클 크기 조정
            Destroy(particle, 2.0f);  // 일정 시간(2초)이 지난 후 파티클을 파괴

            Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);  // 위에서 지정한 모든 아이템들 중에 하나를 랜덤으로 드랍
        
            OnEnemyDead?.Invoke();  // 몬스터가 기절했음을 알리는 함수
        }
    }
}
