using UnityEngine;

// 캔버스의 컴포넌트

public class RunButton : MonoBehaviour
{
    public bool click=false;

    public void PointerDown(){click=true;}

    public void PointerUp(){click=false;}
}
