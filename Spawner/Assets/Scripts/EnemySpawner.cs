using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyTemplate;

    private Coroutine _spawnCoroutine;

    public void StartInstantSpawn(float spawnDelay)
    {
        _spawnCoroutine = StartCoroutine(StartSpawn(spawnDelay));
    }

    public void StopInstantSpawn()
    {
        StopCoroutine(_spawnCoroutine);
    }

    public void SpawnInPosition(Vector3 position)
    {
        Instantiate(_enemyTemplate, position, Quaternion.identity);
    }

    private IEnumerator StartSpawn(float delay)
    {
        bool isSpawnStarted = true;

        while (isSpawnStarted == true)
        {
            SpawnInPosition(transform.position);

            yield return new WaitForSeconds(delay);
        }
    }
}