using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform SpawnPoint;
    public int WaitTime;
    public GameObject[] EnemyPrefabs;

    void OnEnable()
    {
        Enemy.OnEnemyDead += SpawnNewEnemy;
    }

    void SpawnNewEnemy()
    {
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(WaitTime);
        Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], SpawnPoint.transform.position, Quaternion.identity);
    }
}
