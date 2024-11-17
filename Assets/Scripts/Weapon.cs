using UnityEngine;
using System.Collections;

// 플레이어의 손에 들린 무기 자체의 컴포넌트

public class Weapon : MonoBehaviour
{
    public enum Type { Melee };
    public Type type;  // 무기의 타입
    public int damage;  // 무기의 대미지
    public float rate;  // 무기의 속도

    public AudioSource swingSound;  // 무기의 휘두르는 오디오 소스
    public BoxCollider meleeArea;  // 무기의 공격 영역
    public TrailRenderer trailEffect;  // 무기의 트레일 효과

    private void Start()
    {
        meleeArea.enabled = false;  // 시작할 때 무기의 공격 영역을 비활성화
    }

    public void Use()
    {
        switch (type)
        {
            case Type.Melee:  // 무기 타입이 "Melee"일 때
                StopCoroutine(Swing());
                StartCoroutine(Swing());  // 코루틴 Swing 실행
                break;
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = true;  // 트레일 효과 활성화
        swingSound.Play();  // 휘두르는 오디오 실행

        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;  // 공격 영역 활성화

        yield return new WaitForSeconds(0.4f);
        meleeArea.enabled = false;  // 공격 영역 비활성화

        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = false;  // 트레일 효과 비활성화
    }
}
