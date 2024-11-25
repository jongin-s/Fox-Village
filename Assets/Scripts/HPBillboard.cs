using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy 오브젝트의 자식 오브젝트 HPCanvas의 컴포넌트로 삽입

public class HPBillboard : MonoBehaviour
{
    public Transform cam;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
