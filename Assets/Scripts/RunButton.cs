using UnityEngine;

public class RunButton : MonoBehaviour {

    public bool click=false;

    public void PointerDown(){click=true;}

    public void PointerUp(){click=false;}
}
