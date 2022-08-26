using UnityEngine;

[RequireComponent(typeof(TouchableObject))]
public class TouchableSpawner : Spawner
{
    public TouchableObject Body { get; private set; }

    private void Awake()
    {
        Body = GetComponent<TouchableObject>();
    }

    private void OnEnable()
    {
        Body.Touched += SpawnOnce;        
    }
    private void OnDisable()
    {
        Body.Touched -= SpawnOnce;
    }

    private void SpawnOnce()
    {
        SpawnInAllPoints();
        Body.Touched -= SpawnOnce;
    }
}