using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public bool click = false;

    public void PointerDown() { click = true; }

    public void PointerUp() { click = false; }
}
