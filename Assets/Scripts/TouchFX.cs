using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchFX : MonoBehaviour
{
    public GameObject prefab;
    float spawnTime;
    public float defaultTime = 0.05f;

    void Update()
    {
        if (Input.touchCount > 0 && spawnTime >= defaultTime)
        {
            Create();
            spawnTime = 0f;
        }
        spawnTime += Time.deltaTime;
    }

    void Create()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //mPosition.z = 0;
        Instantiate(prefab, mPosition + Camera.main.transform.forward * Vector3.Distance(Camera.main.transform.position, GameObject.Find("Canvas").transform.position), Camera.main.transform.rotation);
    }
}
