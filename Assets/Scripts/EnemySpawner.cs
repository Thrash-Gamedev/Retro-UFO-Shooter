using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnFrequency;
    public float radius;
    public static float Radius;
    private ObjectPooler _enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        _enemyPool = GetComponent<ObjectPooler>();
        StartCoroutine(SpawnRoutine());
        StartCoroutine(IncreaseRateRoutine());
        Radius = radius;
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / spawnFrequency);

            Vector2 spawnPosition = Random.insideUnitCircle.normalized * radius;
            _enemyPool.SpawnAt(spawnPosition);
        }
    }

    IEnumerator IncreaseRateRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            spawnFrequency += .05f;
        }
    }
}
