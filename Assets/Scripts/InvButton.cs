using UnityEngine;

public class InvButton : MonoBehaviour {

    public GameObject inv;  // Inspector에서 Inventory Panel을 드래그

    private void Start(){inv.SetActive(false);}  // 처음에는 inv의 활성화 상태가 false가 되어야 함

    public void ToggleInventory(){inv.SetActive(!inv.activeSelf);}  // 버튼을 누를 때마다 inv의 활성화 상태가 토글되어야 함
}
