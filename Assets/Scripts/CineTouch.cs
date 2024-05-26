using UnityEngine;
using Cinemachine;

public class CineTouch : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cineCam;  // Cinemachine 프리룩 카메라
    public FixedTouchField touchField;  // Canvas에 생성한 투명한 이미지
    private float rotSensitive = 1f;  // 카메라 민감도

    private void Start()
    {
        cineCam = GetComponentInChildren<CinemachineFreeLook>();
    }

    private void Update()
    {
        cineCam.m_XAxis.Value += touchField.TouchDist.x * rotSensitive * Time.deltaTime * 45;
        cineCam.m_YAxis.Value += touchField.TouchDist.y * rotSensitive * Time.deltaTime;  // Cinemachine 프리룩 카메라의 x축 값은 -180<=x<=180인 반면, y축 값은 0<=y<=1이기 때문에 같은 길이라도 touchField 위의 움직임은 x축 움직임에 큰 값을 곱해서 카메라 x축이 더 많이 움직이기 해야 함
    }
}
