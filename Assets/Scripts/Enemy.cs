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
    public int damage;
    public bool isChase;
    public bool isAttack;

    public Transform target;
    public BoxCollider meleeArea;
    public AudioClip hitSound;
    public AudioClip deathSound;

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
            nav.SetDestination(target.position);
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
        if (other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;

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

            Destroy(gameObject, 3);
        }
    }
}
