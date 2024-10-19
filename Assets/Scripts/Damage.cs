using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int curHealth;
    [HideInInspector] public bool isDamage;
    [HideInInspector] public bool isDead;

    public AudioClip hitSound;
    public AudioClip deathSound;
    public HealthBar healthBar;
    public GameObject menu;

    Rigidbody rigid;
    MeshRenderer[] meshes;
    Animator anim;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        meshes = GetComponentsInChildren<MeshRenderer>();
        anim = GetComponentInChildren<Animator>();

        curHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        menu.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyAttack")
        {
            if (!isDamage && curHealth > 0)
            {
                EnemyAttack enemyAtk = other.GetComponent<EnemyAttack>();
                curHealth -= enemyAtk.damage;
                healthBar.SetHealth(curHealth);

                Vector3 reactVec = transform.position - other.transform.position;

                StartCoroutine(OnDamage(reactVec));

                Debug.Log($"Player: {curHealth}");
            }
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        isDamage = true;
        foreach(MeshRenderer mesh in meshes)
        {
            mesh.material.color = Color.yellow;
        }
        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);

        reactVec = reactVec.normalized;
        reactVec += Vector3.up;
        rigid.AddForce(reactVec * 2.5f, ForceMode.Impulse);

        yield return new WaitForSeconds(0.1f);

        isDamage = false;
        if (curHealth > 0)
        {
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.material.color = Color.white;
            }
            yield return new WaitForSeconds(0.9f);
        }
        else
        {
            foreach (MeshRenderer mesh in meshes)
            {
                //mesh.material.color = Color.blue;
                mesh.material.color = Color.white;
            }

            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            gameObject.layer = 9;

            anim.SetTrigger("doDie");

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            isDead = true;
            menu.SetActive(true);
        }
    }
}
