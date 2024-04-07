using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler  // ��ġ ������ �ڵ鷯�� �����;� ��
{
    [HideInInspector] public Vector2 TouchDist;  // ��ġ �巡���� ���̸� ��Ÿ���� 2���� ����
    [HideInInspector] public Vector2 PointerOld;  // ó���� ��ġ �巡�׸� �����ߴ� ��ġ
    [HideInInspector] protected int PointerId;  // ��ġ �������� �̸�
    [HideInInspector] public bool Pressed;  // ��ġ ���¸� ��Ÿ���� �Ҹ��� ��

    void Update()
    {
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)  // ��ġ �����Ͱ� ������ ��
            {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;  // TouchDist�� ���� ������ PointerOld�� �� ������ ������ �Ÿ� ����
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;  // �����͸� ó�� ������ �� ����
            }
        }
        else { TouchDist = new Vector2(); }  // ��ġ ���°� �ƴ� �� TouchDist�� �ʱ�ȭ
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;  // ��ġ�� �� �� Pressed�� true�� �����ϰ� ��ġ �����Ϳ� �� ��ġ�� ����
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
