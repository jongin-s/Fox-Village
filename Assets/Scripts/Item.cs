using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type {Gold, Silver, Bronze}  // Type의 열거형, 각 아이템의 이름을 나열하고 아이템별로 Inspector에서 설정
    public Type type;  // Type의 일반형, 각 아이템의 이름을 불러올 때 사용
    public AudioClip sound;  // 각 아이템별로 원하는 클립을 Inspector에서 드래그

    void OnTriggerEnter(Collider other)  // 아이템의 Trigger Collider가 작동했을 때
    {
        if (other.tag == "Player")  // 충돌 물체의 태그가 "Player"라면
        {
            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);  // 카메라 위치에서 클립을 재생, 아이템이나 플레이어의 위치에서 재생하면 볼륨이 너무 작음
            Destroy(gameObject);  // 스스로 gameObject를 제거
        }
    }
}
