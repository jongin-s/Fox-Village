using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
