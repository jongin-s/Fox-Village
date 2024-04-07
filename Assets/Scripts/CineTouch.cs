using UnityEngine;
using Cinemachine;

public class CineTouch : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cineCam;  // Cinemachine ������ ī�޶�
    public FixedTouchField touchField;  // Canvas�� ������ ������ �̹���
    private float rotSensitive = 1f;  // ī�޶� �ΰ���

    private void Start()
    {
        cineCam = GetComponentInChildren<CinemachineFreeLook>();
    }

    private void Update()
    {
        cineCam.m_XAxis.Value += touchField.TouchDist.x * rotSensitive * Time.deltaTime * 45;
        cineCam.m_YAxis.Value += touchField.TouchDist.y * rotSensitive * Time.deltaTime;  // Cinemachine ������ ī�޶��� x�� ���� -180<=x<=180�� �ݸ�, y�� ���� 0<=y<=1�̱� ������ ���� ���̶� touchField ���� �������� x�� �����ӿ� ū ���� ���ؼ� ī�޶� x���� �� ���� �����̱� �ؾ� ��
    }
}
