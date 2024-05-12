using UnityEngine;

public class IntButton : MonoBehaviour {

    public bool click = false;

    public void PointerDown() { click = true; }

    public void PointerUp() { click = false; }
}
