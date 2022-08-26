using System.Collections;
using UnityEngine;

public class SpawnersStarter: MonoBehaviour
{
    [SerializeField] private float _spawnerStartDelay;

    private EnemySpawner[] _enemySpawners;

    private void Awake()
    {
        _enemySpawners = GetComponentsInChildren<EnemySpawner>();
    }

    private void Start()
    {
        StartCoroutine(StartAllSpawners(_spawnerStartDelay));
    }

    private IEnumerator StartAllSpawners(float delay)
    {
        foreach (EnemySpawner spawner in _enemySpawners)
        {
            spawner.StartInstantSpawn(_enemySpawners.Length * delay);
            yield return new WaitForSeconds(delay);
        }
    }
}