using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캔버스의 컴포넌트

public class WeaponButton : MonoBehaviour
{
    public bool sDown1;
    public bool sDown2;
    public bool sDown3;
    public bool sDown4;

    public void SDown1True() { sDown1 = true; }
    public void SDown1False() { sDown1 = false; }
    public void SDown2True() { sDown2 = true; }
    public void SDown2False() { sDown2 = false; }
    public void SDown3True() { sDown3 = true; }
    public void SDown3False() { sDown3 = false; }
    public void SDown4True() { sDown4 = true; }
    public void SDown4False() { sDown4 = false; }
}
