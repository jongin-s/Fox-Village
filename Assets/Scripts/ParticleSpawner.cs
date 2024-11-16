using UnityEngine;

// 오브젝트 Canvas의 자식 오브젝트 Particle의 컴포넌트

public class UITouchParticleSpawner : MonoBehaviour
{
    public ParticleSystem particlePrefab; // UI용 파티클 프리팹
    public Canvas canvas; // 파티클을 올릴 Canvas

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 터치나 클릭 감지
        {
            Vector3 touchPosition = Input.mousePosition; // 터치 위치
            touchPosition.z = 0f;

            ParticleSystem particle = Instantiate(particlePrefab, touchPosition, Quaternion.identity, canvas.transform); // 캔버스 안에 파티클 생성
            particle.transform.localScale = new Vector3(300, 300, 300); // 크기를 300으로 설정
            particle.gameObject.layer = LayerMask.NameToLayer("UI"); // "UI" 레이어로 설정
            particle.Play(); // 파티클 재생

            Destroy(particle.gameObject, particle.main.duration); // 지속 시간 후 파괴
        }
    }
}
