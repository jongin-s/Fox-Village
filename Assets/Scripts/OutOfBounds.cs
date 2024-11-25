using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 플레이어 오브젝트의 컴포넌트

public class OutOfBounds : MonoBehaviour
{
    public GameObject respawn;  // 리스폰 위치를 나타내는 빈 오브젝트

    void Update()
    {
        if (transform.position.y <= 0)  // 어떤 이유로 맵을 뚫고 떨어져서 y값이 0이 된다면
        {
            transform.position = respawn.transform.position;  // 맵의 리스폰 위치로 이동
        }
    }
}
