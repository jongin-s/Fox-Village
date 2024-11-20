using UnityEngine;
using Cinemachine;

// 카메라 오브젝트 FreeLook Camera의 컴포넌트로 삽입

public class FixedTouchField : MonoBehaviour
{
    [HideInInspector] public Vector2 TouchDist;  // 터치 거리
    [HideInInspector] public Vector2 PointerOld;  // 터치를 시작한 위치
    [HideInInspector] public CinemachineFreeLook cam;  // CinemachineFreeLook 컴포넌트, 이상은 퍼블릭 변수지만 인스펙터에서 숨김
    public float rotSensitive = 0.5f;  // 회전 민감도
    public float zoomSensitive = 0.25f;  // 줌 민감도
    public int stretch = 630;  // 화면의 여백

    public float h0Min = 15f;
    public float h0Max = 30f;  // Top 리그의 높이의 최솟값과 최댓값
    public float r0Min = 3f;
    public float r0Max = 6f;  // Top 리그의 반지름의 최솟값과 최댓값

    public float h1Min = 10.8f;
    public float h1Max = 21.6f;  // Middle 리그의 높이의 최솟값과 최댓값
    public float r1Min = 10.8f;
    public float r1Max = 21.6f;  // Middle 리그의 반지름의 최솟값과 최댓값

    public float h2Min = 3f;
    public float h2Max = 6f;  // Bottom 리그의 높이의 최솟값과 최댓값
    public float r2Min = 15f;
    public float r2Max = 30f;  // Bottom 리그의 반지름의 최솟값과 최댓값

    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();  // 스스로 컴포넌트를 가져옴
    }

    void Update()
    {
        if (Input.touchCount == 0)  // 터치가 없을 때
        {
            TouchDist = Vector2.zero;  // 터치 거리는 0
        }
        else if (Input.touchCount == 1)  // 지정된 카메라 이동 구역 내에 있는 터치 1개를 찾기
        {
            if (Input.touches[0].position.x >= stretch && Input.touches[0].position.x < Screen.width - stretch)  // 터치가 화면의 가로 여백 내에 있을 때
            {
                TouchDist = Input.touches[0].deltaPosition;  // 터치 거리는 첫번째 터치의 델타값(변위)
            }
            else
            {
                TouchDist = Vector2.zero;
            }
            cam.m_XAxis.Value += TouchDist.x * rotSensitive * Time.deltaTime * 45;
            cam.m_YAxis.Value -= TouchDist.y * rotSensitive * Time.deltaTime;  // x축 회전은 그대로 두고, y축 회전은 반대 방향으로 적용
        }
        else if (Input.touchCount == 2)  // 터치가 2개일 때
        {
            if ((Input.touches[0].position.x >= stretch && Input.touches[0].position.x < Screen.width - stretch)
                && !(Input.touches[1].position.x >= stretch && Input.touches[1].position.x < Screen.width - stretch))  // 터치가 화면의 가로 여백 내에 있을 때
            {
                TouchDist = Input.touches[0].deltaPosition;  // 터치 거리는 첫번째 터치의 델타값(변위)
            }
            if (!(Input.touches[0].position.x >= stretch && Input.touches[0].position.x < Screen.width - stretch)
                && (Input.touches[1].position.x >= stretch && Input.touches[1].position.x < Screen.width - stretch))  // 터치가 화면의 가로 여백 내에 있을 때
            {
                TouchDist = Input.touches[1].deltaPosition;  // 터치 거리는 첫번째 터치의 델타값(변위)
            }
            if (!(Input.touches[0].position.x >= stretch && Input.touches[0].position.x < Screen.width - stretch)
                && !(Input.touches[1].position.x >= stretch && Input.touches[1].position.x < Screen.width - stretch))
            {
                TouchDist = Vector2.zero;
            }
            cam.m_XAxis.Value += TouchDist.x * rotSensitive * Time.deltaTime * 45;
            cam.m_YAxis.Value -= TouchDist.y * rotSensitive * Time.deltaTime;  // x축 회전은 그대로 두고, y축 회전은 반대 방향으로 적용

            if ((Input.touches[0].position.x >= stretch && Input.touches[0].position.x < Screen.width - stretch)
                && (Input.touches[1].position.x >= stretch && Input.touches[1].position.x < Screen.width - stretch))
            {
                Vector2 touch0PrevPos = Input.touches[0].position - Input.touches[0].deltaPosition;
                Vector2 touch1PrevPos = Input.touches[1].position - Input.touches[1].deltaPosition;  // 두 손가락의 터치 간 거리 차이를 계산하여 줌

                float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;  // 두 터치의 원래 위치의 차이
                float currentTouchDeltaMag = (Input.touches[0].position - Input.touches[1].position).magnitude;  // 두 터치의 최종 위치의 차이
                float zoomDifference = currentTouchDeltaMag - prevTouchDeltaMag;  // 두 차이를 빼면 줌인인지 줌아웃인지를 결정 가능

                Zoom(zoomDifference * Time.deltaTime * zoomSensitive); // 줌 민감도 적용
            }
        }
    }

    public void Zoom(float increment)  // 줌 함수
    {
        cam.m_Orbits[0].m_Height = Mathf.Clamp(cam.m_Orbits[0].m_Height - increment, h0Min, h0Max);
        cam.m_Orbits[0].m_Radius = Mathf.Clamp(cam.m_Orbits[0].m_Radius - increment, r0Min, r0Max);
        cam.m_Orbits[1].m_Height = Mathf.Clamp(cam.m_Orbits[1].m_Height - increment, h1Min, h1Max);
        cam.m_Orbits[1].m_Radius = Mathf.Clamp(cam.m_Orbits[1].m_Radius - increment, r1Min, r1Max);
        cam.m_Orbits[2].m_Height = Mathf.Clamp(cam.m_Orbits[2].m_Height - increment, h2Min, h2Max);
        cam.m_Orbits[2].m_Radius = Mathf.Clamp(cam.m_Orbits[2].m_Radius - increment, r2Min, r2Max);  // 각 리그의 높이와 반지름이 최솟값과 최댓값을 벗어나지 않도록 설정
    }
}
