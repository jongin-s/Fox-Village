using UnityEngine;

// 캔버스 오브젝트의 컴포넌트로 삽입

public class AttackButton : MonoBehaviour
{
    public bool click = false;

    public void PointerDown() { click = true; }

    public void PointerUp() { click = false; }
}
