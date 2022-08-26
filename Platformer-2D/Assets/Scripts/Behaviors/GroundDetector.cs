using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundDetector : MonoBehaviour
{   
    private const float CastAngle = 0f;
    private const float CastDistance = .1f;

    [SerializeField] private LayerMask _groundLayer;

    private Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(_collider2D.bounds.center, _collider2D.bounds.size, CastAngle, Vector2.down, CastDistance, _groundLayer);
    }


}