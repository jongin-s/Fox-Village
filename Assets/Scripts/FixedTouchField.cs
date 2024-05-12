using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {  // 터치 포인터 핸들러를 가져와야 함

    [HideInInspector] public Vector2 TouchDist;  // 터치 드래그의 길이를 나타내는 2차원 벡터
    [HideInInspector] public Vector2 PointerOld;  // 처음에 터치 드래그를 시작했던 위치
    [HideInInspector] protected int PointerId;  // 터치 포인터의 이름
    [HideInInspector] public bool Pressed;  // 터치 상태를 나타내는 불리언 값

    void Update() {

        if (Pressed) {

            if (PointerId >= 0 && PointerId < Input.touches.Length) {  // 터치 포인터가 존재할 때

                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;  // TouchDist는 원래 포지션 PointerOld와 새 포지션 사이의 거리 벡터
            }
            else { 

                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;  // 포인터를 처음 생성할 때 실행
            }
        }
        else { TouchDist = new Vector2(); }  // 터치 상태가 아닐 때 TouchDist를 초기화
    }

    public void OnPointerDown(PointerEventData eventData) {

        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;  // 터치를 할 때 Pressed를 true로 설정하고 터치 포인터와 그 위치를 생성
    }

    public void OnPointerUp(PointerEventData eventData) {

        Pressed = false;
    }
}
