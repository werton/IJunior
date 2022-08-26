using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyDestroyer : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}