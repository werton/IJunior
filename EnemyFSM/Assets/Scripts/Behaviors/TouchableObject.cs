using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class TouchableObject : MonoBehaviour
{
    public event UnityAction Touched;

    public virtual void OnTouch()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            OnTouch();
            Touched?.Invoke();
        }
    }
}