using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee };
    public Type type;
    public int damage;
    public float rate;

    public AudioSource swingSound;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;

    private void Start()
    {
        meleeArea.enabled = false;
    }

    public void Use()
    {
        switch (type)
        {
            case Type.Melee:
                StopCoroutine(Swing());
                StartCoroutine(Swing());
                break;
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = true;
        swingSound.Play();

        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.4f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = false;
    }
}
