using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터가 스폰하는 위치를 나타내는 빈 오브젝트 EnemyHome의 컴포넌트로 삽입

public class EnemyManager : MonoBehaviour
{
    public Transform SpawnPoint;  // 몬스터가 스폰하는 위치
    public int WaitTime;  // 몬스터가 스폰하기까지 기다리는 시간
    public GameObject[] EnemyPrefabs;  // 스폰하는 몬스터의 프리팹 (복수), 모두 인스펙터에서 설정

    void OnEnable()  // 스폰하는 오브젝트(몬스터)가 활성화될때만 실행
    {
        Enemy.OnEnemyDead += SpawnNewEnemy;  //몬스터가 기절하면 SpawnNewEnemy() 함수를 실행
    }

    void SpawnNewEnemy()
    {
        StartCoroutine(SpawnDelay());  // SpawnNewEnemy()는 코루틴 SpawnDelay()를 실행
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(WaitTime);  // 설정한 대기시간만큼 대기
        Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], SpawnPoint.transform.position, Quaternion.identity);  // 몬스터 프리팹을 랜덤으로 하나 선택하고 지정한 위치에 스폰
    }
}
