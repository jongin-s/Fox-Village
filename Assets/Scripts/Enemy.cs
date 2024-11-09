using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B };
    public Type enemyType;

    public int maxHealth;
    public int curHealth;
    public float chaseRange;
    public float homeRange;
    public bool isChase;
    public bool isAttack;

    public Transform target;
    public BoxCollider meleeArea;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public HealthBar healthBar;
    public GameObject[] items;
    public Vector3 initPosition;

    public delegate void EnemyDead();
    public static event EnemyDead OnEnemyDead;
    public GameObject deathParticleEffect;


    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    NavMeshAgent nav;
    Animator anim;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        curHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        target = GameObject.FindWithTag("Player").transform;
        initPosition = transform.position;

        Invoke("ChaseStart", 0);
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Chase()
    {
        if (nav.enabled)
        {
            if (Vector3.Distance(transform.position, target.position) < chaseRange)
            {
                nav.SetDestination(target.position);
            }
            else
            {
                nav.SetDestination(new Vector3(initPosition.x + Random.Range(-homeRange, homeRange), initPosition.y, initPosition.z + Random.Range(-homeRange, homeRange)));
            }
            nav.isStopped = !isChase;
        }
    }

    private void Update()
    {
        Chase();
    }

    void Targetting()
    {
        float targetRadius = 0;
        float targetRange = 0;

        switch (enemyType)
        {
            case Type.A:
                targetRadius = 1;
                targetRange = 2;
                break;
            case Type.B:
                targetRadius = 1;
                targetRange = 10;
                break;
        }

        RaycastHit[] rayhits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

        if (rayhits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        switch (enemyType)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);
                break;
            case Type.B:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 25, ForceMode.Impulse);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                yield return new WaitForSeconds(2f);
                break;
        }

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }

    void FixVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        Targetting();
        FixVelocity();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")  // 무기의 태그를 반드시 "Melee"로 설정!
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            healthBar.SetHealth(curHealth);

            Vector3 reactVec = transform.position - other.transform.position;

            StartCoroutine(OnDamage(reactVec));

            Debug.Log($"Melee: {curHealth}");
        }
    }

IEnumerator OnDamage(Vector3 reactVec)
{
    mat.color = Color.red;
    AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);

    reactVec = reactVec.normalized;
    reactVec += Vector3.up;
    rigid.AddForce(reactVec * 2.5f, ForceMode.Impulse);

    yield return new WaitForSeconds(0.1f);

    if (curHealth > 0)
    {
        mat.color = Color.white;
    }
    else
    {
        mat.color = Color.blue;
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
        gameObject.layer = 9;

        isChase = false;
        nav.enabled = false;
        anim.SetTrigger("doDie");

        reactVec = reactVec.normalized;
        reactVec += Vector3.up;
        rigid.AddForce(reactVec * 5, ForceMode.Impulse);

        Destroy(gameObject, 0.5f);

        // 파티클 효과 생성
        GameObject particle = Instantiate(deathParticleEffect, transform.position, Quaternion.identity);
        particle.transform.localScale = new Vector3(5, 5, 5);
        // 일정 시간이 지난 후 파티클을 파괴
        Destroy(particle, 2.0f);  // 2초 후 파티클 파괴

        Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
        
        OnEnemyDead?.Invoke();
    }
}

}
