using UnityEngine;
using Cinemachine;

public class FixedTouchField : MonoBehaviour
{
    [HideInInspector] public Vector2 TouchDist;  
    [HideInInspector] public Vector2 PointerOld;  
    [HideInInspector] public CinemachineFreeLook cam;
    public float rotSensitive = 0.5f;  
    public int stretch = 630;

    public float h0Min = 15f;
    public float h0Max = 30f;
    public float r0Min = 3f;
    public float r0Max = 6f;

    public float h1Min = 10.8f;
    public float h1Max = 21.6f;
    public float r1Min = 10.8f;
    public float r1Max = 21.6f;

    public float h2Min = 3f;
    public float h2Max = 6f;
    public float r2Min = 15f;
    public float r2Max = 30f;

    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
    }

void Update()
{
    if (Input.touchCount == 0)
    {
        TouchDist = Vector2.zero;
    }
    else if (Input.touchCount >= 1)
    {
        // 지정된 카메라 이동 구역 내에 있는 터치를 찾기
        TouchDist = Vector2.zero; // 불필요한 이동을 방지하기 위해 초기화

        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x >= stretch && touch.position.x < Screen.width - stretch)
            {
                TouchDist = touch.deltaPosition;
                
                // X축 회전은 그대로 두고, Y축 회전은 반대 방향으로 적용
                cam.m_XAxis.Value += TouchDist.x * rotSensitive * Time.deltaTime * 45;
                cam.m_YAxis.Value -= TouchDist.y * rotSensitive * Time.deltaTime;
                break; // 첫 번째 유효한 터치만 처리하여 이동
            }
        }

        if (Input.touchCount == 2)
        {
            // 두 손가락의 터치 간 거리 차이를 계산하여 줌
            Vector2 touch0PrevPos = Input.touches[0].position - Input.touches[0].deltaPosition;
            Vector2 touch1PrevPos = Input.touches[1].position - Input.touches[1].deltaPosition;

            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
            float currentTouchDeltaMag = (Input.touches[0].position - Input.touches[1].position).magnitude;
            float zoomDifference = currentTouchDeltaMag - prevTouchDeltaMag;

            Zoom(zoomDifference * Time.deltaTime * 0.1f); // 줌 인/아웃을 위한 작은 비율 적용
        }
    }
}

public void Zoom(float increment)
{
    cam.m_Orbits[0].m_Height = Mathf.Clamp(cam.m_Orbits[0].m_Height - increment, h0Min, h0Max);
    cam.m_Orbits[0].m_Radius = Mathf.Clamp(cam.m_Orbits[0].m_Radius - increment, r0Min, r0Max);
    cam.m_Orbits[1].m_Height = Mathf.Clamp(cam.m_Orbits[1].m_Height - increment, h1Min, h1Max);
    cam.m_Orbits[1].m_Radius = Mathf.Clamp(cam.m_Orbits[1].m_Radius - increment, r1Min, r1Max);
    cam.m_Orbits[2].m_Height = Mathf.Clamp(cam.m_Orbits[2].m_Height - increment, h2Min, h2Max);
    cam.m_Orbits[2].m_Radius = Mathf.Clamp(cam.m_Orbits[2].m_Radius - increment, r2Min, r2Max);
}
}
