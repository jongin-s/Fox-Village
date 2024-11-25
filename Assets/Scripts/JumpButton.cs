using UnityEngine;

// 오브젝트 Canvas의 컴포넌트

public class JumpButton : MonoBehaviour
{
    public bool click=false;

    public void PointerDown(){click=true;}

    public void PointerUp(){click=false;}
}
