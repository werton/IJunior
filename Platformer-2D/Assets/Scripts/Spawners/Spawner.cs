using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CollectableObject _item;

    private Transform[] _spawnPoints;

    private void Awake()
    {
        CreateSpawnPoints();
    }

    public void SpawnInAllPoints()
    {
        foreach (Transform point in transform)
        {
            SpawnInPoint(point);
        }
    }

    private void CreateSpawnPoints()
    {
        _spawnPoints = new Transform[transform.childCount];

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = transform.GetChild(i);
        }
    }

    private void SpawnInPoint(Transform spawnPoint)
    {
        Instantiate(_item, spawnPoint.position, Quaternion.identity);
    }
}