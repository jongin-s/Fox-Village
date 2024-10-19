using UnityEngine;
using Cinemachine;

public class FixedTouchField : MonoBehaviour  // 터치 포인터 핸들러를 가져와야 함
{
    [HideInInspector] public Vector2 TouchDist;  // 터치 드래그의 길이를 나타내는 2차원 벡터
    [HideInInspector] public Vector2 PointerOld;  // 처음에 터치 드래그를 시작했던 위치
    [HideInInspector] public CinemachineFreeLook cam;
    public float rotSensitive = 0.5f;  // 카메라 민감도
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
            TouchDist = new Vector2();
        }
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].rawPosition.x >= stretch && Input.touches[0].rawPosition.x < Screen.width - stretch)
            {
                if (Input.touchCount == 1)
                {
                    TouchDist = Input.touches[0].deltaPosition;

                    cam.m_XAxis.Value += TouchDist.x * rotSensitive * Time.deltaTime * 45;
                    cam.m_YAxis.Value += TouchDist.y * rotSensitive * Time.deltaTime;  // Cinemachine 프리룩 카메라의 x축 값은 -180<=x<=180인 반면, y축 값은 0<=y<=1이기 때문에 같은 길이라도 touchField 위의 움직임은 x축 움직임에 큰 값을 곱해서 카메라 x축이 더 많이 움직이기 해야 함
                }
                else if (Input.touchCount >= 2)
                {
                    Vector2 TouchDist0 = Input.touches[0].position;
                    Vector2 PointerOld0 = Input.touches[0].rawPosition;  // TouchDist는 원래 포지션 PointerOld와 새 포지션 사이의 거리 벡터

                    Vector2 TouchDist1 = Input.touches[1].position;
                    Vector2 PointerOld1 = Input.touches[1].rawPosition;  // TouchDist는 원래 포지션 PointerOld와 새 포지션 사이의 거리 벡터

                    TouchDist = (TouchDist0 - TouchDist1);
                    PointerOld = (PointerOld0 - PointerOld1);

                    float difference = TouchDist.magnitude - PointerOld.magnitude;
                    Zoom(difference * Time.deltaTime * 0.1f);
                }
                else
                {
                    TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                    PointerOld = Input.mousePosition;  // 포인터를 처음 생성할 때 실행
                }
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
