using UnityEngine;

// 각 아이템 오브젝트에 컴포넌트로 삽입

public class Item : MonoBehaviour
{
    public enum Type {Jade, Diamond, Ruby, Amethyst, Weapon}  // Type의 열거형, 각 아이템의 이름을 나열하고 아이템별로 인스펙터에서 설정
    public Type type;  // Type의 일반형, 각 아이템의 이름을 불러올 때 사용
    public AudioClip sound;  // 원하는 오디오 클립을 인스펙터로 드래그
    public int value;  // Weapon 타입의 아이템에서만 사용하는 무기의 일련번호

    void OnTriggerEnter(Collider other)  // 아이템의 Trigger Collider가 작동했을 때
    {
        if (other.tag == "Player")  // 충돌 물체의 태그가 "Player"라면
        {
            AudioSource.PlayClipAtPoint(sound, transform.position);  // 카메라 위치에서 클립을 재생, 아이템이나 플레이어의 위치에서 재생하면 볼륨이 너무 작음
            Destroy(gameObject);  // 스스로 gameObject를 제거
        }
    }
}
