using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(DamageableObject))]

public class TRex : Enemy
{
    private const float BitingDistance = 6f;

    [SerializeField] private Character _target;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private DamageableObject _damageble;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _damageble = GetComponent<DamageableObject>();
    }

    private Vector2 GetTargetDistance(Character target)
    {
        return transform.position - target.transform.position;
    }

}